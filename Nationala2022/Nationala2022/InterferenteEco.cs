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
        }
        Image background;
        Bitmap harta;
        int[,] m = new int[30, 30];

        Dictionary<int, string> digitToObject = new Dictionary<int, string>
        {
            {1,"Meduza1" },
            {2,"Meduza2" },
            {3,"Meduza3" },
            {4,"Meduza4" },
            {5,"Sticla" },
            {6,"Hartie" },
            {7,"Plastic" },
            {8,"Robot" },
            {9,"Dreapta-Jos" },
            {10,"Stanga-Jos" },
            {11,"Stanga-Sus" },
            {12,"Dreapta-Sus" }
        };

        Dictionary<string,int> objectToDigit = new Dictionary<string, int>
        {
            {"Meduza1" ,1},
            {"Meduza2",2 },
            {"Meduza3",3 },
            {"Meduza4",4 },
            {"Sticla",5 },
            {"Hartie",6},
            {"Plastic",7 },
            {"Robot" , 8         },
            {"Dreapta-Jos" , 9 },
            {"Stanga-Jos" , 10 },
            {"Stanga-Sus" , 11 },
            {"Dreapta-Sus" , 12 }
        };

        private void InterferenteEco_Load(object sender, EventArgs e)
        {
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
                    Bitmap obiect = new Bitmap(Image.FromFile(digitToObject[m[i,j]] + ".png"), pasx, pasy);
                    g.DrawImage(obiect, (j - 1) * pasx, (i - 1) * pasy);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            refreshBitmap();
            e.Graphics.DrawImage(harta, 0, 0);

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
                    m[y, x] = objectToDigit[bucati[0]];

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

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) //DEFLECTOR FRAME
        {

        }
    }
}
