using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Nationala2024_peClase
{
    public partial class CosmosImagini : Form
    {
        public CosmosImagini()
        {
            InitializeComponent();
        }

        public List<PictureBox> selected = new List<PictureBox>(); //tracks selected images in real time
        public List<PictureBox> pictureboxes = new List<PictureBox>(); //tracks all pictureboxes in form
        int IndexCorpAles=-1; //tracks chosen Planet
        public List<string> Corpuri = new List<string>{ "Luna", "Soare", "Pamant" }; //tracks all possible planets
        public Dictionary<PictureBox,string> imagini = new Dictionary<PictureBox, string>(); //tracks the source for the image in each picturebox
        //you have to do this manually since c# doesnt care after the image has been loaded
        int nrImaginiCorpAles = 0; //trebuie sa fie 3 in final cu corpul ales
        private void button1_Click(object sender, EventArgs e) //Check if user selected all corectly
        {
            if(selected.Count==3)
            {
                bool selectatcorect = true;
                foreach (PictureBox p in selected)
                {
                    if (!imagini[p].Contains(Corpuri[IndexCorpAles]))
                    {
                        selectatcorect=false;
                    }
                }
                if (selectatcorect == false)
                {
                    MessageBox.Show("Nu ai selectat corect imaginile");
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Nu ai selectat destule imagini");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if((sender as Control).Tag==null)
            {
                Select(sender as PictureBox);
            }
            else
            {
                Deselect(sender as PictureBox);
            }
        }

        public void Deselect(PictureBox sender) //Deselectarea unei imagini
        {
            Console.WriteLine("deselected");
            sender.Tag = null;
            sender.BorderStyle = BorderStyle.None;
            selected.Remove(sender);
        }

        public void Select(PictureBox sender) //Selectarea unei imagini
        {
            Console.WriteLine("selected");
            sender.Tag = "selected";
            sender.BorderStyle = BorderStyle.Fixed3D;
            selected.Add(sender);
        }

        public int SelectRandomCorpIndex() //Selecteaza indexul unuia dintre cele doua corpuri ramase
        {
            int i = 0;
            do
            {
                Random rnd = new Random();
                i = rnd.Next(0, 3);
                Thread.Sleep(100);
            } while (i == IndexCorpAles);
            return i;
        }

        public void GetPictureBoxes() //Fill array for easier acces
        {
            foreach (PictureBox pictureBox in this.Controls.OfType<PictureBox>())
                pictureboxes.Add(pictureBox);
        }

        public bool EnoughPictures() //Verifica daca captchaul are cele 3 imagini corecte
        {
            if (nrImaginiCorpAles < 3)
                return false;
            return true;
        }

        public void AddCorpSelectat()
        {
            Random rnd = new Random();
            int index = 0;
            do
            {
                index = rnd.Next(0, pictureboxes.Count);
                Thread.Sleep(100);

            } while (pictureboxes[index].Image != null);
            pictureboxes[index].Image = Image.FromFile(GetImageDirectoryPath() + Corpuri[IndexCorpAles] + rnd.Next(1, 5).ToString() + ".png");
            imagini.Add(pictureboxes[index], Corpuri[IndexCorpAles]);
            nrImaginiCorpAles++;
        }

        public string GetImageDirectoryPath()
        {
            string path = AppContext.BaseDirectory;
            path += "ImaginiValidare";
            path += @"\";
            return path;
        }

        private void CosmosImagini_Load(object sender, EventArgs e)
        {
            GetPictureBoxes();
            IndexCorpAles = SelectRandomCorpIndex();
            textBox1.Text = "Ai de selectat trei imagini care contin ";
            textBox1.Text += Corpuri[IndexCorpAles];
            Random rnd = new Random();
            while (!EnoughPictures())
            {
                AddCorpSelectat();
            }
            foreach (PictureBox picturebox in pictureboxes)
            {
                if (picturebox.Image == null)
                {
                    string corp = Corpuri[SelectRandomCorpIndex()];
                    picturebox.Image = Image.FromFile(GetImageDirectoryPath() + corp + rnd.Next(1, 5).ToString() + ".png");
                    imagini.Add(picturebox, corp);
                }
            }

        }
    }
}
