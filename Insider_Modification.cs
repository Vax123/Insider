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
    public partial class Insider_Modification : Form
    {

        DbConnect dbc = new DbConnect();
        SqlDataReader rd;
        SqlDataAdapter ad;
        DataSet ds;
        String sq = "";
        public Insider_Modification()
        {
            InitializeComponent();
        }

        private void Insider_Modification_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FeatureProcessing r = new FeatureProcessing();
            this.Hide();
            r.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                     
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

        private void button2_Click(object sender, EventArgs e)
        {


            sq = "delete from tbl_fuzzyrel";
            dbc.execFn(sq);

            //writing fuzzy membership 2 degree

            sq = "select * from granule";

            //cmd= new sql command (sq, cn);
            ds = dbc.fillfn(sq);

            for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
            {

                string member1 = ds.Tables[0].Columns[i].ColumnName;
                string member2="";
                if (i+1<ds.Tables[0].Columns.Count)
                {
                  member2 = ds.Tables[0].Columns[i + 1].ColumnName;
                 }

                if (member1 != "" && member2 != "")
                {

                    string sq1 = "select distinct " + member1 + " from granule";

                    DataSet ds1 = dbc.fillfn(sq1);

                    string sq2 = "select distinct " + member2 + " from granule";

                    DataSet ds2 = dbc.fillfn(sq1);

                    double fuzval = (ds1.Tables[0].Rows.Count + ds1.Tables[0].Rows.Count) / 2;
                    double fuzvallog = 1 / fuzval;

                 //   MessageBox.Show(member1 + "," + member2 + ": " + fuzvallog);

                   string  sqnew = "insert into tbl_fuzzyrel values('" + member1 + "','" + member2 + "'," + fuzvallog + ")";
                    dbc.execFn(sqnew);

                }



            }

            sq = "select * from tbl_fuzzyrel";

            //cmd= new sql command (sq, cn);
            ds = dbc.fillfn(sq);

            dataGridView2.DataSource = ds.Tables[0];


        }

        private void button4_Click(object sender, EventArgs e)
        {
           




        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            RF_RWFF2 rf = new RF_RWFF2();
            rf.Show();
        }
    }
}
