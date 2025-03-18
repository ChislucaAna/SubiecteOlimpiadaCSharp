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

        private void Cosmos_Calendar_Load(object sender, EventArgs e)
        {
            this.Text += " "+ Autentificare.userDeRetinut.Email;
            data_curenta = DateTime.Parse("01.05.2024");
            label1.Text = data_curenta.ToString("MMMM yyyy");
            flowLayoutPanel1.Refresh();
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
            if (index-ziuaInCareIncepeLuna < ultimazi && index >= ziuaInCareIncepeLuna)
                e.Graphics.DrawString((index - ziuaInCareIncepeLuna + 1).ToString(), new Font("Arial", 12), new SolidBrush(Color.Red), new Point(12, 12));
            else
            {
                Console.WriteLine(index);
                e.Graphics.Clear(Color.White);
            }
        }
    }
}
