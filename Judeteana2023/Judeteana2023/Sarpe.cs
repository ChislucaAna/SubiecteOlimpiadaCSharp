using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2023
{
    public partial class Sarpe : Form
    {
        public Sarpe()
        {
            InitializeComponent();
        }

        Snake s;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            s = new Snake(pictureBox1.Width, pictureBox1.Height / 2);
        }
    }
}
