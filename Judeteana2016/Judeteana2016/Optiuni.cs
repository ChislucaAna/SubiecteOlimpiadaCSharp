using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2016
{
    public partial class Optiuni : Form
    {
        public Optiuni()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int S = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text);
                if (S < 250)
                    textBox4.Text = "1800";
                else
                    if (S >= 250 && S <= 275)
                    textBox4.Text = "2200";
                else
                    textBox4.Text = "2500";
                Autentificare_client.client_Autentificat.kcal_zilnice=Convert.ToInt32(textBox4.Text);
                //clear db
                StreamWriter streamWriter = new StreamWriter("clienti.txt",false);
                streamWriter.WriteLine("");
                streamWriter.Close();

                //refresh db
                streamWriter = new StreamWriter("clienti.txt");
                foreach (Client c in Form1.clienti)
                {
                    streamWriter.WriteLine(c.ToString());
                    streamWriter.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Datele introduse nu sunt valide");
            }
        }

    }
}
