using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Judeteana2016
{
    public partial class Vizualizare_comanda : Form
    {
        public Vizualizare_comanda()
        {
            InitializeComponent();
        }

        public void UpdateLabels()
        {
            textBox5.Text = Autentificare_client.client_Autentificat.kcal_zilnice.ToString();
            textBox6.Text = Optiuni.total_kcal.ToString();
            textBox7.Text = Optiuni.total_pret.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.ColumnCount - 1)
                {
                    MessageBox.Show("produs sters cu succes");
                    Optiuni.total_kcal -= Convert.ToInt32(dataGridView1[2, e.RowIndex].Value) * Convert.ToInt32(dataGridView1[3, e.RowIndex].Value);
                    Optiuni.total_pret -= Convert.ToInt32(dataGridView1[1, e.RowIndex]. Value) * Convert.ToInt32(dataGridView1[3, e.RowIndex].Value);
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    UpdateLabels();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la stergerea din cos");
                MessageBox.Show(ex.Message);
            }
        }

        private void Vizualizare_comanda_Load(object sender, EventArgs e)
        {
            var Query =
            from subcomanda in Form1.subcomenzi
            where (Convert.ToInt32(subcomanda.id_comanda) == Form1.index_comanda-1)
            select subcomanda;
            foreach (Subcomanda s in Query)
            {
                dataGridView1.Rows.Add(Form1.meniu[s.id_produs].denumire_produs, Form1.meniu[s.id_produs].pret, Form1.meniu[s.id_produs].kcal,
                    s.cantitate);
            }
            UpdateLabels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comanda trimisa");
        }
    }
}
