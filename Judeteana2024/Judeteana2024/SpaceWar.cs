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
namespace Judeteana2024
{
    public partial class SpaceWar : Form
    {
        public SpaceWar()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        int scor = 0;
        int vieti = 3;
        int nrsecunde = 0;
        List<PictureBox> rachete = new List<PictureBox>(); 
        List<PictureBox> inamici = new List<PictureBox>();
        List<PictureBox> asteroizi = new List<PictureBox>();
        List<PictureBox> bonusuri = new List<PictureBox>();
        bool paused = false;

        private void start_Click(object sender, EventArgs e)
        {
            start.Enabled = false;
            pauza.Enabled = true;
            end.Enabled = true;
            refreshLabels();
            axWindowsMediaPlayer1.URL = Path.GetFullPath("sunetFundal.mp3");
            timerSecunde.Start();
            timer30mili.Start();
        }

        public void refreshLabels()
        {
            textBox1.Text = scor.ToString();
            textBox2.Text = vieti.ToString();
            if (vieti == 0)
            {
                start.Enabled = true;
                pauza.Enabled = false;
                end.Enabled = false;
                timerSecunde.Stop();
                timer30mili.Stop();
                mesaj.Visible = true;
                mesaj.Text = "Nava a fost distrusă";
                MesajViteza.Start();
            }
            if(scor==10)
            {
                start.Enabled = true;
                pauza.Enabled = false;
                end.Enabled = false;
                timerSecunde.Stop();
                timer30mili.Stop();
                mesaj.Visible = true;
                mesaj.Text = "Nava a invins";
                MesajViteza.Start();
            }
        }

        private void pauza_Click(object sender, EventArgs e)
        {
            paused = !paused;
            if (paused)
            {
                timerSecunde.Stop();
                timer30mili.Stop();
                mesaj.Visible = true;
                mesaj.Text = "pauza joc";
                MesajViteza.Start();
                axWindowsMediaPlayer1.URL = null;
            }
            else
            {
                timerSecunde.Start();
                timer30mili.Start();
                mesaj.Visible = false;
                mesaj.Location = new Point(335, 506);
                axWindowsMediaPlayer1.URL = Path.GetFullPath("sunetFundal.mp3");
            }
        }

        private void end_Click(object sender, EventArgs e)
        {
            timerSecunde.Stop();
            timer30mili.Stop();
            var u =MessageBox.Show("Opriti jocul?", "Opriti?", MessageBoxButtons.YesNo);
            if (u == DialogResult.Yes)
            {
                this.Close();
            }
            timerSecunde.Start();
            timer30mili.Start();
        }

        public void genereazaInamic()
        {
            Random rnd = new Random();
            PictureBox inamic = new PictureBox();
            inamic.Image = Image.FromFile("inamic.gif");
            inamic.SizeMode = PictureBoxSizeMode.StretchImage;
            inamic.Size = new Size(50, 50);
            inamic.Location = new Point(pictureBox1.Width, rnd.Next(0, pictureBox1.Height));
            inamici.Add(inamic);
            pictureBox1.Controls.Add(inamic);
        }
        public void genereazaBonus()
        {
            Random rnd = new Random();
            PictureBox bonus = new PictureBox();
            bonus.Image = Image.FromFile("viata.gif");
            bonus.SizeMode = PictureBoxSizeMode.StretchImage;
            bonus.Size = new Size(50, 50);
            bonus.Location = new Point(pictureBox1.Width, rnd.Next(0, pictureBox1.Height));
            bonusuri.Add(bonus);
            pictureBox1.Controls.Add(bonus);
        }
        public void genereazaAsteroid()
        {
            Random rnd = new Random();
            PictureBox astroid = new PictureBox();
            astroid.Image = Image.FromFile("asteroid.png");
            astroid.SizeMode = PictureBoxSizeMode.StretchImage;
            astroid.Size = new Size(50, 50);
            astroid.Location = new Point(pictureBox1.Width, rnd.Next(0, pictureBox1.Height));
            asteroizi.Add(astroid);
            pictureBox1.Controls.Add(astroid);
        }



        private void timerSecunde_Tick(object sender, EventArgs e)
        {
            nrsecunde++;
            genereazaAsteroid();
            if(nrsecunde%2==0) //spawn inamic
            {
                genereazaInamic();
            }
            if (nrsecunde % 7 == 0) //spawn bonus
            {
                genereazaBonus();
            }
        }

        private void timer30mili_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox pictureBox in rachete) //move racheta
            {
                pictureBox.Location = new Point(pictureBox.Location.X + 7, pictureBox.Location.Y);
            }
            foreach (PictureBox pictureBox in rachete) //verifica daca ai lovit un inamic
            {
                bool hit = false;
                Rectangle racheta = new Rectangle(pictureBox.Location, pictureBox.Size);
                foreach (PictureBox inamic in inamici)
                {
                    Rectangle inamicHitbox = new Rectangle(inamic.Location, inamic.Size);
                    if (racheta.IntersectsWith(inamicHitbox))
                    {
                        rachete.Remove(pictureBox);
                        pictureBox1.Controls.Remove(pictureBox);
                        inamici.Remove(inamic);
                        pictureBox1.Controls.Remove(inamic);
                        scor++;
                        refreshLabels();
                        hit = true;
                        break;
                    }
                }
                if (hit == true)
                    break;
            }
            foreach (PictureBox pictureBox in inamici)
            {
                pictureBox.Location = new Point(pictureBox.Location.X - 5, pictureBox.Location.Y);
                Rectangle inamic = new Rectangle(pictureBox.Location, pictureBox.Size);
                Rectangle navaObj = new Rectangle(nava.Location, nava.Size);
                if (navaObj.IntersectsWith(inamic))
                {
                    inamici.Remove(pictureBox);
                    pictureBox1.Controls.Remove(pictureBox);
                    vieti--;
                    refreshLabels();
                    break;
                }
            }
            foreach (PictureBox pictureBox in bonusuri)
            {
                pictureBox.Location = new Point(pictureBox.Location.X - 10, pictureBox.Location.Y);
                Rectangle bonus = new Rectangle(pictureBox.Location, pictureBox.Size);
                Rectangle navaObj = new Rectangle(nava.Location, nava.Size);
                if(navaObj.IntersectsWith(bonus))
                {
                    vieti++;
                    bonusuri.Remove(pictureBox);
                    pictureBox1.Controls.Remove(pictureBox);
                    refreshLabels();
                    break;
                }

            }
            foreach (PictureBox pictureBox in asteroizi)
            {
                pictureBox.Location = new Point(pictureBox.Location.X - 5, pictureBox.Location.Y);
            }
        }

        public void genereazaRacheta()
        {
            PictureBox racheta = new PictureBox();
            racheta.Image = Image.FromFile("rachetaNava.png");
            racheta.SizeMode=PictureBoxSizeMode.StretchImage;
            racheta.Size = new Size(50, 50);
            racheta.Location = nava.Location;
            rachete.Add(racheta);
            pictureBox1.Controls.Add(racheta); 
        }

        private void SpaceWar_KeyDown(object sender, KeyEventArgs e)
        {
            if (start.Enabled == true) //jocul nu a inceput inca
                return;
            if (e.KeyCode == Keys.W)
            {
                if (nava.Location.Y > 0)
                    nava.Location = new Point(nava.Location.X, nava.Location.Y - 10);
                nava.Image = Image.FromFile("navaUp.png");
            }
            if (e.KeyCode == Keys.A)
            {
                if(nava.Location.X>0)
                    nava.Location = new Point(nava.Location.X-10, nava.Location.Y);
                nava.Image = Image.FromFile("navaMove.png");
            }
            if (e.KeyCode == Keys.S)
            {
                if (nava.Location.Y+nava.Height < pictureBox1.Height)
                    nava.Location = new Point(nava.Location.X, nava.Location.Y+10);
                nava.Image = Image.FromFile("navaDown.png");
            }
            if (e.KeyCode == Keys.D)
            {
                if (nava.Location.X+nava.Width < pictureBox1.Width)
                    nava.Location = new Point(nava.Location.X + 10, nava.Location.Y);
                nava.Image = Image.FromFile("navaMove.png");
            }
            if (e.KeyCode == Keys.Space)
            {
                if (nava.Location.X < pictureBox1.Width)
                    genereazaRacheta();
                nava.Image = Image.FromFile("navaFire.png");
            }
        }

        private void MesajViteza_Tick(object sender, EventArgs e)
        {
            mesaj.Location=new Point(mesaj.Location.X, mesaj.Location.Y-10);
            if(mesaj.Location.Y<=(pictureBox1.Height/2))
            {
                MesajViteza.Stop();
            }
        }
    }
}
