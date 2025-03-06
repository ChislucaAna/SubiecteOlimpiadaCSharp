using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2024
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="otji@csharp.ro" && textBox2.Text== "Ojti2024")
            {
                this.Hide();
                AlegeOptiunea alegeOptiunea = new AlegeOptiunea();
                alegeOptiunea.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Ceva nu a mers\r\nbine, mai încercați!");
            }
        }
    }
}
