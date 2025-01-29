using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2016
{
    public partial class Optiuni : Form
    {
        public Optiuni()
        {
            InitializeComponent();
        }

        public static int total_kcal=0;
        public static int total_pret=0;
        bool first_produs = true;
        public int id_subcomanda = 0;

        public void UpdateLabels()
        {
            textBox9.Text = Autentificare_client.client_Autentificat.kcal_zilnice.ToString();
            textBox5.Text = Autentificare_client.client_Autentificat.kcal_zilnice.ToString();
            textBox6.Text = total_kcal.ToString();
            textBox7.Text = total_pret.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int S = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text);
                if (S < 250)
                    textBox4.Text = "1800";
                else
                    if (S >= 250 && S <= 275)
                    textBox4.Text = "2200";
                else
                    textBox4.Text = "2500";
                Autentificare_client.client_Autentificat.kcal_zilnice=Convert.ToInt32(textBox4.Text);
                //clear db
                StreamWriter streamWriter = new StreamWriter("clienti.txt",false);
                streamWriter.WriteLine("");
                streamWriter.Close();

                //refresh db
                streamWriter = new StreamWriter("clienti.txt");
                foreach (Client c in Form1.clienti)
                {
                    streamWriter.WriteLine(c.ToString());
                    streamWriter.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Datele introduse nu sunt valide");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vizualizare_comanda vizualizare_Comanda = new Vizualizare_comanda();
            vizualizare_Comanda.ShowDialog();
        }

        private void Optiuni_Load(object sender, EventArgs e)
        {
            foreach(Produs p in Form1.meniu)
            {
                dataGridView1.Rows.Add(p.id_produs,p.denumire_produs,p.descriere,p.pret,p.kcal,"0");
            }
            UpdateLabels();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.ColumnCount-1)
                {
                    if (Convert.ToInt32(dataGridView1[dataGridView1.ColumnCount - 1,e.RowIndex].Value) == -1)
                    {
                        MessageBox.Show("Cantitate negativa!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("produs adaugat cu succes");
                        total_kcal += Convert.ToInt32(dataGridView1[4,e.RowIndex].Value) * Convert.ToInt32(dataGridView1[5, e.RowIndex].Value);
                        total_pret += Convert.ToInt32(dataGridView1[3,e.RowIndex].Value) * Convert.ToInt32(dataGridView1[5, e.RowIndex].Value);
                        if (first_produs) //primul produs in comanda curenta    
                        {
                            MessageBox.Show("A fost creeata o comanda noua.");
                            Form1.comenzi.Add(new Comanda(Form1.index_comanda.ToString(), Autentificare_client.client_Autentificat.id_client, DateTime.Now));
                            //pregatesti pt urmatoarea
                            Form1.index_comanda++;
                            first_produs = false;
                        }
                        MessageBox.Show("A fost creeata o subcomanda noua.");
                        Form1.subcomenzi.Add(new Subcomanda(id_subcomanda++, (Form1.index_comanda - 1).ToString(),
                            Form1.meniu[Convert.ToInt32(dataGridView1[0, e.RowIndex].Value)].id_produs,
                            Convert.ToInt32(dataGridView1[5, e.RowIndex].Value)));
                        UpdateChart();
                        UpdateLabels();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Eroare la adaugarea in cos");
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateChart()
        {
            foreach (Subcomanda s in Form1.subcomenzi) {
                chart1.Series[0].Points.AddXY(Form1.meniu[s.id_produs].denumire_produs,
                    Form1.meniu[s.id_produs].kcal);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clear previous generation
            dataGridView2.Rows.Clear();
            //generare meniuri
            foreach(Produs p1 in Form1.meniu)
            {
                foreach (Produs p2 in Form1.meniu)
                {
                    foreach (Produs p3 in Form1.meniu)
                    {
                        int pret_meniu = p1.pret + p2.pret + p3.pret;
                        int kcal_meniu = p1.kcal + p2.kcal + p3.kcal;
                        if (pret_meniu<=Convert.ToInt32(textBox8.Text) && kcal_meniu <= Convert.ToInt32(textBox9.Text))
                        {
                            dataGridView2.Rows.Add(p1.denumire_produs, p2.denumire_produs, p3.denumire_produs, kcal_meniu, pret_meniu);
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==dataGridView2.ColumnCount-1)
            {
                Form1.comenzi.Add(new Comanda(Form1.index_comanda.ToString(),Autentificare_client.client_Autentificat.id_client,DateTime.Now));
                Form1.index_comanda++;
                //pregatim indexul pt urmatoarea comanda
                MessageBox.Show("Comanda trimisa"); //trimite comanda
            }
        }
    }
}
