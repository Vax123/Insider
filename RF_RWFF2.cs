using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weka.classifiers.functions;
using weka.classifiers.evaluation;
using System.IO;
using System.Data.SqlClient;
using weka.classifiers.functions.neural;
using weka.core;
using weka.gui.treevisualizer;
using weka.classifiers.trees.j48;
namespace Insider
{
    public partial class RF_RWFF2 : Form
    {
        DbConnect dbc = new DbConnect();
        string sq = "";
        SqlDataReader rd;
        DataSet ds = new DataSet();
        public RF_RWFF2()
        {
            InitializeComponent();
        }

  
        void RF()
        {

            sq = "delete from Rf_Rwff2_results";
            dbc.execFn(sq);
            DateTime dt1 = DateTime.Now;
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
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader("D:\\granule_mod.arff"));
            data.setClassIndex(data.numAttributes() - 1);
            int ic = data.numInstances();
         

              weka.classifiers.trees.RandomTree rf = new weka.classifiers.trees .RandomTree();

              // using an unpruned J48

              rf.buildClassifier(data);
             string  mys1= rf.toString();
             textBox1.Text = mys1;
             StreamWriter sw = new StreamWriter ("D:\\mytree.txt",false );

             sw.WriteLine(mys1);
             sw.Close();
          double []p=  rf.getMembershipValues(data.instance(i) );
          java.util.Random r = new java.util.Random(1);
          weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
          eval.crossValidateModel(rf, data, 2, r);
         
          eval.evaluateModel(rf, data);
          double[][] A = eval.confusionMatrix ();

          double tn = A[0][0];
          double fp = A[0][1];
          double fn = A[1][0];
          double tp = A[1][1];
        
          double acc =(tp+tn) / ( tp + fp+tn+fn  );
          double recall = tp / (tp + fn);
          double precision = tp / (tp + fp);
          int  noii = data.numInstances();
        
      
          sq = "delete from tb_Evaluation where algname='RF-RW'";
          dbc.execFn(sq);
          DateTime dt2 = DateTime.Now;
          TimeSpan t = dt2 - dt1;
       //   int tc = signma;
          sq = "insert into tb_Evaluation values(" + noii.ToString() + ",'RF-RWFF',"+t.Milliseconds  +",'" + acc.ToString()+ "','" + precision.ToString()+ "','" + recall.ToString()+ "','Y')";
          dbc.execFn(sq);
     
// meta-classifier
            // train and make predictions
            //sq = "delete from results";
            //dbc.execFn(sq);
            //rf.
            //for (int i = 0; i < data.numInstances(); i++) 
            //{
            //    double pred = j48.classifyInstance(data.instance(i));
            //    string s=  data.instance(i).value(0).ToString();
            //     string p= data.classAttribute().value((int) pred).ToString();

            //}

        //    weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(data);
        //    weka.classifiers.Classifier clsCopy = weka.classifiers.Classifier.makeCopy(rf);
        //    clsCopy.buildClassifier(data);
        //    java.util.Random r = new java.util.Random(1);

        //    eval.crossValidateModel(rf, data, 2, r);

        //    eval.evaluateModel(clsCopy, data);


          i = 0;


          foreach (object o in eval.predictions().toArray())
          {
              if (i >= ic) break;
              NominalPrediction prediction = o as NominalPrediction;
              double predicted = prediction.predicted();
              //   int nominal = rnd.Next(0, 5);
              int p1 = 0;// (int)predicted;
              if (prediction != null)
              {
                  int loopvt = i;
                  string a = data.classAttribute().value((int)data.instance(i).classValue()).ToString();

                  sq = "insert into Rf_Rwff2_results values(" + loopvt + ",'" + a.ToString() + "','" + predicted.ToString() + "')";
                  dbc.execFn(sq);
                  i++;
              }
          }



        ////    lblAlgorithm.Text = "RF";
        //  //  label3.Text = eval.precision(0).ToString();
        //   // label5.Text = eval.recall(0).ToString();
        //   // label7.Text = eval.fMeasure(0).ToString();

        //    //Session["dt_p"] = eval.precision(0);
        //    //Session["dt_f"] = eval.fMeasure(0);

        //    //Session["dt_r"] = eval.recall(0);

        //    sq = "select * from results";
        //    ds = new DataSet();
        //    ds = dbc.fillfn(sq);
        //    dataGridView2.DataSource = ds.Tables["t1"];
        //    dataGridView2.Refresh();
        }

        

        private void RF_RWFF2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RF();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Evaluation2 ev = new Evaluation2();
            this.Hide();
            ev.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            sq = "Delete from prob_calc";
            dbc.execFn(sq);
            StreamReader sr = new StreamReader("d:\\mytree.txt");
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {

                if (line.Contains(":") == true)
                {
                    string[] parts = line.Split(':');

                    listBox1.Items.Add(parts[1]);
                    try
                    {
                        int brack_pos = parts[1].IndexOf("(");
                        string lab = parts[1].Substring(0, brack_pos - 1).Trim();
                        int sl_pos = parts[1].IndexOf("/", brack_pos + 1);
                        string value = parts[1].Substring(brack_pos + 1, sl_pos - brack_pos - 1);

                        sq = "insert into prob_calc values('" + lab + "'," + value + ")";
                        dbc.execFn(sq);
                    }
                    catch (Exception ex)
                    {


                    }
                }

            }





            sr.Close();


            sq = "select * from  prob_calc";
            ds = dbc.fillfn(sq);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();


            //equation=   pblty( pos)  * weight *( errorate)------------   6/27* 1/7  * 0.01
            //pblty(neg) * weight * errorate -----------------  21/27 * 1/7 * 0.01


            double size = Convert.ToDouble(listBox1.Items[listBox1.Items.Count - 1].ToString().Trim());
            double weight = 1 / size;
            double errorate = (1 / weight);

            sq = "select sum(value) from prob_Calc where tclass='Y'";
            double sum1 = dbc.scalarFnD(sq);

            sq = "select sum(value) from prob_Calc where tclass='N'";
            double sum2 = dbc.scalarFnD(sq);

            double prob1 = sum1 / (sum1 + sum2);
            double prob2 = sum2 / (sum1 + sum2);

            double rfweight = prob1 * weight * errorate;
            double rfweight2 = prob2 * weight * errorate;





            if (rfweight2 > rfweight)
            {

                sq = "insert into tbl_Final_results values(" + rfweight + "," + rfweight2 + ",' Chance of Insider Attack')";
                dbc.execFn(sq);
                MessageBox.Show("Alert!!! Chance of Insider Attack", "Attension", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                sq = "insert into tbl_Final_results values('" + rfweight + "," + rfweight2 + ",' No Insider Attacks Detected')";
                dbc.execFn(sq);
            }

            sq = "select * from Rf_Rwff2_results";
            ds = dbc.fillfn(sq);

            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Refresh();

           

        
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
