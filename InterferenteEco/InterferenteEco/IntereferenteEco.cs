using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InterferenteEco
{
    public partial class IntereferenteEco : Form
    {
        public IntereferenteEco()
        {
            InitializeComponent();
        }
        public IntereferenteEco(Image img)
        {
            InitializeComponent();
            backgroundharta = img;
            this.KeyPreview = true;
        }

        Image backgroundharta;
        Bitmap harta;
        string fisier;
        int currentdeflector=0;
        bool deflectorselectat = false;
        int hooverX, hooverY;
        List<(int,int)> pozitii = new List<(int, int)> ();
        List<(int, int)> BestRoad = new List<(int, int)>();
        List<(int, int)> visited = new List<(int, int)>();
        int indexmove = 0;
        int hartie = 0, plastic = 0, sticla = 0;

        public Dictionary<string, int> stringToInt = new Dictionary<string, int>()
        {
            { "Robot",-1 },
            { "Meduza1",1 },
            { "Meduza2",2 },
             { "Meduza3",3 },
              { "Meduza4",4 },
              { "Hartie",5 },
              { "Plastic",6 },
              { "Sticla",7 },
              { "DJ",8 },
              { "SJ",9 },
              { "SS",10 },
              { "DS",11 },
        };

        int[,] m = new int[30,30];
        int[,]distante_min = new int[30, 30];
        bool started;
        string directiadedeplasare = "";
        string DirectiaInitiala = "";
        int irobot, jrobot;
        int iinitial, jinitial;
        bool matrice_realizata = false;

        public class deflector
        {
            public int x; 
            public int y;
            public int val;

            public deflector(int x, int y,int val)
            {
                this.x = x; 
                this.y = y;
                this.val = val;
            }
        }

        List<deflector> deflectors = new List<deflector>();
        private void IntereferenteEco_Load(object sender, EventArgs e)
        {
            //GENEREAZA IMG PT DEFLECTOR
            Bitmap bmp;
            Graphics g;
            bmp= new Bitmap(pictureBox1.Width / 20, pictureBox1.Height / 10);
            Point stangasus = new Point(0, 0);
            Point dreaptasus = new Point( bmp.Width,0);
            Point stangajos = new Point(0, bmp.Height);
            Point dreaptajos = new Point(bmp.Width, bmp.Height);
            g = Graphics.FromImage(bmp);
            g.FillPolygon(new SolidBrush(Color.White),new Point[] { stangasus, dreaptasus, stangajos });
            bmp.Save("DJ.png", System.Drawing.Imaging.ImageFormat.Png);

            bmp = new Bitmap(pictureBox1.Width / 20, pictureBox1.Height / 10);
            g = Graphics.FromImage(bmp);
            g.FillPolygon(new SolidBrush(Color.White), new Point[] { stangasus, dreaptasus, dreaptajos });
            bmp.Save("SJ.png", System.Drawing.Imaging.ImageFormat.Png);

            bmp = new Bitmap(pictureBox1.Width / 20, pictureBox1.Height / 10);
            g = Graphics.FromImage(bmp);
            g.FillPolygon(new SolidBrush(Color.White), new Point[] { stangajos, dreaptasus, dreaptajos });
            bmp.Save("SS.png", System.Drawing.Imaging.ImageFormat.Png);

            bmp = new Bitmap(pictureBox1.Width / 20, pictureBox1.Height / 10);
            g = Graphics.FromImage(bmp);
            g.FillPolygon(new SolidBrush(Color.White), new Point[] { stangajos, stangasus, dreaptajos });
            bmp.Save("DS.png", System.Drawing.Imaging.ImageFormat.Png);

            pictureBox2.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        public void drawgrid()
        {
            Graphics g = Graphics.FromImage(harta);
            int pas = pictureBox1.Width / 20;
            int x=0;
            for(int i=0; i<20; i++)
            {
                g.DrawLine(new Pen(Color.White), x, 0, x,pictureBox1.Height);
                x += pas;
            }
            x = 0;
            pas = pictureBox1.Height / 10;
            for (int i = 0; i < 10; i++)
            {
                g.DrawLine(new Pen(Color.White), 0, x, pictureBox1.Width, x);
                x += pas;
            }
        }

        public void incarcaharta() //din fisier in matrice
        {
            if (fisier == null) { return; }
            StreamReader sr = new StreamReader(fisier);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] bucati = line.Split(' ');
                int x = Convert.ToInt32(bucati[1]); 
                int y = Convert.ToInt32(bucati[2]);
                int obiect = stringToInt[bucati[0]];
                m[x,y] = obiect;    
            }
        }

        public void incarcaharta2() //din matrice in png
        {
            Graphics g = Graphics.FromImage(harta);
            foreach (var pair in pozitii)
            {
                int widthcelula = (pictureBox1.Width / 20);
                int heightcelula = (pictureBox1.Height / 10);
                if (pair == pozitii.First())
                {
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle((pair.Item2 - 1) * widthcelula, (pair.Item1 - 1) * heightcelula,
    widthcelula, heightcelula));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Purple), new Rectangle((pair.Item2 - 1) * widthcelula, (pair.Item1 - 1) * heightcelula,
    widthcelula, heightcelula));
                }
            }

            for (int i=1; i<=10; i++)
            {
                for(int j=1; j<=20; j++)
                {
                    int searchValue = m[i,j];
                    if (searchValue != 0)
                    {
                        if(searchValue<0)
                        {
                            irobot = i; jrobot=j;
                        }
                        string key = stringToInt.FirstOrDefault(x => x.Value == searchValue).Key;
                        Bitmap element = new Bitmap(Image.FromFile(key + ".png"), pictureBox1.Width / 20, pictureBox1.Height / 10);
                        g.DrawImage(element, (j - 1) * pictureBox1.Width / 20, (i - 1) * pictureBox1.Height / 10);
                    }
                }
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            harta = new Bitmap(backgroundharta, pictureBox1.Size);

            if (checkBox1.Checked)
            {
                drawgrid();
            }
            incarcaharta2();
            e.Graphics.DrawImage(harta, 0, 0);

            if (deflectorselectat)
            {
                string key = stringToInt.FirstOrDefault(x => x.Value == currentdeflector + 8).Key;
                Bitmap element = new Bitmap(Image.FromFile(key + ".png"), pictureBox1.Width / 20, pictureBox1.Height / 10);
                e.Graphics.DrawImage(element,hooverX, hooverY);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fisier = openFileDialog.FileName;
                incarcaharta();
            }
            pictureBox1.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            deflectorselectat = !deflectorselectat;
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentdeflector++;
            currentdeflector = currentdeflector % 4;
            pictureBox2.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            pictureBox2.Size = new Size(pictureBox1.Width / 20, pictureBox1.Height / 10);
            string key = stringToInt.FirstOrDefault(x => x.Value == currentdeflector + 8).Key;
            Bitmap element = new Bitmap(Image.FromFile(key + ".png"), pictureBox1.Width / 20, pictureBox1.Height / 10);
            g.DrawImage(element, 0, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(deflectorselectat)
            {
                int widthcelula = (pictureBox1.Width / 20);
                int heightcelula = (pictureBox1.Height / 10);
                m[(e.Y/ heightcelula)+1, (e.X / widthcelula) + 1] = currentdeflector + 8;
                deflectors.Add(new deflector((e.Y / heightcelula) + 1, (e.X / widthcelula) + 1,currentdeflector+8)); //salvezi pt restart
                printharta();
                deflectorselectat = false;
                pictureBox1.Refresh();
            }
        }

        public void printharta()
        {
            for (int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 20; j++)
                {
                    Console.Write(m[i, j]);
                }
                Console.WriteLine();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (started == false)
            {
                button4.Text = "Stop";
                if (directiadedeplasare == "") //prima data cand incepi salvezi pozitia
                {
                    findrobot();
                    iinitial = irobot; jinitial = jrobot;
                    MessageBox.Show("Apasa una din tastele WASD PT A incepe");
                }
                started = true;
                timer1.Start();
            }
            else if (started == true)
            {
                button4.Text = "Start";
                started = false;
                timer1.Stop();
            }
        }

        private void IntereferenteEco_KeyDown(object sender, KeyEventArgs e)
        {
            if(started && directiadedeplasare=="")
            {
                if(e.KeyCode == Keys.W)
                {
                    directiadedeplasare = "up";
                }
                if (e.KeyCode == Keys.A)
                {
                    directiadedeplasare = "left";
                }
                if (e.KeyCode == Keys.S)
                {
                    directiadedeplasare = "down";
                }
                if (e.KeyCode == Keys.D)
                {
                    directiadedeplasare = "right";
                }
                DirectiaInitiala = directiadedeplasare;
            }
        } //selecteaza directia de deplasare

        public void intoarce()
        {
            if (directiadedeplasare == "up")
                directiadedeplasare = "down";
            else if (directiadedeplasare == "left")
                directiadedeplasare = "right";
            else if (directiadedeplasare == "right")
                directiadedeplasare = "left";
            else if (directiadedeplasare == "down")
                directiadedeplasare = "up";
        }

        public void evalueazadeflector(int val)
        {
            bool a_lovit_cateta = false;
            if (val == 8)
            {
                if (directiadedeplasare == "up")
                    directiadedeplasare = "right";
                else
                    if (directiadedeplasare == "left")
                    directiadedeplasare = "down";
                else
                { intoarce(); a_lovit_cateta = true; }
            }
            if (val == 9)
            {
                if (directiadedeplasare == "up")
                    directiadedeplasare = "left";
                else
                    if (directiadedeplasare == "right")
                    directiadedeplasare = "down";
                else
                { intoarce(); a_lovit_cateta = true; }
            }
            if (val == 10)
            {
                if (directiadedeplasare == "down")
                    directiadedeplasare = "left";
                else
                    if (directiadedeplasare == "right")
                    directiadedeplasare = "up";
                else
                { intoarce(); a_lovit_cateta = true; }
            }
            if (val == 11)
            {
                if (directiadedeplasare == "down")
                    directiadedeplasare = "right";
                else
                    if (directiadedeplasare == "left")
                    directiadedeplasare = "up";
                else
                { intoarce(); a_lovit_cateta = true; }
            }
            //sari peste deflector ca sa nu dispara de pe harta
            if (!a_lovit_cateta)
            {
                if (directiadedeplasare == "up")
                {
                    irobot--;
                }
                if (directiadedeplasare == "left")
                {
                    jrobot--;
                }
                if (directiadedeplasare == "down")
                {
                    irobot++;
                }
                if (directiadedeplasare == "right")
                {
                    jrobot++;
                }
            }
            else
            {
               if (directiadedeplasare == "up")
                {
                    irobot -= 2;
                }
                if (directiadedeplasare == "left")
                {
                    jrobot -= 2;
                }
                if (directiadedeplasare == "down")
                {
                    irobot += 2;
                }
                if (directiadedeplasare == "right")
                {
                    jrobot += 2;

                }
            } //movement cand se loveste de deflector
        }

        public void findrobot()
        {
            Graphics g = Graphics.FromImage(harta);
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                        if (m[i,j] < 0)
                        {
                            irobot = i; jrobot = j;
                        }
                }
            }
        }

        public void adaugatoatedeflectoarele()
        {
            foreach(deflector d in deflectors)
            {
                m[d.x, d.y] = d.val;
            }
        }

        private void button5_Click(object sender, EventArgs e) //RESTART FUNC
        {
            button4.Text = "Stop";
            started = true;
            hartie = 0; plastic = 0; sticla = 0;
            timer1.Stop();
            Array.Clear(m, 0, m.Length);
            incarcaharta();
            adaugatoatedeflectoarele();
            directiadedeplasare = DirectiaInitiala;
            pozitii.Clear();
            irobot = 0;
            jrobot = 0;
            refreshcountlabels();
            pictureBox1.Refresh();
            timer1.Start();
        }

        public void afiseaza_drum()
        {
            foreach(var move in BestRoad)
            {
                Console.WriteLine(move.Item1+" "+move.Item2);    
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (directiadedeplasare == "") return; //nu s-a sleectat inca directie
            if (done())
            {
                iinitial = 10; jinitial = 10;
                if (!matrice_realizata) //nu s-a calculat inca drumul
                {
                    timer1.Stop();
                    findrobot();
                    calculeaza_drum(irobot, jrobot);
                    afiseaza_drum();
                    timer1.Start();
                    return;
                }
                else
                {
                    if (irobot == iinitial && jrobot == jinitial)
                    {
                        timer1.Stop();
                        MessageBox.Show("Robotelul a ajuns cu succes la pozitia initiala");
                    }
                    else
                    {
                        findrobot();
                        m[irobot, jrobot] = 0;
                        distante_min[irobot, jrobot] = 100000;
                        int minim = Math.Min(Math.Min(distante_min[irobot, jrobot + 1], distante_min[irobot, jrobot - 1]),
                            Math.Min(distante_min[irobot + 1, jrobot], distante_min[irobot - 1, jrobot]));
                        afiseaza_matrice_distante();
                        Console.WriteLine(minim);
                        if (minim == distante_min[irobot, jrobot + 1])
                            m[irobot, jrobot + 1] = -1;
                        else if (minim == distante_min[irobot, jrobot - 1])
                            m[irobot, jrobot - 1] = -1;
                        else if (minim == distante_min[irobot + 1, jrobot])
                            m[irobot + 1, jrobot] = -1;
                        else if (minim == distante_min[irobot - 1, jrobot])
                            m[irobot - 1, jrobot] = -1;
                        pictureBox1.Refresh();
                        return;
                    }
                }
            }

            findrobot();
            pozitii.Add((irobot, jrobot)); //pt animatia de inapoi;
            m[irobot, jrobot] = 0;
            int NextCellValue = 0;
            switch (directiadedeplasare)
            {
                case "up":
                    NextCellValue = m[irobot - 1, jrobot];
                    irobot--;
                    break;
                case "left":
                    NextCellValue = m[irobot, jrobot - 1];
                    jrobot--;
                    break;
                case "down":
                    NextCellValue = m[irobot + 1, jrobot];
                    irobot++;
                    break;
                case "right":
                    NextCellValue = m[irobot, jrobot + 1];
                    jrobot++;
                    break;
            }

            if (NextCellValue >= 8) //e deflector continua miscarea, evalueaza schimbarea de directie
                evalueazadeflector(NextCellValue);
            else
            {
                if (NextCellValue >= 1 && NextCellValue <= 4)//meduza
                {
                    timer1.Stop();
                    MessageBox.Show("Robotul a lovit o meduza. Animatia s-a oprit");
                }
                switch (NextCellValue)
                {
                    case 5:
                        hartie++;
                        break;
                    case 6:
                        plastic++;
                        break;
                    case 7:
                        sticla++;
                        break;
                }
            }

            m[irobot, jrobot] = -1;
            refreshcountlabels();
            pictureBox1.Refresh();
        }

        public bool done() //s-au strans toate materialele reciclabile
        {
            return true;
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if (m[i, j] >=5 && m[i,j]<=7)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool iswithinbounds(int i, int j)
        {
            if(i >= 1 && j >= 1 && i <= 10 && j <= 20)
            {
                return true;
            }
            return false;
        }

        public void init_vizitat()
        {
            visited.Clear();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if (m[i, j] >=1 && m[i,j]<=4)
                    {
                        visited.Add((i, j));
                        Console.WriteLine(i + " " + j);
                    }
                }
            }
        }

        public void init_distante_min()
        {
            visited.Clear();
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    distante_min[i, j] = 100000;
                }
            }
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j <= 21; j++)
                {
                    if(!iswithinbounds(i,j))
                        distante_min[i, j] = 100000;
                }
            }
        }
        public void calculeaza_drum(int i,int j)
        {
            //adaugam toate elementele cu meduze in matricea de vizitat (ele sunt considerate obstacole)
            init_vizitat();
            init_distante_min();
            Queue<(int,int)> q = new Queue<(int, int)> ();
            q.Enqueue((i, j));
            visited.Add((i, j));
            distante_min[i, j] = 0;
            while(q.Count > 0) {
                var p = q.Dequeue(); //removes and returns the element at the front of the queue(u add elements thru the back)

                if(p.Item1 == iinitial && p.Item2 == jinitial)
                {
                    return;
                }

                if (iswithinbounds(i + 1, j) && !visited.Contains((i + 1, j)))
                {
                    q.Enqueue((i + 1, j));
                    visited.Add((i+1, j));
                    distante_min[i + 1, j] = distante_min[p.Item1,p.Item2]+1;
                }
                if (iswithinbounds(i - 1, j) && !visited.Contains((i - 1, j)))
                {
                    q.Enqueue((i - 1, j));
                    visited.Add((i - 1, j));
                    distante_min[i -1, j] = distante_min[p.Item1, p.Item2] + 1;
                }
                if (iswithinbounds(i, j + 1) && !visited.Contains((i, j + 1)))
                {
                    q.Enqueue((i, j + 1));
                    visited.Add((i, j+1));
                    distante_min[i,j+1] = distante_min[p.Item1, p.Item2] + 1;
                }
                if (iswithinbounds(i, j - 1) && !visited.Contains((i, j - 1)))
                {
                    q.Enqueue((i, j - 1));
                    visited.Add((i, j - 1));
                    distante_min[i, j - 1] = distante_min[p.Item1, p.Item2] + 1;
                }
            }
            afiseaza_matrice_distante();
            matrice_realizata = true;
        }

        public void afiseaza_matrice_distante()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    Console.Write(distante_min[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
        public void refreshcountlabels()
        {
            textBox1.Text = sticla.ToString(); 
            textBox2.Text = hartie.ToString(); 
            textBox3.Text = plastic.ToString(); 
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(deflectorselectat)
            {
                hooverX = e.X;  
                hooverY = e.Y;
                pictureBox1.Refresh();
            }
            else
            {
                hooverX = -100;
                hooverY = -100;
            }
        }
    }
}
