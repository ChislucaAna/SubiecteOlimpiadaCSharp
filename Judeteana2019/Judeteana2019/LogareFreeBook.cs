using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2019
{
    public partial class LogareFreeBook : Form
    {
        public LogareFreeBook()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var queryResults = from c in Db.utilizatori
                               where (c.email == textBox1.Text.Trim() && textBox2.Text.Trim()==c.parola)
                               select c;
            if (!queryResults.Any())
            {
                MessageBox.Show("Utilizatorul nu a fost gasit!");
                return;
            }

            Db.utilizatorLogat = queryResults.First();
            this.Hide();
            MeniuFreeBook book = new MeniuFreeBook();
            book.ShowDialog();
            this.Show();
        }
    }
}
