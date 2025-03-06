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
    public partial class MiscarePamantLuna : Form
    {
        public MiscarePamantLuna()
        {
            InitializeComponent();
        }

        bool paused = true;
        DateTime datacurenta;

        int distantaMaxima = 152;
        int distantaMinima = 147;
        double UnghiPamant=0, UnghiLuna=0;
        double PasPamant=360.0/366.0, PasLuna=360.0/28;

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("Datele pot fi gasite la {0} in fisierul date", AppContext.BaseDirectory));
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)//start
        {
            paused = !paused;
            button1.Enabled = !button1.Enabled;
            button2.Enabled = !button2.Enabled;
            zi.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            recalculate();
            e.Graphics.DrawEllipse(new Pen(Color.Red), Soare.Location.X - 152 + Soare.Width / 2, Soare.Location.Y - 147 + Soare.Height / 2, 152 * 2, 147 * 2);
            e.Graphics.DrawEllipse(new Pen(Color.Blue), Pamant.Location.X - 152 / 4 + Pamant.Width / 2, Pamant.Location.Y - 147 / 4 + Pamant.Height / 2, 152 / 2, 147 / 2);
        }

        public void recalculate()
        {
            double xpamant = Math.Cos(UnghiPamant*Math.PI/180.0) * distantaMaxima - Pamant.Width/2;
            double ypamant = Math.Sin(UnghiPamant * Math.PI / 180.0) * distantaMinima - Pamant.Height/2;
            Console.WriteLine(xpamant + " " + ypamant); 
            double soarecentruX = Soare.Location.X + Soare.Width / 2;
            double soarecentruy = Soare.Location.Y + Soare.Height / 2;
            Pamant.Location = new Point(Convert.ToInt32(soarecentruX+xpamant),Convert.ToInt32(soarecentruy+ypamant));

            double pamantCentruX = Pamant.Location.X + Pamant.Width / 2;
            double pamantCentruY = Pamant.Location.Y + Pamant.Height / 2;
            double xluna = Math.Cos(UnghiLuna * Math.PI / 180.0) * distantaMaxima / 4 -luna.Width/2;
            double yluna = Math.Sin(UnghiLuna * Math.PI / 180.0) * distantaMinima / 4 -luna.Height/2;
            luna.Location = new Point(Convert.ToInt32(pamantCentruX+xluna),
                Convert.ToInt32(pamantCentruY+yluna));
        }

        private void button2_Click(object sender, EventArgs e) //pause
        {
            paused = !paused;
            button1.Enabled = !button1.Enabled;
            button2.Enabled = !button2.Enabled;
            zi.Stop();
            //addtodb
            StreamWriter sr = new StreamWriter("date.txt",true);
            sr.WriteLine(textBox1.Text + ";" + textBox2.Text + ";" + textBox3.Text);
            sr.Close();
        }

        private void lunaTimer_Tick(object sender, EventArgs e)
        {

            //a trecut o zi
            UnghiLuna -= PasLuna;
            UnghiPamant -= PasPamant;
            Console.WriteLine(UnghiPamant);
            datacurenta=datacurenta.AddDays(1);
            textBox1.Text = datacurenta.ToString();
            textBox2.Text = datacurenta.ToString("MMMM");
            //calc distanta
            double xpamant = Math.Cos(UnghiPamant * Math.PI / 180.0) * distantaMaxima - Pamant.Width / 2;
            double ypamant = Math.Sin(UnghiPamant * Math.PI / 180.0) * distantaMinima - Pamant.Height / 2;
            textBox3.Text = Math.Sqrt(Math.Pow(xpamant, 2) + Math.Pow(ypamant, 2)).ToString(); ;    
            pictureBox1.Refresh();
        }

        private void MiscarePamantLuna_Load(object sender, EventArgs e)
        {
            Soare.Location = new Point(pictureBox1.Width / 2, pictureBox1.Height / 2);
            datacurenta = Convert.ToDateTime("03/01/2024");
            pictureBox1.Invalidate();
        }
    }
}
