using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_4476_
{
    public partial class Form2: Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        


        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            progress.Value = startpoint;
            if(progress.Value == 100)
            {
                progress.Value = 0;
                timer1.Stop();

                Form1 log = new Form1();

                //ProduitForm log = new ProduitForm();

                this.Hide();
                log.Show();
            } 
            
            

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
