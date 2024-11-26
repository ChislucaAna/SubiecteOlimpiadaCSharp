using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Nationala2024
{
    public partial class Cosmos_Calendar : Form
    {
        public Cosmos_Calendar()
        {
            InitializeComponent();
            string datadirectory = AppDomain.CurrentDomain.BaseDirectory;
            string modifiedDataDirectory = datadirectory.Replace(@"\bin\Debug", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", modifiedDataDirectory);
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
        }

        public Cosmos_Calendar(string email)
        {
            InitializeComponent();
            string datadirectory = AppDomain.CurrentDomain.BaseDirectory;
            string modifiedDataDirectory = datadirectory.Replace(@"\bin\Debug", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", modifiedDataDirectory);
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
            this.email = email;
        }

        public class casuta
        {
            public PictureBox b;
            public string cod_luna;
            public string cod_zodia;

            public casuta(string luna, string zodia)
            {
                this.cod_luna = luna;
                this.cod_zodia = zodia;
            }   
        }


        //email ul utilizatorului curent
        string email ="popescum@yahoo.com";
        SqlConnection con;
        SqlCommand cmd;
        DateTime data_curenta= DateTime.Now;
        casuta[] calendar = new casuta[32];

        private void Cosmos_Calendar_Load(object sender, EventArgs e)
        {
            this.Text = email;
            label8.Text = data_curenta.Month.ToString() + " " + data_curenta.Year.ToString();
            generate_calendar();
        }

        public void generate_calendar() 
        {
            flowLayoutPanel1.Controls.Clear();
            Array.Clear(calendar,0,calendar.Length);

            get_month_data();
            DateTime first_day = new DateTime(data_curenta.Year, data_curenta.Month, 1);

            int day_of_week = Convert.ToInt32(first_day.DayOfWeek);
            //skip previous days of week till 1st date of the month
            //astea is cum ar veni zile din luna anterioara
            for(int i=1; i<day_of_week; i++)
            {
                PictureBox but = new PictureBox();
                but.Tag = "0";
                but.Size = new System.Drawing.Size(70, 70);
                flowLayoutPanel1.Controls.Add(but);
            }
            //start generating month days
            //astea is zile in luna curenta
            for (int i = 1; i <= DateTime.DaysInMonth(data_curenta.Year, data_curenta.Month); i++)
            {
                if (calendar[i]==null)
                    calendar[i] = new casuta(null, null);
                
                calendar[i].b = new PictureBox();
                calendar[i].b.BackColor = Color.Pink;
                calendar[i].b.Size = new System.Drawing.Size(70, 70);
                calendar[i].b.Tag = i.ToString();
                calendar[i].b.Paint += new System.Windows.Forms.PaintEventHandler(paint);
                calendar[i].b.Click += new System.EventHandler(onclick);
                calendar[i].b.MouseHover += new System.EventHandler(onhoover);

                flowLayoutPanel1.Controls.Add(calendar[i].b);

            }
            this.Refresh();
        }

        public void onclick(object sender, EventArgs e)
        {
            MessageBox.Show("se calculeaza faza luni..");
            int zi = Convert.ToInt32((sender as PictureBox).Tag.ToString());
            int faza = calculeaza_faza_lunii(zi);
            if (!exista_inregistrare(zi))
            {
                genereaza_inregistrare(zi,faza);
            }
            update_picturebox(faza);
        }

        public bool exista_inregistrare(int i)
        {
            if (Convert.ToInt32(calendar[i].cod_luna) != 0)
                return true;
            else
                return false;
        }

        public void genereaza_inregistrare(int zi, int faza)
        {
            con.Open();
            cmd = new SqlCommand(String.Format("INSERT INTO Inregistrari VALUES ('{0}','{1}', {2}, {3})",
                email,data_curenta,faza,0),con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void update_picturebox(int faza)
        {
            string path = AppContext.BaseDirectory + @"\" + "ImaginiLuna";
            var files = Directory.EnumerateFiles(path);
            foreach (string currentFile in files)
            {
                string fileName = currentFile;
                if (fileName.Contains(faza.ToString()))
                {
                    pictureBox1.Image = Image.FromFile(fileName);
                }
            }
        }

        public int calculeaza_faza_lunii(int zi)
        {
            int an = data_curenta.Year;
            int luna = data_curenta.Month;
            if (luna == 1 || luna == 2)
            {
                an--;
                luna = (luna + 12) % 12;
            }
            int A = an / 4;
            int B =  A/4;
            int C = 2 - A + B;
            int E = Convert.ToInt32(365.25 * (an + 4716));
            int F = Convert.ToInt32(30.6001 * (luna + 1));
            float JD = C + zi + E + F - 1524;
            int zile_de_la_ultima_luna_noua = Convert.ToInt32(JD - 2451549.5);
            double nr_de_lune_Noi = zile_de_la_ultima_luna_noua / 29.5;
            int result = Convert.ToInt32((nr_de_lune_Noi - Convert.ToInt32(nr_de_lune_Noi)) * 29.5);
            int faza = Convert.ToInt32(8 / 29.5) * result;
            return result;
        }

        public void onhoover(object sender, EventArgs e) //show info about zodie
        {

        }

        public void get_month_data()
        {
            DateTime first_day = new DateTime(data_curenta.Year, data_curenta.Month, 1);
            int zile_luna = DateTime.DaysInMonth(data_curenta.Year, data_curenta.Month);
            con.Open();
            SqlCommand cmd = new SqlCommand(String.Format("SELECT * FROM Inregistrari " +
                "WHERE (Data>='{0}' AND Data<='{1}')", first_day, first_day.AddDays(zile_luna)),con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                DateTime data = Convert.ToDateTime(reader[2].ToString());
                int zi = data.Day;
                calendar[zi] = new casuta(reader[3].ToString(), reader[4].ToString());
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriti să părăsiți aplicația?", "Iesire", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
                this.Close();
        }

        private void paint(object sender, PaintEventArgs e)
        {
            int i = Convert.ToInt32((sender as PictureBox).Tag.ToString());
            Graphics h = e.Graphics;
            DateTime first_day = new DateTime(data_curenta.Year, data_curenta.Month, 1);
            int day_of_week = Convert.ToInt32(first_day.DayOfWeek);
            h.DrawString(i.ToString(), new Font("Arial", 25), new SolidBrush(Color.Black), new Point(0, 0));
            if (calendar[i].cod_luna != null)
            {
                string path = AppContext.BaseDirectory + @"\" + "ImaginiLuna";
                var files = Directory.EnumerateFiles(path);
                foreach (string currentFile in files)
                {
                    string fileName = currentFile;
                    if (fileName.Contains(calendar[i].cod_luna))
                    {
                        Image img = Image.FromFile(fileName);
                        Bitmap bitmap = new Bitmap(img, 20, 20);
                        h.DrawImage(bitmap, new PointF(30, 30));
                    }
                }
            }
            if (calendar[i].cod_zodia != null)
            {
                string path = AppContext.BaseDirectory + @"\" + "ImaginiZodii";
                var files = Directory.EnumerateFiles(path);
                foreach (string currentFile in files)
                {
                    string fileName = currentFile;
                    if (fileName.Contains(calendar[i].cod_zodia))
                    {
                        Image img = Image.FromFile(fileName);
                        Bitmap bitmap = new Bitmap(img, 20, 20);
                        h.DrawImage(bitmap, new PointF(30, 30));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data_curenta = data_curenta.AddMonths(-1);
            label8.Text = data_curenta.Month.ToString() + " " + data_curenta.Year.ToString();
            generate_calendar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data_curenta = data_curenta.AddMonths(1);
            label8.Text = data_curenta.Month.ToString() + " "+data_curenta.Year.ToString();
            generate_calendar();
        }

    }
}
