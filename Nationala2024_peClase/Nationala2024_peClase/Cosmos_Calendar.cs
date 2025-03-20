using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2024_peClase
{
    public partial class Cosmos_Calendar : Form
    {
        public Cosmos_Calendar()
        {
            InitializeComponent();
        }

        public DateTime data_curenta;
        int index = 0;
        int ziuaInCareIncepeLuna = 0;
        int ultimazi = 0;
        PictureBox senderForCalc;

        private void Cosmos_Calendar_Load(object sender, EventArgs e)
        {
            this.Text += " " + Autentificare.userDeRetinut.Email;
            data_curenta = DateTime.Parse("01.05.2024");
            label1.Text = data_curenta.ToString("MMMM yyyy");
            flowLayoutPanel1.Refresh();
            Db.Load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var m = MessageBox.Show("Doriti să părăsiți aplicația?", "Iesire", MessageBoxButtons.YesNoCancel);
            if (m == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            index = 0;
            ziuaInCareIncepeLuna = (int)data_curenta.DayOfWeek;
            DateTime d = data_curenta.AddMonths(1).AddDays(-1);
            ultimazi = Convert.ToInt32(d.ToString("dd"));
            Console.WriteLine(ultimazi.ToString());
            Console.WriteLine(d.ToString());
            foreach (PictureBox pictureBox in flowLayoutPanel1.Controls)
            {
                index++;
                pictureBox.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data_curenta = data_curenta.AddMonths(1);
            label1.Text = data_curenta.ToString("MMMM yyyy");
            flowLayoutPanel1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data_curenta = data_curenta.AddMonths(-1);
            label1.Text = data_curenta.ToString("MMMM yyyy");
            flowLayoutPanel1.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) //refresh casuta calendars
        {
            e.Graphics.Clear(Color.White);
            if (index - ziuaInCareIncepeLuna < ultimazi && index >= ziuaInCareIncepeLuna)
            {
                Console.WriteLine("setez tagul");
                (sender as PictureBox).Tag = (index - ziuaInCareIncepeLuna + 1).ToString(); //ziua curenta
                Console.WriteLine((sender as PictureBox).Tag);
                e.Graphics.DrawString((index - ziuaInCareIncepeLuna + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.Red), new Point(12, 12));
                string s = String.Format("{0}.{1}.{2}",
                                   index - ziuaInCareIncepeLuna + 1, data_curenta.Month, data_curenta.Year);
                //Console.WriteLine(s);
                DateTime datainregistrare = DateTime.ParseExact(s, "d.M.yyyy", null);
                //Console.WriteLine(datainregistrare.ToString());
                //Console.WriteLine("inregistrari:");
                var inregistrare = from i in Db.Inregistrari
                                   where i.Data.Date.Equals(datainregistrare.Date)
                                   select i;
                if (inregistrare.Any())
                {
                    Console.WriteLine(inregistrare.ToString());
                    var record = inregistrare.First();
                    string Path = AppContext.BaseDirectory;
                    //ImaginiZoddii si ImaginiLuna
                    if (record.CodFazaLuna == 4)
                    {
                        Image fazaLuna = Image.FromFile(Path + "ImaginiLuna" + @"\" + record.CodFazaLuna + ".jpeg");
                        Bitmap b1 = new Bitmap(fazaLuna, 30, 30);
                        Image zodie = Image.FromFile(Path + "ImaginiZodii" + @"\" + record.CodZodia + ".png");
                        Bitmap b2 = new Bitmap(zodie, 30, 30);
                        e.Graphics.DrawImage(b1, 20, 20);
                        e.Graphics.DrawImage(b2, 45, 20);
                    }
                    else
                    {
                        Image fazaLuna = Image.FromFile(Path + "ImaginiLuna" + @"\" + record.CodFazaLuna + ".png");
                        Bitmap b1 = new Bitmap(fazaLuna, 30, 30);
                        Image zodie = Image.FromFile(Path + "ImaginiZodii" + @"\" + record.CodZodia + ".png");
                        Bitmap b2 = new Bitmap(zodie, 30, 30);
                        e.Graphics.DrawImage(b1, 20, 20);
                        e.Graphics.DrawImage(b2, 45, 20);
                    }
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e) //nu e bun
        {
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e) //pt handle la click stanga/dreapta
        {
            if (e.Button == MouseButtons.Left) //afiseaza faza lunii
            {
                senderForCalc = sender as PictureBox;
                ImagineLuna.Refresh();
                
            }
            else
            {
                if (e.Button == MouseButtons.Right) //deschide cosmos constelatii
                {
                    COSMOS_Constelatii cOSMOS_Constelatii = new COSMOS_Constelatii();
                    cOSMOS_Constelatii.ShowDialog();
                }
            }
        }

        private void ImagineLuna_Paint(object sender, PaintEventArgs e)
        {
            if (senderForCalc == null) return;
            int y = data_curenta.Year;
            int m = data_curenta.Month;
            int d = Convert.ToInt32(senderForCalc.Tag);
            Console.WriteLine("tagul este:");
            Console.WriteLine(senderForCalc.Tag);
            Console.WriteLine(d);

            //Dacă luna este ianuarie sau februarie, scădeți 1 din an și adăugați 12 la lună. 

            if(m==1 || m==2)
            {
                y--;
                m = data_curenta.AddMonths(12).Month;
            }

            int A = y / 100;
            float B = A / 4;
            int C = 2 - A + (int)B;
            int E = (int)(365.25 * (y + 4716));
            int F =(int)( 30.6001 *(m + 1));
            double Jd = C + d + E + F - 1524.5;

            int ziDeLaUltimaLunaNoua = (int)(Jd - 2451549.5);
            double luni_noi = ziDeLaUltimaLunaNoua / 29.5;
            double zileInCiclu = (luni_noi - Convert.ToInt32(luni_noi)) * 29.5; //cate zile au trecut deja
            label10.Text = ((int)(zileInCiclu)).ToString();
            double pas = 360.0/ 29.5;
            int angle =(int)( zileInCiclu * pas);
            SolidBrush BRUSH = new SolidBrush(Color.FromArgb(75, Color.Black));
            e.Graphics.FillPie(BRUSH, new Rectangle(0, 0, (sender as PictureBox).Width, (sender as PictureBox).Height), 90, 90+angle);

            //cauti inregistrare si daca nu este adaugi tu
            string source = d + "." + m + "." + y;
            DateTime dt = DateTime.ParseExact(source, "d.M.yyyy", null);
            var inregistrare = from i in Db.Inregistrari
                               where i.Data.Date.Equals(dt.Date)
                               select i;
            Random rnd = new Random();
            if (!inregistrare.Any())
            {
                //adaugi tu
                Db.Inregistrari.Add(new Inregistrare(Db.Inregistrari.Count(), Autentificare.userDeRetinut.Email, dt, (int)(29.5 / zileInCiclu), rnd.Next(1, 13)));
            }
        }
    }
}
