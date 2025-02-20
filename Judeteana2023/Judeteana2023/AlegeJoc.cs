using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2023
{
    public partial class AlegeJoc : Form
    {
        public AlegeJoc()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Bine ai venit ";
            label1.Text += Db.utilizatorLogat.NumeUtilizator.ToString();
            label1.Text += "!";

            //0 ghiceste 1 sarpe
            var scoruriGhiceste = from rez in Db.rezultate
                                  where rez.TipJoc == 0
                                  select rez;
            Dictionary<int,int> dic = scoruriGhiceste.ToDictionary(n => n.idRezultat, n => n.PunctajJoc);
            var ordered  = dic.OrderBy(n => n.Value*(-1));
            int index = 1;
            foreach (var item in ordered)
            {
                if (index <= 3)
                {
                    Console.WriteLine(item.Key);
                    dataGridView1.Rows.Add(Db.GetNumeFromEmail(Db.rezultate[item.Key-1].EmailUtilizator),
                       Db.GetNumeFromEmail(Db.rezultate[item.Key-1].EmailUtilizator),
                    item.Value.ToString());
                    index++;
                }
            }

            var scoruriSarpe = from rez in Db.rezultate
                                  where rez.TipJoc == 1
                                  select rez;
            Dictionary<int, int> dic2 = scoruriSarpe.ToDictionary(n => n.idRezultat, n => n.PunctajJoc);
            var ordered2 = dic2.OrderBy(n => n.Value * (-1));
            int index2 = 1;
            foreach (var item in ordered2)
            {
                if (index2 <= 3)
                {
                    dataGridView2.Rows.Add(Db.GetNumeFromEmail(Db.rezultate[item.Key-1].EmailUtilizator),
                    Db.GetNumeFromEmail(Db.rezultate[item.Key-1].EmailUtilizator),
                item.Value.ToString());
                    index2++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ghiceste ghiceste = new Ghiceste();
            ghiceste.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormlarSarpe sarpe = new FormlarSarpe();
            sarpe.ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
