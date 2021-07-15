using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace Insider
{
    class DbConnect
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter ad;
        SqlDataReader rd;


        public DbConnect()
        {

         //   Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = E:\MyMicrosoftResearch.Cambridge.SherwoodProject\MicrosoftResearch.Cambridge.Sherwood\MicrosoftResearch.Cambridge.Sherwood\db.mdf; Integrated Security = True; Connect Timeout = 30
            //always make sure this path ....in server explorer also
            cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Varsha_Insider\newDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;MultipleActiveResultSets=True");
            cn.Open();

        }




        public double scalarFnD(string sq)
        {

            cmd = new SqlCommand(sq, cn);
            double  mx = 0;

            try
            {
                mx = Convert.ToDouble (cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                mx = 0;

            }
            return (mx);
        }
        public int scalarFn(string sq)
        {

            cmd = new SqlCommand(sq, cn);
            int mx = 0;

            try
            {
                mx = Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                mx = 0;

            }
            return (mx);
        }
        public void execFn(string sq)  // for insert, update and delete
        {

            cmd = new SqlCommand(sq, cn);
            cmd.ExecuteNonQuery();

        }

        public SqlDataReader readFn(string sq) //for reading data and sending back to calling program
        {
            cmd = new SqlCommand(sq, cn);
            rd = cmd.ExecuteReader();
            return (rd);


        }

        public DataSet fillfn(string sq)
        {

            ad = new SqlDataAdapter(sq, cn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "t1");
            return (ds);

        }

    }
}
