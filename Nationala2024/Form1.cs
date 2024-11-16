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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CosmosDB.mdf;Integrated Security=True");
        }

        SqlConnection con;
        SqlCommand cmd;
        StreamReader reader;

        private void Form1_Load(object sender, EventArgs e)
        {
            reader = new StreamReader("Utilizatori.txt");
            string line;
            while ((line=reader.ReadLine())!=null)
            {
                string[] fields = line.Split(';');
                DateTime datetime = DateTime.ParseExact(fields[4], "MM.dd.yyyy",null);
                string[] bucati = datetime.ToString().Split(' ');

                con.Open();
                cmd = new SqlCommand(String.Format("INSERT INTO Utilizatori VALUES('{0}','{1}','{2}','{3}','{4}');",
                    fields[0], fields[1], fields[2], fields[3], bucati[0]), con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

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
        }
    }
}
