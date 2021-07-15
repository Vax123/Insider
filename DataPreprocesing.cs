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
namespace Insider
{
    public partial class DataPreprocess : Form
    {

        DbConnect dbc = new DbConnect();
        string sq = "";
        SqlDataReader rd;
        DataSet ds = new DataSet();
        public DataPreprocess()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbDataSet5.connect' table. You can move, or remove it, as needed.
            this.connectTableAdapter1.Fill(this.dbDataSet5.connect);
            // TODO: This line of code loads data into the 'dbDataSet4.login' table. You can move, or remove it, as needed.
            this.loginTableAdapter1.Fill(this.dbDataSet4.login);
            // TODO: This line of code loads data into the 'dbDataSet3.Email' table. You can move, or remove it, as needed.
            //  this.emailTableAdapter.Fill(this.dbDataSet3.Email);
            // TODO: This line of code loads data into the 'dbDataSet2.http' table. You can move, or remove it, as needed.
            this.httpTableAdapter.Fill(this.dbDataSet2.http);
            // TODO: This line of code loads data into the 'dbDataSet1.login' table. You can move, or remove it, as needed.
            this.loginTableAdapter.Fill(this.dbDataSet1.login);
            // TODO: This line of code loads data into the 'dbDataSet.connect' table. You can move, or remove it, as needed.
       //     this.connectTableAdapter.Fill(this.dbDataSet.connect);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView5.Rows.Clear();
            StreamWriter sw = new StreamWriter("D:\\granule.csv", false);
            sw.WriteLine("Id,Time,Action_Freq,Url_Freq,Email_Freq,Attach,FSize,Class");
            sq = "select count(activity) from login";
            int actvcount = dbc.scalarFn(sq);

            sq = "select count(url) from http";
            int allurl = dbc.scalarFn(sq);

            sq = "select count(content) from email";
            int allmail = dbc.scalarFn(sq);

          //  sq = "select count(CAST(attachments AS decimal(10, 2))) from email";
            sq = "select count(attachments) from email";
            
            double  allattach = dbc.scalarFnD(sq);

            sq = "Select distinct id from login ";
            rd = dbc.readFn(sq);
            if (rd.HasRows == true)
            {



                while ( rd.Read())
                {
                    //find time used 

                    int f = 0;
                    sq = "select top 1 date from login where id='" + rd["id"].ToString() + "' and activity='Logon' order by date";
                    SqlDataReader  rd1 = dbc.readFn(sq);
                    if (rd1.HasRows == true )
                    {
                    rd1.Read();
                        
                    }
                    else
                        f = 1;
                    
                    sq = "select top 1 date  from login where id='" + rd["id"].ToString() + "'and activity='Logoff' order by date desc";
                    SqlDataReader rd2 = dbc.readFn(sq);
                    if (rd2.HasRows == true )
                    {
                        rd2.Read();
                    }
                        
                    else
                        f = 1;
                    Random r = new Random();
                    int tms = 0;
                    if (f == 0)
                    {
                        DateTime st = Convert.ToDateTime(rd1[0].ToString());
                        DateTime et = Convert.ToDateTime(rd2[0].ToString());
                        TimeSpan t = et - st;
                        tms= (int) t.TotalMinutes;
                    }
                    else
                    {
                        tms = r.Next(100, 1000);

                    }
                    dataGridView5.Rows.Add();
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[0].Value = rd["id"].ToString();

                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[1].Value = tms.ToString();

                    // find number of actions

                    sq = "select count(activity) from login where  id='" + rd["id"].ToString() + "' ";
                    int thiscount = dbc.scalarFn(sq);
                    
                    double activity_per =(double ) thiscount / actvcount;
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[2].Value = activity_per.ToString();
                    // number of sites visited
                    sq = "select count(url) from http where  id='" + rd["id"].ToString() + "' ";
                    int thisurl = dbc.scalarFn(sq);

                    double url_per = (double)thisurl / allurl;
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[3].Value = url_per.ToString();
                

                    //number of emails sent
                    
                    sq = "select count(content) from email where  id='" + rd["id"].ToString() + "' ";
                    int thismail = dbc.scalarFn(sq);

                    double email_per = (double)thisurl / allmail;
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[4].Value = email_per.ToString();

                    //number of file access


                    //number of  attachments
                    sq = "select sum(CAST(attachments AS decimal(10, 2))) from email where  id='" + rd["id"].ToString() + "' ";
                    int thisattach = dbc.scalarFn(sq);

                    double attach_per = (double)thisattach  / allattach;
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[5].Value = attach_per.ToString();


                    //filesize

                    int fover = 0;
                    sq = "select * from email where  id='" + rd["id"].ToString() + "' and CAST(size AS decimal(10, 2))>30000 ";
                    SqlDataReader rd4 = dbc.readFn(sq);
                    if (rd4.HasRows == true)
                    {
                        rd4.Read();
                        fover = 1;
                    }
                    else
                    {
                        fover = 0;

                    }
                    //number of words in content

                    
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[6].Value = fover.ToString();
                    
                    Random r1 = new Random();
                    string classlabel = "";

                    if (tms > 500 && email_per > 0.0025 && url_per > 0.002 && attach_per > 0.0025)
                    {
                        classlabel  = "Y";

                    }
                    else
                    {
                        classlabel = "N";

                    }
                    dataGridView5.Rows[dataGridView5.Rows.Count - 2].Cells[7].Value = classlabel.ToString();

                    sw.WriteLine( rd["id"].ToString()+","+tms+","+activity_per +","+url_per +","+ email_per +","+attach_per +","+fover +","+ classlabel );

                }

            }
            sw.Close();
//            for( int i=0;i< ds.Tables[0].Rows.Count;i++)
//            {


//                string slno = i.ToString();
//                string macid = ds.Tables[0].Rows[i].ItemArray[0].ToString();
//                string  pc= ds.Tables[0].Rows[i].ItemArray[1].ToString();
//                string  user= ds.Tables[0].Rows[i].ItemArray[2].ToString();
//                string date= ds.Tables[0].Rows[i].ItemArray[3].ToString(); ;
//                string activity= ds.Tables[0].Rows[i].ItemArray[4].ToString();
//                string from = ds.Tables[0].Rows[i].ItemArray[5].ToString();
//                string attach = ds.Tables[0].Rows[i].ItemArray[6].ToString();
//                string ccontent= ds.Tables[0].Rows[i].ItemArray[7].ToString();
//                string action= ds.Tables[0].Rows[i].ItemArray[8].ToString();
//                string url = ds.Tables[0].Rows[i].ItemArray[9].ToString();

//                ///The models consist of auxiliary information related to each
//                //  user, such as assigned machines, relationships with other users,
//                //  roles, work hours, permitted access and so on.


//                //find userid 
//                //find time duration
//                //number of actions performed.


//                //Frequency features, which are the count of different types
//            //    of actions the user performed in the aggregation period,
////e.g.number of emails sent, number of file accesses after
////work hour, or number of websites visited on a shared PC


//                    //email statitical features
//             //   attachment sizes, file sizes, and the number of words in
//                 //websites visited.


//            }



        }

        private void emailBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            this.Hide();
            h.Show();
        }
    }
}
