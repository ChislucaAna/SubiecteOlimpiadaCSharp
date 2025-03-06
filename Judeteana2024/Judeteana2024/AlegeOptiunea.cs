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
    public partial class AlegeOptiunea : Form
    {
        public AlegeOptiunea()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.Hide();
                MiscarePamantLuna miscarePamantLuna = new MiscarePamantLuna();
                miscarePamantLuna.ShowDialog();
                this.Show();
            }
            else
            {
                if (radioButton2.Checked)
                {
                    this.Hide();
                    SpaceWar spaceWar = new SpaceWar();
                    spaceWar.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Selectati un joc inainte de a apasa start");
                }
            }
        }
    }
}
