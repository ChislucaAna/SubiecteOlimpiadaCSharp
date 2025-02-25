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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using Microsoft.VisualBasic;

namespace Nationala2022
{
    public partial class InterferenteEco : Form
    {
        public InterferenteEco()
        {
            InitializeComponent();
        }
        public InterferenteEco(Image background)
        {
            InitializeComponent();
            this.background = background;
            this.KeyPreview = true;
        }
        
        Image background;
        Bitmap harta;
        int[,] m = new int[30, 30];
        int directie_curenta=0;
        bool deflector_selectat = false;
        int mouseX, mouseY;
        bool started = false;
        Keys directieRobot;


        private void InterferenteEco_Load(object sender, EventArgs e)
        {
            Deflector.width = pictureBox1.Width / 20;
            Deflector.height = pictureBox1.Height / 10;
            Deflector.Init(); //right size to fit in a cell
            pictureBox2.Size = new Size(pictureBox1.Width / 20, pictureBox1.Height / 10);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        public void refreshBitmap()
        {
            harta = new Bitmap(background, new Size(pictureBox1.Width, pictureBox1.Height));
            Graphics g = Graphics.FromImage(harta);
            int x = 0, y = 0;
            int pasx = pictureBox1.Width / 20;
            int pasy = pictureBox1.Height / 10;

            if (checkBox1.Checked)
            {
                for (int i = 1; i <= 20; i++) //20 coloane
                {
                    g.DrawLine(new Pen(color: Color.White), new Point(x, 0), new Point(x, pictureBox1.Height));
                    x += pasx;
                }
                for (int i = 1; i <= 10; i++) //10 randuri
                {
                    g.DrawLine(new Pen(color: Color.White), new Point(0, y), new Point(pictureBox1.Width, y));
                    y += pasy;
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if (m[i, j] == 0) continue;
                    if (m[i, j] < 9)
                    {
                        Bitmap obiect = new Bitmap(Image.FromFile(Harta.digitToObject[m[i, j]] + ".png"), pasx, pasy);
                        g.DrawImage(obiect, (j - 1) * pasx, (i - 1) * pasy);
                    }
                    else
                    {
                        int dir = m[i, j] - 9;
                        Point p1, p2, p3;
                        (p1, p2, p3) = Deflector.types[dir];
                        Point[] points = { p1, p2, p3 };
                        Bitmap aux = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                        Graphics h = Graphics.FromImage(aux);   
                        h.FillPolygon(new SolidBrush(Color.Red), points);
                        g.DrawImage(aux, (j - 1) * pasx, (i - 1) * pasy);
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            refreshBitmap();
            e.Graphics.DrawImage(harta, 0, 0);
            if (deflector_selectat) //temporar apare unde e mouse ul
            {
                e.Graphics.DrawImage(Deflector.current, mouseX, mouseY);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                //pe prima linie e robotul dupa entitatiile
                StreamReader streamReader   = new StreamReader(ofd.FileName);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                { 
                    string[] bucati = line.Split(' ');
                    
                    string obiect = bucati[0];
                    int x = Convert.ToInt32(bucati[1]);
                    int y = Convert.ToInt32(bucati[2]);
                    m[y, x] = Harta.objectToDigit[bucati[0]];

                    pictureBox1.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Selectati o harta valida!");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            deflector_selectat = !deflector_selectat;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            directie_curenta++;
            pictureBox2.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) //DEFLECTOR FRAME
        {
            e.Graphics.Clear(BackColor);
            Point p1, p2, p3;
            (p1, p2, p3) = Deflector.types[directie_curenta % 4];
            Point[] points = { p1, p2, p3 };

            //desenezi in bitmap
            Deflector.resetBitmap();  
            Graphics g = Graphics.FromImage(Deflector.current);
            g.FillPolygon(new SolidBrush(Color.Red), points);

            e.Graphics.DrawImage(Deflector.current, new Point(0, 0));
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            deflector_selectat = false;
            int pasx = pictureBox1.Width / 20;
            int pasy = pictureBox1.Height / 10;

            //adaugam in cel mai apropiat patrat (1 based index)
            int x = e.X / pasx;
            x++;
            int y = e.Y / pasy;
            y++;
            m[y, x] = (directie_curenta%4)+9;
            Console.WriteLine(y);
            Console.WriteLine(x);
            Console.WriteLine(m[y,x].ToString());

            pictureBox1.Refresh();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            started = !started;
            if (started)
                button4.Text = "stop";
            else
                button4.Text = "start";
            MessageBox.Show("Apasati una din tastele WASD pentru a incepe deplasarea in directia dorita");
            this.KeyDown += SelectDirection;
        }

        public void SelectDirection(object sender, KeyEventArgs e)
        {
            Console.WriteLine("key event fired");
            directieRobot = e.KeyCode;
            timer1.Start();
            this.KeyDown -= SelectDirection;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(deflector_selectat)
            {
                mouseX = e.X; mouseY = e.Y;
                pictureBox1.Refresh();
            }
        }
    }
}
