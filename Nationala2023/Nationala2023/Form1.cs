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
using MessagingToolkit.QRCode.Codec.Data;

namespace Nationala2023
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        public static List<Utilizator> utilizatori = new List<Utilizator>();
        public static List<Rezultat> rezultate = new List<Rezultat>();
        public static Utilizator utilizatorLogat = new Utilizator("ion@oti.ro", "ion", "noi");

        private void Form1_Load(object sender, EventArgs e)
        {
            string line;
            StreamReader streamReader = new StreamReader("Utilizatori.txt");
            while ((line = streamReader.ReadLine())!=null)
            {
                string[] strings = line.Split(';');
                utilizatori.Add(new Utilizator(strings[0], strings[1], strings[2]));
            }
            streamReader = new StreamReader("Rezultate.txt");
            while ((line = streamReader.ReadLine())!=null)
            {
                string[] strings = line.Split(';');
                DateTime dt = DateTime.ParseExact(strings[3], "dd.mm.yyyy", null);
                rezultate.Add(new Rezultat(Convert.ToInt16(strings[0]), strings[1], Convert.ToInt16(strings[2]),dt));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (utilizatorLogat == null)
            {
                IEnumerable<Utilizator> cauta =
                from utilizator in utilizatori
                where (utilizator.email == textBox1.Text && utilizator.parola == textBox2.Text)
                select utilizator;
                if (cauta.Any())
                {
                    AlegeJoc alegeJoc = new AlegeJoc();
                    alegeJoc.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Date de\r\nautentificare invalide!");
                    textBox1.Clear();
                    textBox2.Clear();
                    pictureBox1.Image = null;
                }
            }
            else
            {
                AlegeJoc alegeJoc = new AlegeJoc();
                alegeJoc.ShowDialog();
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppContext.BaseDirectory;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                MessagingToolkit.QRCode.Codec.QRCodeDecoder objDecodare = new
MessagingToolkit.QRCode.Codec.QRCodeDecoder();
                string sirCodare = objDecodare.decode(new
                MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(pictureBox1.Image as Bitmap));
                Console.WriteLine(sirCodare);
                string[] bucati = sirCodare.Split(System.Environment.NewLine.ToCharArray());
                textBox1.Text = bucati[1];
                textBox2.Text = bucati[2];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Inregistrare inregistrare = new Inregistrare();
            inregistrare.ShowDialog();
        }
    }
}
