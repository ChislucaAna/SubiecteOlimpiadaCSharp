using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2018
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool automat = true;
        int currentimage = 1;

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Load();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = from u in Db.utilizatori
                        where(u.EmailUtilizator==textBox1.Text && u.ParolaUtilizator==textBox2.Text)
                        select u;
            if(query.Any())
            {
                eLearning1918_Elev eLearning1918_Elev = new eLearning1918_Elev();
                eLearning1918_Elev.ShowDialog();
            }
            else
            {
                MessageBox.Show("Eroare de autentificare!");
                textBox1.Clear(); textBox2.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            automat = !automat;
            if (automat)
                button4.Text = "Manual";
            else
                button4.Text = "Automat";
            if (!automat)
                timer1.Stop();
            else
                timer1.Start();
            if(automat)
            {
                button2.Enabled = false; button3.Enabled = false;   
            }
            else
            {
                button2.Enabled = true; button3.Enabled = true;
            }
        }

        public void refreshimage()
        {
            string path = AppContext.BaseDirectory;
            Console.WriteLine(path);
            path += "imaginislideshow";
            path+=@"\";
            path += currentimage;
            path += ".jpg";
            pictureBox1.Image=Image.FromFile(path);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(currentimage<5)
            {
                currentimage++;
            }
            else
            {
                currentimage = 1;
            }
            refreshimage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentimage > 1)
            {
                currentimage--;
                button3.Enabled = true;
                if(currentimage==1)
                    button2.Enabled = false;
            }
            refreshimage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentimage < 5)
            {
                currentimage++;
                button2.Enabled=true;   
                if (currentimage == 5)
                    button3.Enabled = false;
            }
            refreshimage();
        }
    }
}
