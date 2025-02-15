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

namespace Judeteana2023
{
    public partial class Ghiceste : Form
    {
        public Ghiceste()
        {
            InitializeComponent();
        }
        string cuvant;
        int stadiu = 6;
        int g = 0;
        int punctaj = 0;
        bool gameEnded = false;
        List<Label> labels = new List<Label>();
        Random rnd;

        private void Ghiceste_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Cuvinte.txt");
            string line;
            rnd= new Random();  
            int index = rnd.Next(0, 9);
            while ((line = sr.ReadLine()) != null)
            {
                if (index == 0)
                {
                    cuvant = line.ToUpper();
                }
                else
                {
                    index--;
                }
            }

            int startx = 100;
            int starty = 100;
            for(int i=0; i < cuvant.Length; i++)
            {
                Label label = new Label();
                label.BackColor = Color.Pink;
                label.Size = new Size(20, 20);
                label.Location = new Point(startx, starty);
                label.Text = " ";
                startx += 50;

                labels.Add(label);
                this.Controls.Add(label);
            }
        }

        private void button1_Click(object sender, EventArgs e) //s-a dat click pe litera
        {
            if (gameEnded)
                return;
            Button b = sender as Button;
            if(cuvant.Contains(b.Text))
            {
                for (int i = 0; i < cuvant.Length; i++)
                {
                    if (cuvant[i] == Convert.ToChar(b.Text))
                    {
                        labels[i].Text = cuvant[i].ToString();
                    }
                   
                }
                if(stadiu<6)
                {
                    stadiu++;
                    pictureBox1.Image = Image.FromFile(stadiu + ".png");
                }
                string progress="";
                foreach(Label l in labels)
                {
                    progress += l.Text;
                }
                MessageBox.Show(progress);
                if(progress.Equals(cuvant))
                {
                    gameEnded = true;
                    endGame();
                }
            }
            else
            {
                g++;
                if(stadiu>1)
                {
                    stadiu--;
                    pictureBox1.Image = Image.FromFile(stadiu + ".png");
                }
                else
                {
                    gameEnded = true;
                    endGame();
                }
            }
            this.flowLayoutPanel1.Controls.Remove(b);
        }

        public void endGame()
        {
            punctaj = 100 - 4 * g;
            textBox1.Text= punctaj.ToString();
            MessageBox.Show("jocul s-a incheiat");
            Db.rezultate.Add(new Rezultat(Db.rezultate.Count+1, 0, Db.utilizatorLogat.EmailUtilizator, punctaj));
            Db.SaveData();
        }
    }
}
