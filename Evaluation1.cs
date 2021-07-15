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
   
    public partial class Evaluation1 : Form
    {
        DbConnect dbc = new DbConnect();
        SqlDataReader rd;
        SqlDataReader rd1;
        String sq = "";
        
        public Evaluation1()
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
            dt.Columns.Add("tc_LR");
            dt.Columns.Add("tc_RF");
            dt.Columns.Add("tc_XG");
            dt.Columns.Add("tc_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='LR' ";
            SqlDataReader     rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();

                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='NN' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[3] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algname='XG' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[4] = rd1[0].ToString();


                }
                rd1.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "tc_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "tc_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "tc_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "tc_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Time Complexity";
            chart1.DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Attackers a = new Attackers();
            this.Hide();
            a.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("pr_LR");
            dt.Columns.Add("pr_RF");
            dt.Columns.Add("pr_XG");
            dt.Columns.Add("pr_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select precision  from tb_evaluation where noi=" + noi + " and algname='LR' ";
                SqlDataReader rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();

                sq = "select precision from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select precision  from tb_evaluation where noi=" + noi + " and algname='NN' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[3] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select precision  from tb_evaluation where noi=" + noi + " and algname='XG' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[4] = rd1[0].ToString();


                }
                rd1.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "pr_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "pr_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "pr_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "pr_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Precison";
            chart1.DataBind();

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("ac_LR");
            dt.Columns.Add("ac_RF");
            dt.Columns.Add("ac_XG");
            dt.Columns.Add("ac_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algname='LR' ";
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
                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algname='NN' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[3] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select accuracy from tb_evaluation where noi=" + noi + " and algname='XG' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[4] = rd1[0].ToString();


                }
                rd1.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "ac_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "ac_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "ac_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "ac_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "accuracy";
            chart1.DataBind();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("re_LR");
            dt.Columns.Add("re_RF");
            dt.Columns.Add("re_XG");
            dt.Columns.Add("re_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd = dbc.readFn(sq);
            while (rd.Read())
            {


                DataRow dr = dt.NewRow();
                string noi = rd[0].ToString();

                sq = "select recall  from tb_evaluation where noi=" + noi + " and algname='LR' ";
                SqlDataReader rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[1] = rd1[0].ToString();


                }
                rd1.Close();

                sq = "select recall from tb_evaluation where noi=" + noi + " and algname='RF' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[2] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select recall  from tb_evaluation where noi=" + noi + " and algname='NN' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[3] = rd1[0].ToString();


                }
                rd1.Close();
                sq = "select recall  from tb_evaluation where noi=" + noi + " and algname='XG' ";
                rd1 = dbc.readFn(sq);
                if (rd1.HasRows == true)
                {
                    rd1.Read();

                    dr[0] = noi;
                    dr[4] = rd1[0].ToString();


                }
                rd1.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "re_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "re_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "re_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "re_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "recall";
            chart1.DataBind();

        }
    }
}
