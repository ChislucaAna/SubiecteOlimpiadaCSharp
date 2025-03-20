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

namespace Nationala2024_peClase
{
    public partial class COSMOS_Constelatii : Form
    {
        public COSMOS_Constelatii()
        {
            InitializeComponent();
        }

        int index_zodia_curenta = 0;
        Dictionary<int, (string, string)> info = new Dictionary<int, (string, string)>();
        Bitmap bmp;
        int zoomtoX=0, zoomtoY=0;
        int zoomcoeficient=1;

        private void COSMOS_Constelatii_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("detalii.txt");
            string line;
            int indexer = 0;
            while ((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                info.Add(indexer, (fields[0],fields[3]));
                indexer++;
            }
            refreshinfo();
        }

        public void refreshinfo()
        {
            Console.WriteLine(index_zodia_curenta);
            label1.Text = info[index_zodia_curenta].Item1;
            label1.Text+=(Environment.NewLine);
            label1.Text+=(info[index_zodia_curenta].Item2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (index_zodia_curenta >= 1)
                index_zodia_curenta--;
            else
                index_zodia_curenta = 11;
            pictureBox1.Refresh();
            refreshinfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index_zodia_curenta <11)
                index_zodia_curenta++;
            else
                index_zodia_curenta = 0;
            pictureBox1.Refresh();
            refreshinfo();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoomcoeficient = trackBar1.Value;
            
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("painting");
            Console.WriteLine(zoomcoeficient);
            if (bmp != null)
            {
                Bitmap aux = new Bitmap(bmp, pictureBox1.Width * zoomcoeficient, pictureBox1.Height * zoomcoeficient);
                e.Graphics.DrawImage(aux, -zoomtoX*zoomcoeficient+(pictureBox2.Width/2), -zoomtoY*zoomcoeficient+(pictureBox2.Height/2));
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //nui ce trebe
        {
            Console.WriteLine("movin");
            zoomtoX = e.X; zoomtoY = e.Y;
            pictureBox2.Refresh();
        }

        private void COSMOS_Constelatii_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);   

            string path = AppContext.BaseDirectory;
            path += @"ImaginiConstelatii\";
            path += "earth.png";
            Bitmap pamant = new Bitmap(Image.FromFile(path), 70, 70);
           g.DrawImage(pamant, new Point(pictureBox1.Width / 2 - pamant.Width / 2, pictureBox1.Height / 2 - pamant.Height / 2));

            for(int index=0; index<12; index++){
                path = AppContext.BaseDirectory;
                path += @"ImaginiConstelatii\";
                path += info[index].Item1.ToLower() + ".png";
                Bitmap zodie = new Bitmap(Image.FromFile(path), 70, 70);
                double angle_current = index * (360 / 12);
                angle_current = (angle_current* Math.PI) / 180;
                Console.WriteLine(angle_current);
                int widthelipsa = 350;
                int heightelipsa = 200;
                int x =(pictureBox1.Width/2)-35+ (int)(widthelipsa*Math.Cos(angle_current*(-1)));
                int y = (pictureBox1.Height / 2)-35+(int)(heightelipsa * Math.Sin(angle_current * (-1)));
                //Console.WriteLine(x);
                //Console.WriteLine(y);
                g.DrawImage(zodie, new Point(x,y));

                if (index == index_zodia_curenta)
                {
                    path = AppContext.BaseDirectory;
                    path += @"ImaginiZodii\";
                    path += (index_zodia_curenta + 1).ToString() + ".png";
                    Bitmap icon = new Bitmap(Image.FromFile(path), 30, 30);
                    widthelipsa = 300;
                    heightelipsa = 150;
                    x = (pictureBox1.Width / 2) - 35 +icon.Width/2 +(int)(widthelipsa * Math.Cos(angle_current * (-1)));
                    y = (pictureBox1.Height / 2) - 35 +icon.Width/2 + (int)(heightelipsa * Math.Sin(angle_current * (-1)));
                    g.DrawImage(icon, new Point(x, y));
                }
            }

            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
