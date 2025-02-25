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

namespace Nationala2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictrueBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader("Useri.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    if (bucati[0] == comboBox1.SelectedItem.ToString())
                    {
                        if (bucati[1].Equals(textBox2.Text))
                        {
                            InterferenteEco interferenteEco = new InterferenteEco((sender as PictureBox).Image);
                            this.Hide();
                            interferenteEco.ShowDialog();
                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("Date de auth invalide");
                            textBox2.Clear();
                        }
                    }
                    comboBox1.Items.Add(bucati[0]);
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());   
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("Useri.txt");
            string line;
            while ((line = reader.ReadLine())!=null)
            {
                string[] bucati =line.Split(' ');
                comboBox1.Items.Add(bucati[0]);
            }
        }
    }
}
