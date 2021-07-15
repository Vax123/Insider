using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Remoting.Messaging;


namespace Insider
{
    public partial class DataManagement : Form
    {
        DbConnect dbc = new DbConnect();
        SqlDataReader rd;
        SqlDataAdapter ad;
        DataSet ds;
        String sq = "";
        public DataManagement()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataManagement_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            StreamReader sr = new StreamReader(textBox1.Text);
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
                sq = "drop table "+comboBox1.Text ;
                dbc.execFn(sq);

            }
            catch (Exception ex)
            {

            }

            sq = "create table "+comboBox1.Text +" (";
            string part2 = "";
            string fs = "";
            int j = 0;
            string fs1 = "Slno numeric(10) PRIMARY KEY,";
            textBox3.Text = dt.Columns.Count.ToString();
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
                sq = "insert into "+comboBox1.Text +" values(" + rc.ToString() + ",";
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
                textBox2.Text = rc.ToString();


                try

                {
                    sq = "select * from "+comboBox1.Text ;

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
        

    }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            this.Hide();
            h.Show();
        }
    }

    }


    

   
    


