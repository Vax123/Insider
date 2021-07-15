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
using DataTable = System.Data.DataTable;
using com.sun.corba.se.spi.orbutil.threadpool;
using System.IO;
using Microsoft.Office.Interop.Excel;

using weka.gui.treevisualizer;
namespace Insider
{
   
    public partial class Evaluation2 : Form
    {
        DbConnect dbc = new DbConnect();
        SqlDataReader rd;
        SqlDataReader rd1;
        String sq = "";
        
        public Evaluation2()
        {
            InitializeComponent();
        }

        private void Evaluation1_Load(object sender, EventArgs e)
        {
            dbc = new DbConnect();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
          
            dt.Columns.Add("tc_RF");
       
            dt.Columns.Add("tc_RFRWW");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='RF' ";
            SqlDataReader     rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();

                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='RF-RWW' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
         

                dt.Rows.Add(dr);
            }

         
            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "tc_RF";

         
            chart1.Series["RF-RWFF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF-RWFF"].XValueMember = "noofinstances";
            chart1.Series["RF-RWFF"].YValueMembers = "tc_RFRWW";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Time Complexity";
            chart1.DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            this.Hide();
            h.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
          
            dt.Columns.Add("pr_RF");
            dt.Columns.Add("pr_RFRWW");
           


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

             

                sq = "select precision from tb_evaluation where noi=" + noi + " and algname='RF-RWW' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select precision  from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
              

                dt.Rows.Add(dr);
            }

          
            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "pr_RF";

            chart1.Series["RF-RWFF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF-RWFF"].XValueMember = "noofinstances";
            chart1.Series["RF-RWFF"].YValueMembers = "pr_RFRWW";

          



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Precison";
            chart1.DataBind();

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
       
            dt.Columns.Add("ac_RF");
            dt.Columns.Add("ac_RFRWW");
          


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algname='RF-RWW' ";
                SqlDataReader rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();

                sq = "select  accuracy from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
            

                dt.Rows.Add(dr);
            }

         
            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "ac_RF";

            chart1.Series["RF-RWFF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF-RWFF"].XValueMember = "noofinstances";
            chart1.Series["RF-RWFF"].YValueMembers = "ac_RFRWW";
                   


            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "accuracy";
            chart1.DataBind();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
             dt.Columns.Add("re_RF");
            dt.Columns.Add("re_RFRWW");
        

            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

              
                sq = "select recall from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select recall  from tb_evaluation where noi=" + noi + " and algname='RF-RWW' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
               


                dt.Rows.Add(dr);
            }

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "re_RF";

            chart1.Series["RF-RWFF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF-RWFF"].XValueMember = "noofinstances";
            chart1.Series["RF-RWFF"].YValueMembers = "re_RFRWW";

         


            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "recall";
            chart1.DataBind();

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            sq = "select count(predicted) from Rf_Rwff2_results where predicted=1";

            int mx1 = dbc.scalarFn(sq);

            sq = "select count(predicted) from results where predicted=1";

            int mx2 = dbc.scalarFn(sq);


            double[] yValues = new double [2];
            if (mx1 > mx2)
            {
                yValues[0] = mx1;
                yValues[1] = mx2;
            }
            else
            {
                yValues[1] = mx1;
                yValues[0] = mx2;

            }

            string[] xValues = { "RF-RWFF", "RF"};
            chart2.Series["Series1"].Points.DataBindXY(xValues, yValues);

            chart2.Series["Series1"].Points[0].Color = Color.MediumSeaGreen;
            chart2.Series["Series1"].Points[1].Color = Color.PaleGreen;
         //   chart2.Series["Series1"].Points[2].Color = Color.LawnGreen;

            chart2.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            chart2.Series["Series1"]["PieLabelStyle"] = "Disabled";

            chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            chart2.Legends[0].Enabled = true;




        }
    }
}
