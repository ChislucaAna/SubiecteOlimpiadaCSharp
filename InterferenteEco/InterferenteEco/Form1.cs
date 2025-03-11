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

namespace InterferenteEco
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void eval(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;
                StreamReader sr = new StreamReader("Useri.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    if (bucati[0].Equals(comboBox1.SelectedItem.ToString()) && bucati[1].Equals(textBox1.Text))
                    {
                        IntereferenteEco intereferenteEco = new IntereferenteEco((sender as PictureBox).Image);
                        intereferenteEco.ShowDialog();
                        return;
                    }
                }
                MessageBox.Show("Eroare autentificare");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Useri.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] bucati = line.Split(' ');
                comboBox1.Items.Add(bucati[0].ToString());  
            }
        }
    }
}
