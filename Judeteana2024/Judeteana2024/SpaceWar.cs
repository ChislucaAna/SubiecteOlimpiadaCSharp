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
            textBox2.Text = vieti.ToString() ;
        }

        private void pauza_Click(object sender, EventArgs e)
        {

        }

        private void end_Click(object sender, EventArgs e)
        {

        }

        private void timerSecunde_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            nrsecunde++;
            //spawn asteroid
            if(nrsecunde%2==0) //spawn inamic
            {
                inamic.Location = new Point(pictureBox1.Width, rnd.Next(0, pictureBox1.Height));
            }
            if (nrsecunde % 7 == 0) //spawn bonus
            {
                bonus.Location = new Point(pictureBox1.Width, rnd.Next(0, pictureBox1.Height));
            }
        }

        private void timer30mili_Tick(object sender, EventArgs e)
        {
            inamic.Location= new Point(inamic.Location.X-5, inamic.Location.Y);
            bonus.Location = new Point(inamic.Location.X - 10, inamic.Location.Y);
        }

        public void genereazaRacheta()
        {

        }

        private void SpaceWar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                if (nava.Location.Y > 0)
                    nava.Location = new Point(nava.Location.X, nava.Location.Y - 10);
                nava.Image = Image.FromFile("navaUp.png");
            }
            if (e.KeyCode == Keys.A)
            {
                if(nava.Location.X<0)
                    nava.Location = new Point(nava.Location.X-10, nava.Location.Y);
                nava.Image = Image.FromFile("navaMove.png");
            }
            if (e.KeyCode == Keys.S)
            {
                if (nava.Location.Y < pictureBox1.Height)
                    nava.Location = new Point(nava.Location.X, nava.Location.Y+10);
                nava.Image = Image.FromFile("navaDown.png");
            }
            if (e.KeyCode == Keys.D)
            {
                if (nava.Location.X < pictureBox1.Width)
                    nava.Location = new Point(nava.Location.X + 10, nava.Location.Y);
                nava.Image = Image.FromFile("navaMove.png");
            }
            if (e.KeyCode == Keys.D)
            {
                if (nava.Location.X < pictureBox1.Width)
                    genereazaRacheta();
                nava.Image = Image.FromFile("navaFire.png");
            }
        }
    }
}
