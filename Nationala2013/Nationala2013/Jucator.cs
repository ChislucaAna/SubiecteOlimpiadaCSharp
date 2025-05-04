using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Nationala2013
{
    public partial class Jucator : Form
    {
        public Jucator()
        {
            InitializeComponent();
            this.KeyPreview = true;
            Db.Init();
        }

        Bitmap sursa;
        int nrLinii;
        int nrColoane;
        int piese_adaugate;
        Random rnd;


        public class Componenta
        {
            public PictureBox piesa; //imaginea in value, in tag ai indexul de ordine
            public int unghiRotatie;

            public Componenta(PictureBox piesa, int unghiRotatie)
            {
                this.piesa = piesa;
                this.unghiRotatie = unghiRotatie;
            }
        }

        List<Componenta> componente = new List<Componenta>();

        public Componenta componentaSelectata;
        Control s;
        int x;
        int y;
        bool apasat;
        int secunde = 0;
        int nr_mutari_efectuate;
        private void Incarca_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = AppContext.BaseDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sursa = new Bitmap(Image.FromFile(openFileDialog.FileName), model.Width, model.Height);
                model.Image = sursa;
            }

        }

        public void valideazaInput()
        {
            //only numbers between 4 and 10

            bool ok = true;
            if (textBox1.Text.Length != 0)
            {
                if (int.TryParse(textBox1.Text, out int result) == false || result < 1 || result > 10)
                {
                    ok = false;
                    textBox1.Clear();
                }
            }

            if (textBox2.Text.Length != 0)
            {
                if (int.TryParse(textBox1.Text, out int result1) == false || result1 < 1 || result1 > 10)
                {
                    ok = false;
                    textBox2.Clear();
                }
            }

            if (!ok)
            {
                MessageBox.Show("Textul introdus in e invalid.Introduceti un numar intre 4 si 10");
                return;
            }

            nrLinii = Convert.ToInt32(textBox1.Text);
            nrColoane = Convert.ToInt32(textBox2.Text);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //no input
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Enter puzzle bounds in textboxes before attempting to start");
                return;
            }

            flowLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.ColumnCount = nrColoane;
            tableLayoutPanel1.RowCount = nrLinii;

            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
               
            }

            // Set rows to AutoSize
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            valideazaInput();
            separaPeComponente();
            timer1.Start();

        }


        public void Select(object sender, MouseEventArgs e)
        {
            if (componentaSelectata == null)
            {
                var query = from c in componente
                            where c.piesa == (sender as PictureBox)
                            select c;
                if (query.Any())
                {
                    componentaSelectata = query.First();
                }
                s = (sender as Control).Parent;
                Console.WriteLine(s.ToString());
            }
        }


        public void Deselect(object sender, MouseEventArgs e)
        {
            if (componentaSelectata != null)
            {
                if ((sender as Control).GetType() == typeof(TableLayoutPanel))
                {
                    TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)sender;
                    tableLayoutPanel.Controls.Add(componentaSelectata.piesa, piese_adaugate % nrColoane, piese_adaugate / nrLinii);
                    componentaSelectata = null;
                    if (piese_adaugate < nrLinii * nrColoane && s.GetType()==typeof(FlowLayoutPanel))
                    {
                        piese_adaugate++;
                    }
                    nr_mutari_efectuate++;
                    Mutaritxt.Text = nr_mutari_efectuate.ToString();
                    this.Refresh();
                }
                else
                {
                    if ((sender as Control).GetType() == typeof(FlowLayoutPanel))
                    {
                        FlowLayoutPanel tableLayoutPanel = (FlowLayoutPanel)sender;
                        tableLayoutPanel.Controls.Add(componentaSelectata.piesa);
                        componentaSelectata = null;
                        if (piese_adaugate > 0 && s.GetType() == typeof(TableLayoutPanel))
                        {
                            piese_adaugate--;
                        }
                        nr_mutari_efectuate++;
                        Mutaritxt.Text = nr_mutari_efectuate.ToString();
                        this.Refresh();
                    }
                }
            }
        }

        public void separaPeComponente()
        {
            int widthPiesa = sursa.Width / nrColoane;
            int heightPiesa = sursa.Height / nrLinii;
            int index = 1;
            for (int i = 1; i <= nrLinii; i++)
            {
                for (int j = 1; j <= nrColoane; j++)
                {
                    rnd = new Random();
                    int v = rnd.Next(0, 4);

                    Bitmap piesa = new Bitmap(widthPiesa, heightPiesa);
                    piesa.Tag = index.ToString();
                    Graphics graphics = Graphics.FromImage(piesa);
                    graphics.DrawImage(sursa, -widthPiesa * (j - 1), -heightPiesa * (i - 1));
                    if (v == 1)
                        piesa.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    if (v == 2)
                        piesa.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    if (v == 3)
                        piesa.RotateFlip(RotateFlipType.Rotate270FlipNone);

                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Tag = piesa.Tag;
                    pictureBox.Size = piesa.Size;
                    pictureBox.Image = piesa;
                    pictureBox.MouseDown += Select;
                    componente.Add(new Componenta(pictureBox, v));

                    index++;


                    Thread.Sleep(20);
                }
            }

            var shuffled = componente.OrderBy(n => rnd.Next());
            index = 1;
            foreach (var sh in shuffled)
                flowLayoutPanel1.Controls.Add(sh.piesa);

        }

        private void Roteste_Click(object sender, EventArgs e)
        {
            if (componentaSelectata == null)
            {
                MessageBox.Show("Select a puzzle piece before attempting rotation");
                return;
            }
            componentaSelectata.unghiRotatie++;
            componentaSelectata.unghiRotatie %= 4;
            int v = componentaSelectata.unghiRotatie;
            var img = componentaSelectata.piesa.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            componentaSelectata.piesa.Size = img.Size;
            componentaSelectata.piesa.Image = img;
            Console.WriteLine(componentaSelectata.piesa.Tag.ToString());
        }

        private void general_MouseMove(object sender, MouseEventArgs e)
        {
            if (componentaSelectata != null)
            {
                x = e.X;
                y = e.Y;
                (sender as Control).Refresh();

            }
        }

        private void paint_general(object sender, PaintEventArgs e)
        {
            if (componentaSelectata != null)
                e.Graphics.DrawImage(componentaSelectata.piesa.Image, x, y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            secunde++;
            TimeSpan t = TimeSpan.FromSeconds(secunde);
            Timptxt.Text = t.ToString(@"hh\:mm\:ss"); // "01:01:01"
            if (piese_adaugate == nrLinii * nrColoane)
            {
                int index = 1;
                bool ok = true;
                foreach (PictureBox p in tableLayoutPanel1.Controls.OfType<PictureBox>())
                {

                    var query = from c in componente
                                where c.piesa == p
                                select c;
                    

                    if (p.Tag.ToString() == index.ToString() && query.First().unghiRotatie%4==0)
                    {
                        index++;
                    }
                    else
                    {
                        ok = false;
                    }
                }
                if(ok)
                {
                    timer1.Stop();
                    MessageBox.Show("Ai castigat");
                    Db.scoruri.Add(new Scor(Form1.IDUtilizatorLogat, secunde
    , piese_adaugate, nrLinii * nrColoane));
                    Db.Save();
                    this.Close();
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Ai pierdut");
                    this.Close();
                }
            }
        }

        private void Timptxt_Click(object sender, EventArgs e)
        {

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Db.scoruri.Add(new Scor(Form1.IDUtilizatorLogat, secunde
                , piese_adaugate,nrLinii*nrColoane));
            timer1.Stop();
            this.Close();
            Db.Save();
        }
    }
}
