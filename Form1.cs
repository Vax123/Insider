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
using System.Threading;

namespace Insider
{
    public partial class Form1 : Form
    {
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Home h = new Home();
            //this.Hide();
            //h.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            i++;
            if (i == 3)
            {
                Home h = new Home();

                this.Hide();
                timer1.Stop();
                h.Show();
            }
        }
        }
    }
