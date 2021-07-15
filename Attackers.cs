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
namespace Insider
{

    
    public partial class Attackers : Form
    {

        DbConnect dbc = new DbConnect();
        string  sq = "";

        public Attackers()
        {
            InitializeComponent();
        }

        private void Attackers_Load(object sender, EventArgs e)
        {
            sq = "select ID as MACID from granule where class='Y'";
            DataSet ds = new DataSet();
            ds = dbc.fillfn(sq);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Refresh();

                

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            this.Hide();
            h.Show();


        }
    }
}
