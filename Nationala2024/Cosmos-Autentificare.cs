using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace Nationala2024
{
    public partial class Cosmos_Autentificare : Form
    {
        public Cosmos_Autentificare()
        {
            InitializeComponent();
            string datadirectory = AppDomain.CurrentDomain.BaseDirectory;
            string modifiedDataDirectory = datadirectory.Replace(@"\bin\Debug", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", modifiedDataDirectory);
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
        }

        string parola;
        string email;
        SqlConnection con;
        SqlCommand cmd;
        StreamReader reader;

        public void load_db()
        {
            reader = new StreamReader("Utilizatori.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                DateTime datetime = DateTime.ParseExact(fields[4], "MM.dd.yyyy", null);
                string[] bucati = datetime.ToString().Split(' ');

                con.Open();
                cmd = new SqlCommand(String.Format("INSERT INTO Utilizatori VALUES('{0}','{1}','{2}','{3}','{4}');",
                    fields[0], fields[1], fields[2], fields[3], bucati[0]), con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            reader.Close();

            reader = new StreamReader("Inregistrari.txt");
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                DateTime datetime = DateTime.ParseExact(fields[2], "dd/MM/yyyy", null);
                string[] bucati = datetime.ToString().Split(' ');

                con.Open();
                cmd = new SqlCommand(String.Format("INSERT INTO Inregistrari VALUES('{0}','{1}',{2},{3});",
                    fields[0], bucati[0], fields[1], fields[3]), con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            reader.Close();

        }


        private void Cosmos_Autentificare_Load(object sender, EventArgs e)
        {
            //load_db();
            if (File.Exists("last_account.txt"))
            {
                reader = new StreamReader("last_account.txt");
                string last="";
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    last = line.Trim();
                }
                reader.Close();
                if(last!=null)
                    textBox1.Text = last;   
            }
        }

        public static string criptare_parola(string parola)
        {
            string parola_criptata="";
            foreach(char c in parola)
            {
                if(Char.IsLower(c))
                {
                    parola_criptata += (Convert.ToChar('a' + c % 26)).ToString();
                }
                else if (Char.IsUpper(c))
                {
                    parola_criptata += (Convert.ToChar('A' + c % 26)).ToString();
                }
                else if (Char.IsDigit(c))
                {
                    parola_criptata += (c % 10).ToString();
                }
            }
            return parola_criptata;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                FileStream sb = new FileStream("last_account.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(sb);
                sw.Write(textBox1.Text);
                sw.Close();
            }

            email = textBox1.Text;
            parola = textBox2.Text;
            parola = criptare_parola(parola);
            con.Open();
            cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE Email='{0}' AND Parola='{1}';",email,parola),con);
            if(cmd.ExecuteScalar()!=null)
            {
                con.Close();
                Cosmos_Imagini cosmos_Imagini = new Cosmos_Imagini();
                this.Hide();
                cosmos_Imagini.ShowDialog();
                //daca imaginile au fost selectate corect
                this.Show();
                if (Cosmos_Inregistrare.cont_valid) //s-a intrat din inregistrare
                    add_new_acc(); //acum utilizatorul trebuie sa se autentifice
                else //s-a intrat din auth
                {
                    Cosmos_Calendar cal = new Cosmos_Calendar(textBox1.Text);
                    this.Hide();
                    cal.ShowDialog();
                    this.Show();
                }

            }
            else
            {
                con.Close();
                MessageBox.Show("Eroare de autentificare!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        public void add_new_acc()
        {
            con.Open();
            string password = criptare_parola(Cosmos_Inregistrare.parola);
            cmd = new SqlCommand(String.Format("INSERT INTO Utilizatori VALUES('{0}','{1}','{2}','{3}','{4}');",
                Cosmos_Inregistrare.email, Cosmos_Inregistrare.nume, Cosmos_Inregistrare.prenume, password, Cosmos_Inregistrare.data_nasterii), con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cosmos_Inregistrare cosmos_Inregistrare = new Cosmos_Inregistrare();
            this.Hide();
            cosmos_Inregistrare.ShowDialog();
            if(Cosmos_Inregistrare.cont_valid==true)
            {
                Cosmos_Imagini cosmos_Imagini = new Cosmos_Imagini();
                cosmos_Imagini.ShowDialog();
            }
            this.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
