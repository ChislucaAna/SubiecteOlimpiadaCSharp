using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2018
{
    public partial class eLearning1918_Elev : Form
    {
        public eLearning1918_Elev()
        {
            InitializeComponent();
        }

        int punctaj = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label1.Text = punctaj.ToString();
        }
    }
}
