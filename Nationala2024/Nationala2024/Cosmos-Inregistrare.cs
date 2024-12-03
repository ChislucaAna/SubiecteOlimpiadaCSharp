using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2024
{
    public partial class Cosmos_Inregistrare : Form
    {
        public Cosmos_Inregistrare()
        {
            InitializeComponent();
            string datadirectory = AppDomain.CurrentDomain.BaseDirectory;
            string modifiedDataDirectory = datadirectory.Replace(@"\bin\Debug", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", modifiedDataDirectory);
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
        }

        SqlConnection con;
        SqlCommand cmd;
        public static bool cont_valid = false;
        public static string email, nume, prenume, data_nasterii, parola;

        private void Cosmos_Inregistrare_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text != null || textBox2.Text != null ||
                textBox3.Text != null || textBox4.Text != null || textBox5.Text != null))
            {   
                MessageBox.Show("Toate campurile sunt obligatorii!");
                return;
            }

            if (!(textBox1.Text.Contains("@") && textBox1.Text.Contains(".")))
            {
                MessageBox.Show("Email-ul introdus nu este valid.");
                textBox1.Clear();
                return;
            }
            if(DateTime.Now.Subtract(dateTimePicker1.Value).TotalDays<365*7)
            {
                MessageBox.Show("Nu ai varsta necesara!");
                dateTimePicker1.Value = DateTime.Now;
                return;
            }

            if(textBox4.Text!=textBox5.Text)
            {
                MessageBox.Show("Parolele introiduse nu corespund!");
                textBox4.Clear();
                textBox5.Clear();
            }

            if(!parola_valida())
            {
                MessageBox.Show("Parola nu este valida.");
                textBox4.Clear();
                textBox5.Clear();
                return;
            }

            if(email_exista())
            {
                MessageBox.Show("Email-ul este utilizat deja de alt cont.");
                textBox1.Clear();
            }

            cont_valid = true;
            email = textBox1.Text;
            nume = textBox2.Text;
            prenume = textBox3.Text;
            data_nasterii = dateTimePicker1.Value.ToString();
            parola = textBox5.Text;

            this.Close();
        }

        public bool parola_valida()
        {
            bool numar=false, literamica=false, literamare=false;
            foreach(char c in textBox4.Text) 
            {
                if (Char.IsDigit(c)) numar = true;
                if (Char.IsUpper(c)) literamare = true;
                if (Char.IsLower(c)) literamica = true;
            }
            return literamica&& literamare && numar;
        }

        public bool email_exista()
        {
            bool ok = false;
            con.Open();
            cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE Email='{0}';", textBox1.Text), con);
            if (cmd.ExecuteScalar() != null)
            {
                ok = true;
            }
            con.Close();
            return ok;
        }
    }
}
