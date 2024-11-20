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

namespace Nationala2024
{
    public partial class Cosmos_Calendar : Form
    {
        public Cosmos_Calendar()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
        }

        public Cosmos_Calendar(string email)
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
            this.email = email;
        }

        public class casuta
        {
            public Button b;
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
        bool loaded = false;

        private void Cosmos_Calendar_Load(object sender, EventArgs e)
        {
            this.Text = email;

            generate_calendar();
            loaded = true;
            //this.Refresh();
        }

        public void generate_calendar() 
        {
            Array.Clear(calendar,0,calendar.Length);

            get_month_data();
            DateTime first_day = new DateTime(data_curenta.Year, data_curenta.Month, 1);
            int day_of_week = Convert.ToInt32(first_day.DayOfWeek);
            //skip previous days of week till 1st date of the month
            for(int i=1; i<day_of_week; i++)
            {
                Button but = new Button();
                but.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                but.ForeColor = System.Drawing.Color.Purple;
                but.Tag = "0";
                but.Size = new System.Drawing.Size(70, 70);
                but.Text = "";
                flowLayoutPanel1.Controls.Add(but);
            }
            //start generating month days
            for (int i = 1; i <= DateTime.DaysInMonth(data_curenta.Year, data_curenta.Month); i++)
            {
                if (calendar[i]==null)
                    calendar[i] = new casuta(null,null);
                calendar[i].b = new Button();
                calendar[i].b.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                calendar[i].b.ForeColor = System.Drawing.Color.Purple;
                calendar[i].b.Tag = 0;
                calendar[i].b.Size = new System.Drawing.Size(70, 70);
                calendar[i].b.Text = i.ToString();
                calendar[i].b.Tag = i.ToString(); ;
                calendar[i].b.Click += new System.EventHandler(but_Click);

                flowLayoutPanel1.Controls.Add(calendar[i].b);

            }
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

        public void but_Click(object sender, EventArgs e)
        {
            Button but = sender as Button;
            MessageBox.Show(but.Tag.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Doriti să părăsiți aplicația?", "Iesire", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
                this.Close();
        }

        private void Cosmos_Calendar_Paint(object sender, PaintEventArgs e)
        {
            if (loaded ==true)
            {
                MessageBox.Show("p");
                for (int i = 1; i < calendar.Length - 1; i++)
                {
                    //MessageBox.Show(i.ToString());
                    Graphics h = calendar[i].b.CreateGraphics();
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
                                Bitmap bitmap = new Bitmap(20, 20);
                                Graphics g = Graphics.FromImage(bitmap);
                                g.DrawImage(img, new PointF(0, 0));
                                h.DrawImage(bitmap, new PointF(0, 0));
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
                                Bitmap bitmap = new Bitmap(20, 20);
                                Graphics g = Graphics.FromImage(bitmap);
                                g.DrawImage(img, new PointF(0, 0));
                                h.DrawImage(bitmap, new PointF(0, 0));
                            }
                        }
                    }
                }
            }
        }
    }
}
