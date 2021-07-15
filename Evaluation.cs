using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;

using weka.gui.treevisualizer;

namespace Insider
{
    public partial class Evaluation : Form
    {
        DbConnect dbc= new DbConnect();
        SqlDataReader rd;
        SqlDataReader rd1;
        String sq = "";
        public Evaluation()
        {
            InitializeComponent();
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
            rd1 = dbc.readFn(sq);
            while (rd1.Read())
            {


                DataRow dr = dt.NewRow();
                //string noi = [rd1].ToString();
                string noi = rd1["noi"].ToString();
                
                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algorithm='LR' ";
                rd = dbc.readFn (sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[1] = rd[0].ToString();


                }
                rd.Close();

                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algorithm='RF' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[2] = rd[0].ToString();


                }
                rd.Close();
                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algorithm='NN' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[3] = rd[0].ToString();


                }
                rd.Close();
                sq = "select time_complex  from tb_evaluation where noi=" + noi + " and algorithm='XG' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[4] = rd[0].ToString();


                }
                rd.Close();


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

        private void Evaluation_Load(object sender, EventArgs e)
        {
            dbc = new DbConnect();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("precision_LR");
            dt.Columns.Add("precision_RF");
            dt.Columns.Add("precision_XG");
            dt.Columns.Add("precision_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd1 = dbc.readFn(sq);
            while (rd1.Read())
            {


                DataRow dr = dt.NewRow();
                //string noi = [rd1].ToString();
                string noi = rd1["noi"].ToString();

                sq = "select precision  from tb_evaluation where noi=" + noi + " and algorithm='LR' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[1] = rd[0].ToString();


                }
                rd.Close();

                sq = "select precision  from tb_evaluation where noi=" + noi + " and algorithm='RF' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[2] = rd[0].ToString();


                }
                rd.Close();
                sq = "select precision  from tb_evaluation where noi=" + noi + " and algorithm='NN' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[3] = rd[0].ToString();


                }
                rd.Close();
                sq = "select precision  from tb_evaluation where noi=" + noi + " and algorithm='XG' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[4] = rd[0].ToString();


                }
                rd.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "precision_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "precision_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "precision_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "precision_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Precision";
            chart1.DataBind();


        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("accuracy_LR");
            dt.Columns.Add("accuracy_RF");
            dt.Columns.Add("accuracy_XG");
            dt.Columns.Add("accuracy_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd1 = dbc.readFn(sq);
            while (rd1.Read())
            {


                DataRow dr = dt.NewRow();
                //string noi = [rd1].ToString();
                string noi = rd1["noi"].ToString();

                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algorithm='LR' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[1] = rd[0].ToString();


                }
                rd.Close();

                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algorithm='RF' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[2] = rd[0].ToString();


                }
                rd.Close();
                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algorithm='NN' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[3] = rd[0].ToString();


                }
                rd.Close();
                sq = "select accuracy  from tb_evaluation where noi=" + noi + " and algorithm='XG' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[4] = rd[0].ToString();


                }
                rd.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "accuracy_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "accuracy_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "accuracy_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "accuracy_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "accuracy";
            chart1.DataBind();


        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("noofinstances");
            dt.Columns.Add("recall_LR");
            dt.Columns.Add("recall_RF");
            dt.Columns.Add("recall_XG");
            dt.Columns.Add("recall_NN");


            sq = "select distinct noi from tb_evaluation  order by noi ";
            rd1 = dbc.readFn(sq);
            while (rd1.Read())
            {


                DataRow dr = dt.NewRow();
                //string noi = [rd1].ToString();
                string noi = rd1["noi"].ToString();

                sq = "select recall  from tb_evaluation where noi=" + noi + " and algorithm='LR' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[1] = rd[0].ToString();


                }
                rd.Close();

                sq = "select recall  from tb_evaluation where noi=" + noi + " and algorithm='RF' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[2] = rd[0].ToString();


                }
                rd.Close();
                sq = "select recall  from tb_evaluation where noi=" + noi + " and algorithm='NN' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[3] = rd[0].ToString();


                }
                rd.Close();
                sq = "select recall  from tb_evaluation where noi=" + noi + " and algorithm='XG' ";
                rd = dbc.readFn(sq);
                if (rd.HasRows == true)
                {
                    rd.Read();

                    dr[0] = noi;
                    dr[4] = rd[0].ToString();


                }
                rd.Close();


                dt.Rows.Add(dr);
            }

            chart1.Series["LR"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["LR"].XValueMember = "noofinstances";
            chart1.Series["LR"].YValueMembers = "recall_LR";

            chart1.Series["RF"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["RF"].XValueMember = "noofinstances";
            chart1.Series["RF"].YValueMembers = "recall_RF";

            chart1.Series["XG"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["XG"].XValueMember = "noofinstances";
            chart1.Series["XG"].YValueMembers = "recall_XG";

            chart1.Series["NN"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series["NN"].XValueMember = "noofinstances";
            chart1.Series["NN"].YValueMembers = "recall_NN";



            chart1.DataSource = dt;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Number of Instance";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "recall";
            chart1.DataBind();


        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attackers at = new Attackers();
            at.Show();
        }
    }
}
