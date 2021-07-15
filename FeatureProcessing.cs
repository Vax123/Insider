using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using weka.classifiers.functions.neural;
using weka.core;
using weka.classifiers.functions;
using weka.classifiers.evaluation;
using System.IO;
using MicrosoftResearch.Cambridge.Sherwood;
namespace Insider
{
    public partial class FeatureProcessing : Form
    {
        DbConnect dbc = new DbConnect();
        string sq = "";
        SqlDataReader rd;
        public FeatureProcessing()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

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



          //  lblAlgorithm.Text = "RF";
          //  label3.Text = eval.precision(0).ToString();
           // label5.Text = eval.recall(0).ToString();
          ///  label7.Text = eval.fMeasure(0).ToString();

            //Session["dt_p"] = eval.precision(0);
            //Session["dt_f"] = eval.fMeasure(0);

            //Session["dt_r"] = eval.recall(0);

            sq = "select * from results";
            ds = new DataSet();
            ds = dbc.fillfn(sq);
            dataGridView2.DataSource = ds.Tables["t1"];
            dataGridView2.Refresh();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           //read dataset with fuzzy features

            //initoalise random part

            //build trees 
            //do RWMA instead of majorityvoting

           //select most weighted tree for prediction- use that class lanels

            //print the class labels with macid

            RF_RWFF2 r = new RF_RWFF2();
            this.Hide();
            r.Show();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sq = "select distinct member1 from tbl_fuzzyrel where fuzzy_val>" + _txtthreshold.Text;
            rd = dbc.readFn(sq);
            if (rd.HasRows == true)
            {

                while (rd.Read())
                {

                    listBox1.Items.Add(rd[0].ToString());

                }



            }
            rd.Close();
             sq = "select distinct member2 from tbl_fuzzyrel where  fuzzy_val>" + _txtthreshold.Text;
            rd = dbc.readFn(sq);
            if (rd.HasRows == true)
            {

                while (rd.Read())
                {
                  
                  if( listBox1.Items.Contains ( rd[0].ToString())==false )
                    listBox1.Items.Add(rd[0].ToString());

                }



            }
            rd.Close();


            sq="select * from tbl_fuzzyrel where fuzzy_val>"+_txtthreshold.Text;
            DataSet ds = new DataSet();
            ds = dbc.fillfn(sq);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();

        }

        private void FeatureProcessing_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {




         

            string filter = "";
            for (int i = 0; i < listBox1.Items.Count ; i++)
            {
                if (filter == "")
                {

                    filter = listBox1.Items[i].ToString();
                }
                else
                {
                    filter = filter + "," + listBox1.Items[i].ToString(); ;

                }


            }

            sq = "select " + filter + "  from granule";



            DataTable dt = new DataTable();

            StreamReader sr = new StreamReader("D:\\granule.csv");
            string line = "";


            line = sr.ReadLine();
            string[] header = filter.Split(',');

            for (int h = 0; h < header.Length; h++)
            {
                dt.Columns.Add(header[h]);

            }

            // step2   dynamic table creation.... from header attributes

            // create table dataset (slono numeric pkey, age numeric..........);
            try
            {
                sq = "drop table granule_new ";
                dbc.execFn(sq);
                 
            }
            catch (Exception ex)
            {

            }

            sq = "create table granule_new (";
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

            StreamWriter sw = new StreamWriter("d:\\granule_mod.csv");
            //   table creation competed
            sq = "select " + filter + " from  granule";
            DataSet ds= new DataSet ();
            ds = dbc.fillfn(sq);
            dt = ds.Tables[0];

            sw.WriteLine("slno,"+filter);
            //now insertion starts
            int rc = 0;
           
          for( int k=0;k< dt.Rows.Count;k++)
          { 
              string fesh = "";

                int fc = 0;
                sq = "insert into granule_new  values(" + rc.ToString() + ",";
                part2 = "";

                string[] recordparts = line.Split(',');
                rc++;

                //insert into stud values(1,'manoj', 393,343);

                foreach (string data in dt.Rows[k].ItemArray )
                {
                    if (part2 == "")
                    {
                        part2 = "'" + data + "'";
                        fesh = data;
                    }
                    else
                    {

                        part2 = part2 + " ,'" + data + "'";
                        if (data == "1")
                        {
                            fesh = fesh + ",Y";

                        }
                        else if (data == "0")
                        {
                            fesh = fesh + ",N";

                        }
                        else
                        {
                            fesh = fesh + "," + data;
                        }
                    }
                    fc++;
                }
                sq = sq + part2 + ")";
                dbc.execFn(sq);
                sw.WriteLine(rc.ToString() + ","+fesh );
                fesh = "";
                // one record saved
              


            }


          sw.Close();
            try
            {
                sq = "select * from granule_new";

                //cmd= new sql command (sq, cn);
               ds = dbc.fillfn(sq);

                dataGridView2.DataSource = ds.Tables["t1"];
                dataGridView2.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                string fname = @"D:\granule_mod.csv";
                File.Delete(@"D:\granule_mod.arff");
                weka.core.converters.CSVLoader loader = new weka.core.converters.CSVLoader();
                loader.setSource(new java.io.File(fname));
                weka.core.Instances inst2 = loader.getDataSet();
                weka.core.converters.ArffSaver saver = new weka.core.converters.ArffSaver();
                saver.setInstances(inst2);
                saver.setFile(new java.io.File(@"d:\granule_mod.arff"));
                saver.writeBatch();
                //Response.Write("<html><script>alert('File Converted..');</script></html>");
                MessageBox.Show("ARFF created");
            }
            catch (Exception ex)
            {
                //Response.Write("<html><script>alert('" + ex.Message.ToString() + "');</script></html>");
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void _txtthreshold_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
