using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Drawing;
using System.Diagnostics;
using System.IO;
namespace Nationala2024
{
    public partial class Cosmos_Imagini : Form
    {
        public Cosmos_Imagini()
        {
            InitializeComponent();
        }

        Random rnd;
        string[] corpuri = { "Soare", "Luna", "Pamant" };
        string ales;
        bool[] ocupat = new bool[7];
        int other;
        bool[] selectate  = new bool[7];
        int nr_selectate = 0;

        private void Cosmos_Imagini_Load(object sender, EventArgs e)
        {
            gaseste_scop();
            genereaza_bune();
            genereaza_rele();
        }

        public void gaseste_scop()
        {
            rnd = new Random();
            int indice = rnd.Next(0, 3);
            ales = corpuri[indice];
            do
            {
                rnd = new Random();
                other = rnd.Next(0, 3);
                Thread.Sleep(50);

            } while (other == indice);
            label1.Text += "Selecteaza cele 3 imagini care contin ";
            label1.Text += ales;
        }

        public void genereaza_bune()
        {
            for(int i=1; i<=3; i++)
            {
                int pbox;
                do
                {
                    rnd = new Random();
                    Thread.Sleep(50);
                    rnd = new Random();
                    pbox = rnd.Next(1, 7);

                    foreach (PictureBox c in this.Controls.OfType<PictureBox>())
                    {
                        if (c.Tag.ToString() == pbox.ToString())
                        {
                            if (ocupat[pbox] == false)
                            {
                                c.ImageLocation = AppContext.BaseDirectory + String.Format(@"ImaginiValidare\{0}", ales + i.ToString() + ".png");
                                   c.Image = Image.FromFile(
                                    String.Format(@"ImaginiValidare\{0}", ales + i.ToString() + ".png"));
                                //c.Tag += ales;
                            }
                            break;
                        }
                    }
                } while (ocupat[pbox]);
                ocupat[pbox] = true;
            }
        }

        public void genereaza_rele()
        {
            for (int i = 1; i <= 3; i++)
            {
                int pbox;
                do
                {
                    Thread.Sleep(50);
                    rnd = new Random();
                    pbox = rnd.Next(1, 7);

                    foreach (PictureBox c in this.Controls.OfType<PictureBox>())
                    {
                        if (c.Tag.ToString() == pbox.ToString())
                        {
                            if (ocupat[pbox] == false)
                            {
                                c.ImageLocation = AppContext.BaseDirectory + String.Format(@"ImaginiValidare\{0}", corpuri[other] + i.ToString() + ".png");
                                c.Image = Image.FromFile(
                                    String.Format(@"ImaginiValidare\{0}", corpuri[other] + i.ToString() + ".png"));
                                //c.Tag += corpuri[other];
                            }
                            break;
                        }
                    }
                } while (ocupat[pbox]);
                ocupat[pbox] = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string hash = sender.GetHashCode().ToString();
            foreach (PictureBox c in this.Controls.OfType<PictureBox>())
            {
                if (c.GetHashCode().ToString()==hash)
                {
                    if (selectate[Convert.ToInt32(c.Tag)] == false)
                    {
                        if (nr_selectate < 3)
                        {
                            Pen borderPen = new Pen(Color.Red, 1);
                            Graphics g = this.CreateGraphics();
                            g.DrawRectangle(borderPen, c.Location.X - 1, c.Location.Y - 1, c.Width + 1, c.Height + 1);
                            nr_selectate++;
                        }
                        else
                        {
                            MessageBox.Show("POTI SELECTA MAXIM 3 IMAGINI");
                        }
                    }
                    else
                    {
                        Pen borderPen = new Pen(Color.Black, 1);
                        Graphics g = this.CreateGraphics();
                        g.DrawRectangle(borderPen, c.Location.X - 1, c.Location.Y - 1, c.Width + 1, c.Height + 1);
                        nr_selectate--;
                    }
                    selectate[Convert.ToInt32(c.Tag)] = !selectate[Convert.ToInt32(c.Tag)];
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(nr_selectate<3)
            {
                MessageBox.Show("nu ai selectat destule imagini");
                return;
            }
            foreach (PictureBox c in this.Controls.OfType<PictureBox>())
            {
                if (selectate[Convert.ToInt32(c.Tag)])
                {
                    string path = c.ImageLocation.ToString();
                    if(!path.Contains(ales))
                    {
                        MessageBox.Show("nu ai selectat corect");
                        return;
                    }
                }
            }
            MessageBox.Show("Ai reusit");
            this.Close();//aici esti
        }
    }
}
