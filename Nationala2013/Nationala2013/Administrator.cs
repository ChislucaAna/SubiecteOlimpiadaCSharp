using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Nationala2013
{
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();
            ..Db.Init();
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Db.utilizatori;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) 
                dataGridView1.Rows.Remove(row);
        }

        private void button3_Click(object sender, EventArgs e)
        {
           FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                Console.WriteLine(path + @"\top3.txt");
                StreamWriter streamWriter = new StreamWriter(path + @"\top3.txt");
                for(int i=0; i<3; i++)
                {

                    //gaseste in tabela scoruri utilizatorul cu id ul dat si ia datele alea

                    var scoruri_sorted = Db.scoruri.OrderBy(n => n.Timp);
                    var query = (from r in scoruri_sorted
                                 where r.ID == Db.utilizatori[i].ID
                                select r).FirstOrDefault();
                    if (query != null)
                    {
                        streamWriter.WriteLine(Db.utilizatori[i].Nume + " " + Db.utilizatori[i].Prenume
                        + " " + query.NrMutari + " " + query.NrPiese);
                    }
                }
                streamWriter.Close();
            }
        }
    }
}
