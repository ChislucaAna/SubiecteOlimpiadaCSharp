using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2022
{
    public partial class Vizualizare : Form
    {
        public Vizualizare()
        {
            InitializeComponent();
        }

        IEnumerable<Masurare> query;
        int filtru =0;
        public static int indexHarta = -1;
        public static DateTime dataselectata;
        public static int clickedX, clickedY;
        Point starttraseu;
        Point punct1;
        Point punct2;

        private void Vizualizare_Load(object sender, EventArgs e)
        {
            Db.Init();
            foreach(Harta h in Db.harti)
            {
                comboBox1.Items.Add(h.NumeHarta);
            }
            foreach(var c in Db.masurari)
            {
                Console.WriteLine(c.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexHarta = comboBox1.SelectedIndex;
            string item = comboBox1.SelectedItem.ToString();
            if (item == "Harta Cluj-Napoca")
                item = "harta_cluj";
            else
            {
                item = item.ToLower();
                item = item.Replace(" ", "_");
            }
            
            Console.WriteLine(item);
            string path = AppContext.BaseDirectory;
            path += @"Harti\";
            path += item + ".png";
            pictureBox1.Image = Image.FromFile(path);
            pictureBox2.Image = Image.FromFile(path);   

            query = from masurare in Db.masurari
                        where masurare.DataMasurare.Date.Equals(dateTimePicker1.Value.Date)
                        select masurare;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            query = from masurare in Db.masurari
                    where masurare.DataMasurare.Date.Equals(dateTimePicker1.Value.Date)
                    select masurare;
            Console.WriteLine("refreshing");
            if (query == null)
                return;
            if (!(query.Any()))
                return;
            foreach (var m in query)
            {
                Console.WriteLine(m.ValoareMasurare);
                Pen p = new Pen(Color.Green);
                if (m.ValoareMasurare >= 20 && m.ValoareMasurare <= 40)
                    p.Color = Color.Yellow;
                else
                    if (m.ValoareMasurare > 40)
                    p.Color = Color.Red;
                if (filtru == 0 || (filtru == 1 && m.ValoareMasurare < 20) || (filtru == 2 && m.ValoareMasurare >= 20 && m.ValoareMasurare <= 40) || (filtru == 3
                    && m.ValoareMasurare > 40))
                {
                    e.Graphics.DrawEllipse(p, new Rectangle(new Point(m.PozitieX, m.PozitieY), new Size(20, 20)));
                    e.Graphics.DrawString(m.ValoareMasurare.ToString(),new Font("Arial",12),new SolidBrush(p.Color), new Point(m.PozitieX, m.PozitieY));
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtru = comboBox2.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filtru = 0;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataselectata = dateTimePicker1.Value.Date;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e) //nui bun
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            query = from masurare in Db.masurari
                    where masurare.DataMasurare.Date.Equals(dateTimePicker1.Value.Date)
                    select masurare;
            Console.WriteLine("refreshing");
            if (query == null)
                return;
            if (!(query.Any()))
                return;
            foreach (var m in query)
            {
                Console.WriteLine(m.ValoareMasurare);
                Pen p = new Pen(Color.Green);
                if (m.ValoareMasurare >= 20 && m.ValoareMasurare <= 40)
                    p.Color = Color.Yellow;
                else
                    if (m.ValoareMasurare > 40)
                    p.Color = Color.Red;
                if (filtru == 0 || (filtru == 1 && m.ValoareMasurare < 20) || (filtru == 2 && m.ValoareMasurare >= 20 && m.ValoareMasurare <= 40) || (filtru == 3
                    && m.ValoareMasurare > 40))
                {
                    e.Graphics.DrawEllipse(p, new Rectangle(new Point(m.PozitieX, m.PozitieY), new Size(20, 20)));
                    e.Graphics.DrawString(m.ValoareMasurare.ToString(), new Font("Arial", 12), new SolidBrush(p.Color), new Point(m.PozitieX, m.PozitieY));
                }
            }

            if (punct1 == null || punct2 == null || starttraseu == null)
                return;
            else
            {
                HashSet<Point> set = new HashSet<Point>();
                set.Add(punct1);    
                set.Add(punct2);
                set.Add(starttraseu);
                foreach (Point p in set)
                {
                    Console.WriteLine(p.X +" "+p.Y);
                }
                if(set.Count==2)
                {
                    Point p1 = new Point(set.First().X, set.First().Y);
                    set.Remove(p1);
                    Point p2 = new Point(set.First().X, set.First().Y);
                    e.Graphics.DrawLine(new Pen(Color.Red), p1, p2);
                }
                else
                {
                    double dp1p2= Math.Pow((punct1.X - starttraseu.X ),2) + Math.Pow((punct1.Y-  starttraseu.Y),2);
                    double dp1p3 = Math.Pow((punct2.X- starttraseu.X) ,2)+ Math.Pow((punct2.Y  - starttraseu.Y),2);
                    if(dp1p2<dp1p3)
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red), starttraseu, punct1);
                    }
                    else
                    {
                        e.Graphics.DrawLine(new Pen(Color.Red), starttraseu, punct2);
                    }
                    e.Graphics.DrawLine(new Pen(Color.Red), punct1,punct2);
                }
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            query = from masurare in Db.masurari
                    where masurare.DataMasurare.Date.Equals(dateTimePicker1.Value.Date)
                    select masurare;
            if (query == null) Console.WriteLine("null query");
            var query2 = from n in query
                         where n.PozitieX >= e.X - 15 && n.PozitieX <= e.X + 15 && n.PozitieY >= e.Y - 15
&& n.PozitieY <= e.Y + 15
                         select n;
            if (query2.Any())
            {
                //s-a dat click pe punct deja existent e ok
                starttraseu= new Point(query2.First().PozitieX,query2.First().PozitieY);
                var ordered = query.OrderBy(n=>n.ValoareMasurare*(-1)).ToList();
                punct1 = new Point(ordered.First().PozitieX, ordered.First().PozitieY) ;
                punct2= new Point(ordered[1].PozitieX, ordered[1].PozitieY);

                pictureBox2.Refresh();
            }
            else
            {
                MessageBox.Show(@"Selectați un punct de pe hartă corespunzător\r\nunei măsurări existente în baza de date!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(indexHarta==-1)
            {
                MessageBox.Show("Selectati o harta inainte de adaugarea unei masuratori");
                return;
            }    
            clickedX = e.X;
            clickedY = e.Y;
            if(query.Any(n=> n.PozitieX>=e.X-5 && n.PozitieX <= e.X +5 && n.PozitieY>=e.Y-5
            && n.PozitieY<=e.Y+5))
            {
                MessageBox.Show("Exista deja masuratoare in zona aia");
                return;
            }

            AdaugaMasurare adaugaMasurare = new AdaugaMasurare();
            adaugaMasurare.ShowDialog();
            pictureBox1.Refresh();
            pictureBox2.Refresh();  
        }
    }
}
