using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2016
{
    public partial class FrmRebus : Form
    {
        public FrmRebus()
        {
            InitializeComponent();
            
        }

        private void FrmRebus_Load(object sender, EventArgs e)
        {
            Db.Init();
            //fill rebusuri combobox
            foreach(Rebus r in Db.rebusuri)
                comboBox1.Items.Add(r.DenumireRebus);
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayRebus();
        }

        public void displayRebus()
        {
            tableLayoutPanel1.Controls.Clear();
            listView1.Items.Clear();

            Rebus rebus_current = Db.rebusuri[comboBox1.SelectedIndex];


            //fill with placeholder controls
            for(int i=0; i<tableLayoutPanel1.RowCount; i++)
                for(int j=0; j<tableLayoutPanel1.ColumnCount; j++)
                {
                    Label t = new Label();
                    t.Text = "";
                    t.Width = 20;
                    t.Height = 20;
                    t.BackColor = Color.Black;

                    if (tableLayoutPanel1.GetControlFromPosition(j, i) == null)
                    {
                        tableLayoutPanel1.Controls.Add(t,j, i);
                    }
                }

            string nume_fisier = rebus_current.DenumireRebus.ToString();
            StreamReader reader = new StreamReader(nume_fisier + ".txt");
            Console.WriteLine(nume_fisier);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('|');
                int col = Convert.ToInt32(fields[0]);
                int linie = Convert.ToInt32(fields[1]);
                string directie = fields[2];
                string sol = fields[3]; 
                string descriere = fields[4];

                //add to list
                ListViewItem item2 = new ListViewItem(linie.ToString());
                item2.SubItems.Add(directie);
                item2.SubItems.Add(descriere);
                listView1.Items.Add(item2);
                //add to grid
                if(directie=="orizontal")
                {
                    WriteHorizontally(sol, linie, col);
                }
                else //"vertical"
                {
                    WriteVertically(sol, linie, col);
                }
            }
            reader.Close();
        }
        public void WriteHorizontally(string s, int startingRow, int startingColumn)
        {
            int coloana = startingColumn;
            foreach (char c in s)
            {
                Label t = tableLayoutPanel1.GetControlFromPosition(coloana-1, startingRow-1) as Label;
                t.BackColor = Color.White;
                t.Text = c.ToString();
                coloana++;
            }
        }

        public void WriteVertically(string s, int startingRow, int startingColumn)
        {
            int rand = startingRow;
            foreach (char c in s)
            {
                Label t = tableLayoutPanel1.GetControlFromPosition(startingColumn-1, rand-1) as Label;
                t.BackColor = Color.White;
                t.Text = c.ToString();
                rand++;
            }
        }
    }
}
