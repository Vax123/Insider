using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Data;
namespace Insider
{
    /// <summary>
    /// Random forest classifier. Implementation
    /// leverages both ordinal and categorical features.
    /// </summary>
    public class RandomForestC
    {

        DbConnect dbc = new DbConnect();
        string sq = "";
        private const float FeatureFraction = 0.5f;
        private const float InstanceFraction = 0.66f;
        private const int MinSampleCount = 4;
        private const int MaxDepth = 100;

        public enum FeatureType
        {
            Ordinal = 0,
            Categorical = 1,
        }

        [DebuggerDisplay("FeatureIndex:{FeatureIndex} Alpha:{Alpha}")]
        private struct CompactNode
        {
            public readonly double  FeatureIndex;

            /// <summary>
            /// Value recycled for distinct purposes: 
            /// - inclusive threshold on the left for ordinal variable
            /// - inclusive bitarray on the left for small categorical variable
            /// - constant at ushort.MaxValue for large categorical variable
            /// 
            /// In case of a circular variable:
            /// - lower 8bits represent the min (inclusive)
            /// - higher 8bits represent the max (exclusive)
            /// </summary>
            public readonly double  Alpha;

            public readonly int Left;

            public int Right => Left + 1;

            public bool IsLeaf => FeatureIndex == ushort.MaxValue;

            public bool IsLargeCategorical => Alpha == int.MaxValue;

            public CompactNode(double  label)
            {
                Left = 0;
                Alpha = label;
                FeatureIndex = ushort.MaxValue;
            }

            public CompactNode(double featureIndex, double  alpha, int left)
            {
                FeatureIndex = featureIndex;
                Alpha = alpha;
                Left = left;
            }

            public bool IsLargeCategoricalLeft(BitArray feature, BitArray array)
            {
                return feature < array.Length && array[feature];
            }

            public bool IsSmallCategoricalLeft(double  feature)
            {
                return (Alpha & (1 << (ushort )feature)) != 0;
            }
        }

        private class Tree
        {
            public readonly FeatureType[] Features;

            public readonly CompactNode[] Nodes;

            public readonly Dictionary<int, BitArray> LeftInstances;

            public Tree(FeatureType[] features, CompactNode[] nodes, Dictionary<int, BitArray> leftInstances)
            {
                Features = features;
                Nodes = nodes;
                LeftInstances = leftInstances;
            }

            /// <summary> Syntactic sugar. </summary>
            public double  Regress(double [] instance)
            {
                return Classify(instance);
            }

            public double  Classify(double [] instance)
            {
                var node = Nodes[0];
                var nodeIndex = 0;

                while (!node.IsLeaf)
                {
                    var v = instance[node.FeatureIndex];

                    switch (Features[node.FeatureIndex])
                    {
                        case FeatureType.Ordinal:
                            nodeIndex = v <= node.Alpha ? node.Left : node.Right;
                            break;
                        case FeatureType.Categorical:
                            nodeIndex = (node.IsLargeCategorical ?
                                node.IsLargeCategoricalLeft(v, LeftInstances[nodeIndex]) :
                                node.IsSmallCategoricalLeft(v)) ? node.Left : node.Right;
                            break;
                        default:
                            throw new NotSupportedException();
                    }

                    node = Nodes[nodeIndex];
                }

                return node.Alpha;
            }
        }

        private static Tree BuildTree(Node root, FeatureType[] features)
        {
            var list = new List<Node>();
            var queue = new Queue<Node>();

            list.Add(root);
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var x = queue.Dequeue();
                if (x.IsLeaf) continue;

                x.LeftIndex = list.Count;
                list.Add(x.Left);
                list.Add(x.Right);
                queue.Enqueue(x.Left);
                queue.Enqueue(x.Right);
            }

            var compact = new CompactNode[list.Count];
            var leftInstances = new Dictionary<int, BitArray>();
            for (int i = 0; i < list.Count; i++)
            {
                var ni = list[i];
                if (ni.IsLeaf)
                {
                    compact[i] = new CompactNode(ni.Label);
                }
                else
                {
                    if (features[ni.FeatureIndex] == FeatureType.Ordinal)
                    {
                        compact[i] = new CompactNode(ni.FeatureIndex, ni.Threshold, ni.LeftIndex);
                    }
                    else
                    {
                        if (ni.LeftInstances.Length <= 15)
                        {
                            var threshold = (ushort)ConvertToInt(ni.LeftInstances);
                            compact[i] = new CompactNode(ni.FeatureIndex, threshold, ni.LeftIndex);
                        }
                        else
                        {
                            leftInstances.Add(i, ni.LeftInstances);
                            compact[i] = new CompactNode(ni.FeatureIndex, int.MaxValue, ni.LeftIndex);
                        }
                    }
                }
            }

            return new Tree(features, compact, leftInstances);
        }

        private static int ConvertToInt(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            var array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        private class Node
        {
            public readonly bool IsLeaf;
            public readonly double  Label;

            public readonly double    FeatureIndex;
            public readonly double  Threshold;
            public readonly BitArray LeftInstances;
            public readonly Node Left;
            public readonly Node Right;

            /// <summary>Mutable element used flattening the tree</summary>
            public int LeftIndex;

            public Node(double  featureIndex, double  threshold, BitArray leftInstances, Node left, Node right)
            {
                FeatureIndex = featureIndex;
                Threshold = threshold;
                LeftInstances = leftInstances;
                Left = left;
                Right = right;
            }

            /// <summary>Leaf constructor</summary>
            public Node(double  label)
            {
                IsLeaf = true;
                Label = label;
            }
        }

        [DebuggerDisplay("FeatureIndex:{FeatureIndex} Entropy:{Entropy}")]
        private class Split
        {
            public readonly int FeatureIndex;
            public readonly float Entropy;
            public readonly int[] Left;
            public readonly int[] Right;
            public readonly ushort Threshold;
            public readonly BitArray LeftInstances;

            public Split(int featureIndex, float entropy, int[] left, int[] right, ushort threshold, BitArray leftInstances)
            {
                FeatureIndex = featureIndex;
                Entropy = entropy;
                Left = left;
                Right = right;
                Threshold = threshold;
                LeftInstances = leftInstances;
            }
        }

        [DebuggerDisplay("Instance:{Instance} Label:{Label}")]
        private struct InstanceLabel : IComparable<InstanceLabel>
        {
            public readonly ushort Instance;
            public readonly int Label;

            public InstanceLabel(ushort instance, int label)
            {
                Instance = instance;
                Label = label;
            }

            public int CompareTo(InstanceLabel other)
            {
                var c = Instance.CompareTo(other.Instance);
                return c != 0 ? c : Label.CompareTo(other.Label);
            }

            /// <summary>Bucket Sort by the InstanceLabel.Label values.</summary>
            public static void BucketSort(InstanceLabel[] pairs)
            {
                var counts = new List<int>();
                for (var i = 0; i < pairs.Length; i++)
                {
                    var inputi = pairs[i].Instance;

                    if (inputi < counts.Count)
                    {
                        counts[inputi] += 1;
                    }
                    else
                    {
                        for (var j = counts.Count; j < inputi; j++)
                        {
                            counts.Add(0);
                        }
                        counts.Add(1); // for the 'inputi'
                    }
                }

                // computing cumulative indices
                for (var i = 1; i < counts.Count; i++) counts[i] += counts[i - 1];
                for (var i = counts.Count - 1; i > 0; i--) counts[i] = counts[i - 1];
                counts[0] = 0;

                var defined = new bool[pairs.Length];

                // actual in-place sort 
                // last pair should be skipped (already swapped)
                for (var i = 0; i < pairs.Length - 1;)
                {
                    if (defined[i])
                    {
                        i++;
                        continue;
                    }

                    var pairi = pairs[i];
                    var n = counts[pairi.Instance];

                    counts[pairi.Instance] = n + 1;

                    // we swap 'i' and 'n'
                    var pairn = pairs[n];
                    pairs[n] = pairi;
                    pairs[i] = pairn;
                    defined[n] = true;
                }
            }
        }

        [DebuggerDisplay("LabelCount:{LabelCount} Instance:{Instance}")]
        private struct CountInstance : IComparable<CountInstance>
        {
            public readonly int LabelCount;

            public readonly ushort Instance;

            public CountInstance(int labelCount, ushort instance)
            {
                LabelCount = labelCount;
                Instance = instance;
            }

            public int CompareTo(CountInstance other)
            {
                var c = LabelCount.CompareTo(other.LabelCount);
                return c != 0 ? c : Instance.CompareTo(other.Instance);
            }
        }

        public static double [][] Classify(
            FeatureType[] features, double [][] instances, int[] labels, double [] unlabeled,
            int treeCount = 500, int degreeOfParallism = 1)
        {
            var instanceSampleCount = (int)(labels.Length * InstanceFraction);
            var featureSampleCount = (int)((features.Length + 1) * FeatureFraction);

            var seed = 42;
            var trees = BuildForest(features, instances, labels,
                instanceSampleCount, featureSampleCount, treeCount, seed, degreeOfParallism);

            var results = new double [unlabeled.Length][];

            for (int i = 0; i < unlabeled.Length; i++)
            {
                var ui = unlabeled[i][];
                results[i] = trees.Select(t => t.Classify(ui)).ToArray();
            }

            return results;
        }

        private static Tree[] BuildForest(
            FeatureType[] features, double [][] instances, int  [] labels,
            int instanceSampleCount, int featureSampleCount, int treeCount, int seed, int degreeOfParallism)
        {
            var trees = Enumerable.Range(0, treeCount)
                .AsParallel().AsOrdered().WithDegreeOfParallelism(degreeOfParallism)
                .Select(i =>
                {
                    var rand = new Random(seed + i);

                    // fast sampling, it's OK to select the same pair input/label multiple times
                    var sampleInstances = new double [instanceSampleCount][];
                    var sampleLabels = new double [instanceSampleCount];
                    for (int j = 0; j < instanceSampleCount; j++)
                    {
                        var n = rand.Next(labels.Length);
                        sampleInstances[j] = instances[n];
                        sampleLabels[j] = labels[n];
                    }

                    var node = BuildNode(features, featureSampleCount, sampleInstances, sampleLabels, rand.Next(), 0);
                    return BuildTree(node, features);
                })
                .ToArray();

            return trees;
        }

        /// <remarks>Inputs have already been sampled (with repetition).</remarks>
        private static Node BuildNode(
            FeatureType[] features, int featureSampleCount, double [][] instances, double [] labels,
            int seed, int depth)
        {
            if (instances.Length == 0)
            {
                return new Node(0); // very degenerate case
            }

            var maxLabel = labels.Max();
            var minLabel = labels.Min();

            // if there is only one label is left, then return a leaf
            if (maxLabel == minLabel)
            {
                return new Node(maxLabel);
            }

            // if labels are too few, or if we are too deep, then pick a leaf at random from the labels
            var rand = new Random(seed);
            if (labels.Length < MinSampleCount || depth >= MaxDepth)
            {
                return new Node(labels[rand.Next(labels.Length)]);
            }

            // TODO: [vermorel] May 2016, 'featureSample' calculation should be isolated

            // fast variable sampling
            // only the 'varSampleSize' first cells of the table get initialized
            var featureSample = new int[features.Length];
            for (int i = 0; i < featureSampleCount; i++)
            {
                var n = rand.Next(features.Length);

                // lazily generating the values only swap only 
                // the zero has the semantic 'undefined'
                var vi = featureSample[i];
                vi = vi > 0 ? vi : i + 1;
                var vn = featureSample[n];
                vn = vn > 0 ? vn : n + 1;

                featureSample[i] = vn;
                featureSample[n] = vi;
            }

            var splits = new Split[featureSampleCount];
            var instance = new double [instances.Length];
            for (int i = 0; i < featureSampleCount; i++)
            {
                var v = featureSample[i] - 1; // variable indices are shifted of +1 (algorithmic trick above)

                for (int j = 0; j < instances.Length; j++) // recycling the input vector
                {
                    instance[j] = instances[j][v];
                }

                switch (features[v])
                {
                    case FeatureType.Ordinal:
                        splits[i] = ClassifyOrdinal(v, instance, labels, maxLabel);
                        break;
                    case FeatureType.Categorical:
                        splits[i] = ClassifyCategorical(v, instance, labels, maxLabel);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            // TODO: [vermorel] May 2016, use ArgMin here

            // finding the minimum (dumb code faster than Linq)
            var minEntropy = float.MaxValue;
            Split bestSplit = null;
            for (int i = 0; i < splits.Length; i++)
            {
                var spliti = splits[i];
                if (spliti.Entropy < minEntropy)
                {
                    minEntropy = spliti.Entropy;
                    bestSplit = spliti;
                }
            }

            // if the best split does not add anything, return a leaf
            if (bestSplit.Left.Length == 0 || bestSplit.Right.Length == 0)
            {
                return new Node(labels[rand.Next(labels.Length)]);
            }

            var left = bestSplit.Left;
            var leftInstances = new double [left.Length][];
            var leftLabels = new double [left.Length];
            for (int i = 0; i < leftInstances.Length; i++)
            {
                var lefti = left[i];
                leftInstances[i] = instances[lefti];
                leftLabels[i] = labels[lefti];
            }

            var right = bestSplit.Right;
            var rightInstances = new double [right.Length][];
            var rightLabels = new double [right.Length];
            for (int i = 0; i < rightInstances.Length; i++)
            {
                var righti = right[i];
                rightInstances[i] = instances[righti];
                rightLabels[i] = labels[righti];
            }

            var leftNode = BuildNode(features, featureSampleCount, leftInstances, leftLabels, rand.Next(), depth + 1);
            var rightNode = BuildNode(features, featureSampleCount, rightInstances, rightLabels, rand.Next(), depth + 1);

            return new Node((ushort)bestSplit.FeatureIndex, bestSplit.Threshold, bestSplit.LeftInstances, leftNode, rightNode);
        }

        //private static Split ClassifyOrdinal(int featureIndex, double [] instances, double  [] labels, double   maxLabel)
        //{
        //    var labelCounts = new double [maxLabel + 1];
        //    for (int i = 0; i < labels.Length; i++)
        //    {
        //        labelCounts[labels[i]] += 1;
        //    }

        //    var pairs = new InstanceLabel[labels.Length];
        //    for (int i = 0; i < pairs.Length; i++)
        //    {
        //        pairs[i] = new InstanceLabel(instances[i], labels[i]);
        //    }

        //    // no bucket sort here, ordinal values can be large
        //    Array.Sort(pairs);

        //    var minEntropy = double.MaxValue;
        //    var minEntropyIndex = -1;

        //    var partialCounts = new int[maxLabel + 1];

        //    for (int i = 0; i < pairs.Length; i++)
        //    {
        //        var pair = pairs[i];
        //        partialCounts[pair.Label] += 1;

        //        // entropy calculation only applies at input thresholds
        //        // otherwise, the threshold won't properly reflect the partition
        //        if (i == pairs.Length - 1 || pair.Instance != pairs[i + 1].Instance)
        //        {
        //            if (i == pairs.Length - 1 && minEntropyIndex > 0) continue;

        //            double leftEntropy = 0.0, rightEntropy = 0.0;
        //            for (int j = 0; j <= maxLabel; j++)
        //            {
        //                var cj = partialCounts[j];

        //                var labelj = labelCounts[j];

        //                //if (cj > labelj)
        //                //    throw new InvalidOperationException();

        //                var lpj = cj / (double)(i + 1);
        //                if (lpj > 0 && lpj < 1)
        //                {
        //                    leftEntropy -= lpj * Math.Log(lpj);
        //                }

        //                var rpj = (labelj - cj) / (double)(labels.Length - i - 1);
        //                if (rpj > 0 && rpj < 1)
        //                {
        //                    rightEntropy -= rpj * Math.Log(rpj);
        //                }

        //                //if (lpj > 1 || rpj > 1)
        //                //    throw new InvalidOperationException();
        //                //if (double.IsNaN(rightEntropy))
        //                //    throw new InvalidOperationException();
        //            }

        //            var entropy = ((i + 1) * leftEntropy + (labels.Length - i) * rightEntropy) / labels.Length;

        //            //if (double.IsNaN(entropy))
        //            //    throw new InvalidOperationException();

        //            if (entropy < minEntropy)
        //            {
        //                minEntropy = entropy;
        //                minEntropyIndex = i;
        //            }
        //        }
        //    }

        //    var threshold = pairs[minEntropyIndex].Instance;

        //    var left = new int[minEntropyIndex + 1];
        //    var right = new int[labels.Length - left.Length];
        //    for (int i = 0, j = 0, k = 0; i < labels.Length; i++)
        //    {
        //        var vi = instances[i];
        //        if (vi <= threshold) left[j++] = i;
        //        else right[k++] = i;
        //    }

        //    return new Split(featureIndex, (float)minEntropy, left, right, threshold, null);
        //}

        //private static Split ClassifyCategorical(int featureIndex, double [] instances, double [] labels, int maxLabel)
        //{
        //    var labelCounts = new int[maxLabel + 1];
        //    for (int i = 0; i < labels.Length; i++)
        //    {
        //        labelCounts[labels[i]] += 1;
        //    }

        //    var pairs = new InstanceLabel[labels.Length];
        //    for (int i = 0; i < pairs.Length; i++)
        //    {
        //        pairs[i] = new InstanceLabel(instances[i], labels[i]);
        //    }

        //    // 2/3 of compute time spent in Array.Sort, lowered to 1/3 with bucket sort
        //    InstanceLabel.BucketSort(pairs);
        //    //Array.Sort(pairs);

        //    // mapping 'Label => Input counts'
        //    var perLabel = new List<CountInstance>[maxLabel + 1];
        //    for (int j = 0; j <= maxLabel; j++) perLabel[j] = new List<CountInstance>();

        //    // mapping 'Input => Label counts'
        //    var perInstance = new Dictionary<int, int[]>();

        //    var count = new int[maxLabel + 1];
        //    var currentInstance = pairs[0].Instance;

        //    // preparing the structures need in the "main" loop below
        //    // mostly 'perLabel' and 'perInput'
        //    for (int i = 0; i < pairs.Length; i++)
        //    {
        //        var pair = pairs[i];

        //        if (pair.Instance != currentInstance)
        //        {
        //            for (int j = 0; j <= maxLabel; j++)
        //            {
        //                var cj = count[j];
        //                if (cj > 0)
        //                {
        //                    perLabel[j].Add(new CountInstance(cj, currentInstance));
        //                }
        //            }

        //            perInstance.Add(currentInstance, count);

        //            count = new int[maxLabel + 1];
        //            currentInstance = pair.Instance;
        //        }

        //        count[pair.Label] += 1;
        //    }

        //    // don't miss the last input
        //    {
        //        var ii = pairs[pairs.Length - 1].Instance;

        //        for (int j = 0; j <= maxLabel; j++)
        //        {
        //            var cj = count[j];
        //            if (cj > 0)
        //            {
        //                perLabel[j].Add(new CountInstance(cj, ii));
        //            }
        //        }

        //        perInstance.Add(ii, count);
        //    }

        //    //if (perLabel.Sum(x => x.Sum(y => y.LabelCount)) != labels.Length)
        //    //    throw new InvalidOperationException();
        //    //if (perInput.Sum(x => x.Value.Sum()) != labels.Length)
        //    //    throw new InvalidOperationException();

        //    var minEntropy = double.MaxValue;
        //    var minEntropyLeftCount = 0;
        //    var minEntropyLeft = new ushort[0];

        //    // For every label value
        //    for (int j = 0; j <= maxLabel; j++)
        //    {
        //        // Probe all partitions obtained through a sort on this label value
        //        var pairsj = perLabel[j];
        //        pairsj.Sort();

        //        // Partition probing is similar to the one in 'SplitOrdinal'
        //        var partialCounts = new int[maxLabel + 1];
        //        for (int i = 0; i < pairsj.Count; i++)
        //        {
        //            var iji = pairsj[i].Instance;

        //            var c = perInstance[iji];
        //            for (var k = 0; k <= maxLabel; k++) partialCounts[k] += c[k];

        //            double leftEntropy = 0.0, rightEntropy = 0.0;
        //            var leftCount = 0;
        //            for (int k = 0; k <= maxLabel; k++)
        //            {
        //                var ck = partialCounts[k];
        //                leftCount += ck;

        //                var labelk = labelCounts[k];

        //                var lpk = ck / (double)leftCount;
        //                if (lpk > 0 && lpk < 1)
        //                {
        //                    leftEntropy -= lpk * Math.Log(lpk);
        //                }

        //                var rpk = (labelk - ck) / (double)(labels.Length - leftCount);
        //                if (rpk > 0 && rpk < 1)
        //                {
        //                    rightEntropy -= rpk * Math.Log(rpk);
        //                }

        //                //if (lpk < 0 || lpk > 1)
        //                //    throw new InvalidOperationException();
        //            }

        //            var entropy = (leftCount * leftEntropy + (labels.Length - leftCount) * rightEntropy) / labels.Length;

        //            //if (double.IsNaN(entropy))
        //            //    throw new InvalidOperationException();

        //            if (entropy < minEntropy)
        //            {
        //                minEntropy = entropy;
        //                minEntropyLeftCount = leftCount;
        //                minEntropyLeft = pairsj.Take(i + 1).Select(pji => pji.Instance).ToArray();
        //            }
        //        }
        //    }

        //    var maxInstance = minEntropyLeft.Max();
        //    var leftInstances = new BitArray(maxInstance + 1);
        //    for (int k = 0; k < minEntropyLeft.Length; k++) leftInstances[minEntropyLeft[k]] = true;

        //    var left = new int[minEntropyLeftCount];
        //    var right = new int[labels.Length - minEntropyLeftCount];
        //    for (int i = 0, j = 0, k = 0; i < labels.Length; i++)
        //    {
        //        var ii = instances[i];
        //        if (ii < leftInstances.Length && leftInstances[ii]) left[j++] = i;
        //        else right[k++] = i;
        //    }

        //    return new Split(featureIndex, (float)minEntropy, left, right, 0 /* threshold*/, leftInstances);
        //}

    }

    public class RandomForestCTests
    {
        DbConnect dbc = new DbConnect();
        string sq = "";
        SqlDataReader rd;
        [Test]
//        public void Classify_mini_mock_data()
//        {
//            var instances = ToUshort(new[]
//{
//                new[] {0, 1, 0, 0},
//                new[] {0, 1, 1, 0},
//                new[] {0, 1, 0, 1},
//                new[] {1, 1, 0, 0},
//                new[] {1, 0, 0, 0},
//                new[] {1, 1, 0, 1},
//                new[] {1, 1, 0, 1},
//            });

//            var labels = new[] { 0, 1, 1, 0, 2, 2, 1 };

//            var unlabeled = ToUshort(new[] { new[] { 1, 1, 1, 1 } });

//            var features = new[] {RandomForestC.FeatureType.Categorical, RandomForestC.FeatureType.Categorical, RandomForestC.FeatureType.Ordinal, RandomForestC.FeatureType.Ordinal};

//            var c = RandomForestC.Classify(features, instances, labels, unlabeled);
//            Assert.Contains(0, c[0]);
//            Assert.Contains(1, c[0]);
//            Assert.Contains(2, c[0]);
//        }

       // [Test]
        //public void Classify_random_data()
        //{
        //    var random = new Random(42);
        //    var N = 200;
        //    var F = 20;
        //    var L = 8;

        //    var instances = ToUshort(
        //        Enumerable.Range(0, N).Select(
        //            x => Enumerable.Range(0, F).Select(
        //                y => random.Next(x + 1)).ToArray()).ToArray());

        //    var labels = Enumerable.Range(0, N).Select(x => random.Next(L)).ToArray();

        //    var unlabeled = ToUshort(
        //        Enumerable.Range(0, N).Select(
        //            x => Enumerable.Range(0, F).Select(
        //                y => random.Next(x + 1)).ToArray()).ToArray());

        //    var features = Enumerable.Range(0, F).Select(
        //        x => random.Next(2) == 0 ? RandomForestC.FeatureType.Ordinal : RandomForestC.FeatureType.Categorical).ToArray();

        //    var c = RandomForestC.Classify(features, instances, labels, unlabeled);
        //}

        //[Test]
        public void Classify_nonrandom_data()
        {
            var random = new Random(45);
            var N = 500;
            var F = 20;
            var L = 2;
            //read granule dataset
            //convert values to numeric/nominal format
            //feed to RF input matrix
            // var instances = (string )null;

            double[][] instances = new double[700][];
            sq = "select action_freq,url_freq,email_freq,attach,fsize,class from granule ";
            DataSet ds = new DataSet();
            ds = dbc.fillfn(sq);

            SqlDataReader rd = dbc.readFn(sq);
            int i = 0;
            while ( rd.Read())
            {
              //var   instances = ToUshort(
              //  Enumerable.Range(0, N).Select(
              //      x => Enumerable.Range(0, F).Select(
              //          y => random.Next(x + 1)).ToArray()).ToArray());

            for (int k=0;k<5;k++)
                {
                    Debug.Print(rd[k].ToString());
                    
                    try
                    {
                        instances[i][ k] = Convert.ToDouble(rd[k].ToString());
                    }
                    catch( Exception ex)
                    {
                        instances[i][ k] = 0;
                    }
                }
                i++;
            }

            rd.Close();
            

            var labels = Enumerable.Range(0, i).Select(x => random.Next(L)).ToArray();

            var unlabeled = Convert.ToDouble(
                Enumerable.Range(0, N).Select(
                    x => Enumerable.Range(0, F).Select(
                        y => random.Next(x + 1)).ToArray()).ToArray());

            for ( i = 0; i < labels.Length; i++)
            {
                instances[i][5] = (double )labels[i]; // the column 0 fully explain the labels
            }

            for (i = 0; i < labels.Length; i++)
            {
                unlabeled[i][0] = (double)random.Next(L);
            }

            var features = Enumerable.Range(0, F).Select(
                x => random.Next(2) == 0 ? RandomForestC.FeatureType.Ordinal : RandomForestC.FeatureType.Categorical).ToArray();

           // categorical selection
            features[0] = RandomForestC.FeatureType.Categorical;
            var c = RandomForestC.Classify(features, instances, labels, unlabeled, treeCount: 500);

            //var m = 0;
            //for (int i = 0; i < unlabeled.Length; i++)
            //{
            //    if (c[i][0] == unlabeled[i][0]) m++;
            //}
            //Assert.Greater(m, N * 0.70);
            //Console.WriteLine($"Accuracy: {m / (float)N}");

            //// ordinal selection
            //features[0] = RandomForestC.FeatureType.Ordinal;
            //c = RandomForestC.Classify(features, instances, labels, unlabeled);

            //m = 0;
            //for (int i = 0; i < unlabeled.Length; i++)
            //{
            //    if (c[i][0] == unlabeled[i][0]) m++;
            //}
            //Assert.Greater(m, N * 0.70);
            //Console.WriteLine($"Accuracy: {m / (float)N}");
        }

        private ushort[][] ToUshort(int[][] array)
        {
            return array.Select(a => a.Select(x => (ushort) x).ToArray()).ToArray();
        }
    }
}
