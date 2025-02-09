using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2019
{
    public partial class MeniuFreeBook : Form
    {
        public MeniuFreeBook()
        {
            InitializeComponent();
        }

        public static int nr_folosite = 0;
        public static int carte_selectata;
        int[] valori= new int[13];

        private void MeniuFreeBook_Load(object sender, EventArgs e)
        {
            label1.Text += Db.utilizatorLogat.email;
            nr_folosite = 0;

            foreach (Carte c in Db.carti)
            {
                var queryResults = from imprumut in Db.imprumuturi
                                   where imprumut.id_carte == c.id_carte && imprumut.email==Db.utilizatorLogat.email
                                   select imprumut.id_carte;
                if (!queryResults.Any()) //nu exista niciun imprumut realizat de utilizatorul logat pentru cartea c:
                {
                    dataGridView1.Rows.Add(c.titlu, c.autor, c.gen);
                }
            }
            repopulate();
            fillpie();
        }

        public void fillpie()
        {

            Dictionary<int,int> dict = new Dictionary<int,int>();

            foreach (Imprumut item in Db.imprumuturi)
            {
                if(!dict.ContainsKey(item.id_carte))
                    dict.Add(item.id_carte, 1);
                else
                    dict[item.id_carte]++;
            }
            dict.OrderBy(v => v.Value);

            int index = 4;
            foreach(int id in dict.Keys)
            {
                if (index >= 1)
                {
                    var carti = from c in Db.carti
                                where c.id_carte == id
                                select c;
                    chart2.Series[0].Points.AddXY(carti.First().titlu, dict[id]);
                    index--;
                }
            }
        }

        public void fillChart()
        {
            chart1.Series[0].Points.Clear();
            var queryResults = from imprumut in Db.imprumuturi
                               where imprumut.data_imprumut.Year == Convert.ToInt32(comboBox1.SelectedItem.ToString().Trim())
                               select imprumut;
            foreach (Imprumut imprumut in queryResults)
            {
                Console.WriteLine("imprumut gasit");
                valori[imprumut.data_imprumut.Month]++;
            }


            for (int index = 1; index <= 12; index++)
            {   
                chart1.Series[0].Points.AddXY(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(index), valori[index]);
            }
        }

        public void repopulate() //refresh second datagridview
        {
            dataGridView2.Rows.Clear();
            var listaImprumuturi = from i in Db.imprumuturi
                                   where i.email == Db.utilizatorLogat.email
                                   select i;
            int index = 0;
            foreach (var imprumut in listaImprumuturi)
            {
                var carti = from c in Db.carti
                            where c.id_carte == imprumut.id_carte
                            select c;
                dataGridView2.Rows.Add(index++, carti.First().titlu, carti.First().autor, imprumut.data_imprumut, imprumut.data_imprumut.AddDays(30));
            }
            nr_folosite = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (DateTime.Now.CompareTo(Convert.ToDateTime(row.Cells["Data_disponibilitate"].Value)) < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    nr_folosite++;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
            progressBar1.Value = nr_folosite;
            label2.Text = nr_folosite + "/3";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                if (nr_folosite < 3)
                {
                    var queryResults = from c in Db.carti
                                       where c.titlu == dataGridView1[0, e.RowIndex].Value.ToString()
                                       select c;
                    Db.imprumuturi.Add(new Imprumut(Db.imprumuturi.Count, queryResults.First().id_carte, Db.utilizatorLogat.email, DateTime.Now));
                    MessageBox.Show("Imprumut realizat cu succes");
                    Db.Refresh();
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    repopulate();
                }
                else
                {
                    MessageBox.Show("Ai imprumutat prea multe carti");
                }
            }
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor==Color.Red)
            {
                MessageBox.Show("Perioada imprumutului expirata!");
            }
            else
            {
                //afisare carte
                AfiseazaCarte afiseazaCarte = new AfiseazaCarte();
                var queryResults = from carte in Db.carti
                                   where carte.titlu == (dataGridView2[1, e.RowIndex].Value.ToString())
                                   select carte.id_carte;
                carte_selectata = queryResults.First();
                this.Hide();
                afiseazaCarte.ShowDialog();
                this.Show();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
