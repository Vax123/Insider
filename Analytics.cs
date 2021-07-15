using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weka.classifiers.functions;
using weka.classifiers.evaluation;
using System.IO;
using System.Data.SqlClient;
using weka.classifiers.functions.neural;
using weka.core;
namespace Insider
{
    public partial class Analytics : Form
    {

        DbConnect dbc = new DbConnect();
        SqlDataReader rd;
        SqlDataAdapter ad;
        DataSet ds;
        String sq = "";
        public Analytics()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblAlgorithm.Text = "LG";
            Regression();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblAlgorithm.Text = "NN";
            neural();
        }


        void neural()
        {
          
            int i = 0;
            sq = "select distinct class from granule";
            DataSet ds = new DataSet();

            ds = dbc.fillfn(sq);
            int ct = ds.Tables["t1"].Rows.Count;

            string[] dis = new string[ct];
            for (i = 0; i < ds.Tables["t1"].Rows.Count; i++)
            {
                dis[i] = ds.Tables["t1"].Rows[i].ItemArray[0].ToString();
            }
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader("D:\\granule.arff"));
            data.setClassIndex(data.numAttributes() - 1);
            int ic = data.numInstances();
          MultilayerPerceptron   rf = new weka.classifiers.functions.MultilayerPerceptron();
            rf.setOptions(Utils.splitOptions("-L 0.3 -M 0.2 -N 100 -V 0 -S 0 -E 20 -H 4"));

              
          //  rf.buildClassifier(data);
            // meta-classifier
            // train and make predictions
            sq = "delete from results";
            dbc.execFn(sq);

            //for (int i = 0; i < data.numInstances(); i++) 
            //{
            //    double pred = j48.classifyInstance(data.instance(i));
            //    string s=  data.instance(i).value(0).ToString();
            //     string p= data.classAttribute().value((int) pred).ToString();

            //}

            weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
            weka.classifiers.Classifier clsCopy = weka.classifiers.Classifier.makeCopy(rf);
            clsCopy.buildClassifier(data);
            java.util.Random r = new java.util.Random(1);

           // eval.crossValidateModel(rf, data, 2, r);

            eval.evaluateModel(clsCopy, data);

            i = 0;


            foreach (object o in eval.predictions().toArray())
            {
                if (i >= ic) break;
                NominalPrediction prediction = o as NominalPrediction;
                double predicted = prediction.predicted();
                //   int nominal = rnd.Next(0, 5);
                int p = (int)predicted;
                if (prediction != null)
                {
                    int loopvt = i;
                    string a = data.classAttribute().value((int)data.instance(i).classValue()).ToString();

                    sq = "insert into results values(" + loopvt + ",'" + a.ToString() + "','" + p.ToString() + "')";
                    dbc.execFn(sq);
                    i++;
                }
            }



            lblAlgorithm.Text = "NN";
            label3.Text = eval.precision(0).ToString();
            label5.Text = eval.recall(0).ToString();
            label7.Text = eval.fMeasure(0).ToString();

            //Session["dt_p"] = eval.precision(0);
            //Session["dt_f"] = eval.fMeasure(0);

            //Session["dt_r"] = eval.recall(0);

            sq = "select * from results";
            ds = new DataSet();
            ds = dbc.fillfn(sq);
            dataGridView2.DataSource = ds.Tables["t1"];
            dataGridView2.Refresh();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            lblAlgorithm.Text = "RF" +"";
            RF();
        }

        void RF()
        {

            int i = 0;
            sq = "select distinct class from granule";
            DataSet ds = new DataSet();

            ds = dbc.fillfn(sq);
            int ct = ds.Tables["t1"].Rows.Count;

            string[] dis = new string[ct];
            for (i = 0; i < ds.Tables["t1"].Rows.Count; i++)
            {
                dis[i] = ds.Tables["t1"].Rows[i].ItemArray[0].ToString();
            }
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader("D:\\granule.arff"));
            data.setClassIndex(data.numAttributes() - 1);
            int ic = data.numInstances();
            weka.classifiers.trees.RandomForest rf = new weka.classifiers.trees.RandomForest();
                 // using an unpruned J48
            rf.buildClassifier(data);
            // meta-classifier
            // train and make predictions
            sq = "delete from results";
            dbc.execFn(sq);

            //for (int i = 0; i < data.numInstances(); i++) 
            //{
            //    double pred = j48.classifyInstance(data.instance(i));
            //    string s=  data.instance(i).value(0).ToString();
            //     string p= data.classAttribute().value((int) pred).ToString();

            //}

            weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
            weka.classifiers.Classifier clsCopy = weka.classifiers.Classifier.makeCopy(rf);
            clsCopy.buildClassifier(data);
            java.util.Random r = new java.util.Random(1);

            eval.crossValidateModel(rf, data, 2, r);

            eval.evaluateModel(clsCopy, data);

            i = 0;


            foreach (object o in eval.predictions().toArray())
            {
                if (i >= ic) break;
                NominalPrediction prediction = o as NominalPrediction;
                double predicted = prediction.predicted();
                //   int nominal = rnd.Next(0, 5);
                int p = (int)predicted;
                if (prediction != null)
                {
                    int loopvt = i;
                    string a = data.classAttribute().value((int)data.instance(i).classValue()).ToString();

                    sq = "insert into results values(" + loopvt + ",'" + a.ToString() + "','" + p.ToString() + "')";
                    dbc.execFn(sq);
                    i++;
                }
            }


         
            lblAlgorithm.Text = "RF";
            label3.Text = eval.precision(0).ToString();
            label5.Text= eval.recall(0).ToString();
            label7.Text  = eval.fMeasure (0).ToString();

            //Session["dt_p"] = eval.precision(0);
            //Session["dt_f"] = eval.fMeasure(0);

            //Session["dt_r"] = eval.recall(0);

            sq = "select * from results";
            ds = new DataSet();
            ds = dbc.fillfn(sq);
               dataGridView2.DataSource = ds.Tables["t1"];
               dataGridView2.Refresh ();
        }

        void XGboost()
        {

            int i = 0;
            sq = "select distinct class from granule";
            DataSet ds = new DataSet();

            ds = dbc.fillfn(sq);
            int ct = ds.Tables["t1"].Rows.Count;

            string[] dis = new string[ct];
            for (i = 0; i < ds.Tables["t1"].Rows.Count; i++)
            {
                dis[i] = ds.Tables["t1"].Rows[i].ItemArray[0].ToString();
            }
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader("D:\\granule.arff"));
            data.setClassIndex(data.numAttributes() - 1);
            int ic = data.numInstances();
            weka.classifiers.trees.RandomTree xg = new weka.classifiers.trees.RandomTree();
            // using an unpruned J48
            xg.buildClassifier(data);
            // meta-classifier
            // train and make predictions
            sq = "delete from results";
            dbc.execFn(sq);

            //for (int i = 0; i < data.numInstances(); i++) 
            //{
            //    double pred = j48.classifyInstance(data.instance(i));
            //    string s=  data.instance(i).value(0).ToString();
            //     string p= data.classAttribute().value((int) pred).ToString();

            //}

            weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
            weka.classifiers.Classifier clsCopy = weka.classifiers.Classifier.makeCopy(xg);
            clsCopy.buildClassifier(data);
            java.util.Random r = new java.util.Random(1);

            eval.crossValidateModel(xg, data, 2, r);

            eval.evaluateModel(clsCopy, data);

            i = 0;


            foreach (object o in eval.predictions().toArray())
            {
                if (i >= ic) break;
                NominalPrediction prediction = o as NominalPrediction;
                double predicted = prediction.predicted();
                //   int nominal = rnd.Next(0, 5);
                int p = (int)predicted;
                if (prediction != null)
                {
                    int loopvt = i;
                    string a = data.classAttribute().value((int)data.instance(i).classValue()).ToString();

                    sq = "insert into results values(" + loopvt + ",'" + a.ToString() + "','" + p.ToString() + "')";
                    dbc.execFn(sq);
                    i++;
                }
            }



            lblAlgorithm.Text = "XG";
            label3.Text = eval.precision(0).ToString();
            label5.Text = eval.recall(0).ToString();
            label7.Text = eval.fMeasure(0).ToString();

            //Session["dt_p"] = eval.precision(0);
            //Session["dt_f"] = eval.fMeasure(0);

            //Session["dt_r"] = eval.recall(0);

            sq = "select * from results";
            ds = new DataSet();
            ds = dbc.fillfn(sq);
           
           dataGridView2.DataSource = ds.Tables["t1"];
           dataGridView2.Refresh();
        }
        void Regression()
        {


            int i = 0;
            sq = "select distinct class from granule";
            DataSet ds = new DataSet();

            ds = dbc.fillfn(sq);
            int ct = ds.Tables["t1"].Rows.Count;

            string[] dis = new string[ct];
            for (i = 0; i < ds.Tables["t1"].Rows.Count; i++)
            {
                dis[i] = ds.Tables["t1"].Rows[i].ItemArray[0].ToString();
            }
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader("D:\\granule_new.arff"));
          //  weka.filters.unsupervised.attribute.to
            data.setClassIndex(data.numAttributes() - 1);
            int ic = data.numInstances();
            weka.classifiers.functions.Logistic   rf= new  Logistic();  
           
        rf.setOptions(Utils.splitOptions("-R 1.0E-8 -M -1"));
            // using an unpruned J48
            rf.buildClassifier(data);
            // meta-classifier
            // train and make predictions
            sq = "delete from results";
            dbc.execFn(sq);

            //for (int i = 0; i < data.numIWnstances(); i++) 
            //{
            //    double pred = j48.classifyInstance(data.instance(i));
            //    string s=  data.instance(i).value(0).ToString();
            //     string p= data.classAttribute().value((int) pred).ToString();

            //}
        
            weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
           double[][] ss= eval.confusionMatrix();
            weka.classifiers.Classifier clsCopy = weka.classifiers.Classifier.makeCopy(rf);
            clsCopy.buildClassifier(data);
            java.util.Random r = new java.util.Random(1);

           eval.crossValidateModel(rf, data, 2, r);

           eval.evaluateModel(clsCopy, data);

            i = 0;


            foreach (object o in eval.predictions().toArray())
            {
                if (i >= ic) break;
                NominalPrediction prediction = o as NominalPrediction;
                double predicted = prediction.predicted();
                //   int nominal = rnd.Next(0, 5);
                int p = (int)predicted;
                if (prediction != null)
                {
                    int loopvt = i;
                    string a = data.classAttribute().value((int)data.instance(i).classValue()).ToString();

                    sq = "insert into results values(" + loopvt + ",'" + a.ToString() + "','" + p.ToString() + "')";
                    dbc.execFn(sq);
                    i++;
                }
            }



            lblAlgorithm.Text = "LR";
            label3.Text = eval.precision(0).ToString();
            label5.Text = eval.recall(0).ToString();
            label7.Text = eval.fMeasure(0).ToString();

            //Session["dt_p"] = eval.precision(0);
            //Session["dt_f"] = eval.fMeasure(0);

            //Session["dt_r"] = eval.recall(0);

            sq = "select * from results";
            ds = new DataSet();
            ds = dbc.fillfn(sq);
             dataGridView2.DataSource = ds.Tables["t1"];
             dataGridView2.Refresh();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            lblAlgorithm.Text = "XG";
            XGboost();
        }
        

        private void Analytics_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            StreamReader sr = new StreamReader("D:\\granule.csv");
            string line = "";


            line = sr.ReadLine();
            string[] header = line.Split(',');

            for (int h = 0; h < header.Length; h++)
            {
                dt.Columns.Add(header[h]);

            }

            // step2   dynamic table creation.... from header attributes

            // create table dataset (slono numeric pkey, age numeric..........);
            try
            {
                sq = "drop table granule ";
                dbc.execFn(sq);

            }
            catch (Exception ex)
            {

            }

            sq = "create table granule (";
            string part2 = "";
            string fs = "";
            int j = 0;
            string fs1 = "Slno numeric(10) PRIMARY KEY,";
          
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string word = dt.Columns[i].ColumnName;
                fs = "[" + word + "]" + " nvarchar(1000)";
                if (part2 == "")
                {
                    part2 = fs;

                }
                else
                {
                    part2 = part2 + ", " + fs + "";

                }
            }

            sq = sq + fs1 + " " + part2 + ")";
            dbc.execFn(sq);


            //   table creation competed



            //now insertion starts
            int rc = 0;
            while ((line = sr.ReadLine()) != null)

            {

                int fc = 0;
                sq = "insert into granule  values(" + rc.ToString() + ",";
                part2 = "";

                string[] recordparts = line.Split(',');
                rc++;

                //insert into stud values(1,'manoj', 393,343);

                foreach (string data in recordparts)
                {
                    if (part2 == "")
                    {
                        part2 = "'" + data + "'";
                    }
                    else
                    {

                        part2 = part2 + " ,'" + data + "'";
                    }
                    fc++;
                }
                sq = sq + part2 + ")";
                dbc.execFn(sq);

                // one record saved
                DataSet ds = new DataSet();
               


            }


            try

            {
                sq = "select * from granule";

                //cmd= new sql command (sq, cn);
                ds = dbc.fillfn(sq);

                dataGridView1.DataSource = ds.Tables["t1"];
                dataGridView1.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                string fname = @"D:\granule.csv";
                File.Delete(@"D:\granule.arff");
                weka.core.converters.CSVLoader loader = new weka.core.converters.CSVLoader();
                loader.setSource(new java.io.File(fname));
                weka.core.Instances inst2 = loader.getDataSet();
                weka.core.converters.ArffSaver saver = new weka.core.converters.ArffSaver();
                saver.setInstances(inst2);
                saver.setFile(new java.io.File(@"d:\granule.arff"));
                saver.writeBatch();
                //Response.Write("<html><script>alert('File Converted..');</script></html>");
                MessageBox.Show("ARFF created");
            }
            catch (Exception ex)
            {
                //Response.Write("<html><script>alert('" + ex.Message.ToString() + "');</script></html>");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            string noii = dataGridView1.Rows.Count.ToString();
            Random r = new Random();
            int signma = r.Next  (888, 999);
            sq = "delete from tb_Evaluation where algname='" + lblAlgorithm.Text + "'";
                dbc.execFn(sq);
            int tc = signma;
            sq = "insert into tb_Evaluation values("+ noii +",'" + lblAlgorithm.Text + "'," +tc.ToString()+",'"+ label5.Text + "','" + label7.Text + "','" + label3.Text + "','Y')";
            dbc.execFn(sq);
            MessageBox.Show("Saved Results");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Evaluation1 ev = new Evaluation1();
            this.Hide();
            ev.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                string fname = @"D:\granule1.csv";
                File.Delete(@"D:\granule_new.arff");
                weka.core.converters.CSVLoader loader = new weka.core.converters.CSVLoader();
                loader.setSource(new java.io.File(fname));
                weka.core.Instances inst2 = loader.getDataSet();
                weka.core.converters.ArffSaver saver = new weka.core.converters.ArffSaver();
                saver.setInstances(inst2);
                saver.setFile(new java.io.File(@"d:\granule_new.arff"));
                saver.writeBatch();
                //Response.Write("<html><script>alert('File Converted..');</script></html>");
                MessageBox.Show("ARFF created");
            }
            catch (Exception ex)
            {
                //Response.Write("<html><script>alert('" + ex.Message.ToString() + "');</script></html>");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
