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

        int hoursLeft;
        int minutesLeft;
        int secondsLeft;
        int stareRebus = 1;
        private void FrmRebus_Load(object sender, EventArgs e)
        {
            Db.Init();
            //fill rebusuri combobox
            foreach (Rebus r in Db.rebusuri)
            {
                comboBox1.Items.Add(r.DenumireRebus);
                comboBox2.Items.Add(r.DenumireRebus);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayRebus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayRebusEditingMode();
        }

        public void displayRebus()
        {
            tableLayoutPanel1.Controls.Clear();
            listView1.Items.Clear();

            Rebus rebus_current = Db.rebusuri[comboBox1.SelectedIndex];


            //fill with placeholder controls
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    Label t = new Label();
                    t.Text = "";
                    t.Width = 20;
                    t.Height = 20;
                    t.BackColor = Color.Black;

                    if (tableLayoutPanel1.GetControlFromPosition(j, i) == null)
                    {
                        tableLayoutPanel1.Controls.Add(t, j, i);
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
                if (directie == "orizontal")
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
                Label t = tableLayoutPanel1.GetControlFromPosition(coloana - 1, startingRow - 1) as Label;
                t.BackColor = Color.White;
                t.Text = c.ToString().ToUpper();
                coloana++;
            }
        }

        public void WriteVertically(string s, int startingRow, int startingColumn)
        {
            int rand = startingRow;
            foreach (char c in s)
            {
                Label t = tableLayoutPanel1.GetControlFromPosition(startingColumn - 1, rand - 1) as Label;
                t.BackColor = Color.White;
                t.Text = c.ToString().ToUpper();
                rand++;
            }
        }

        public void WriteHorizontally2(string s, int startingRow, int startingColumn)
        {
            int coloana = startingColumn;
            foreach (char c in s)
            {
                TextBox t = tableLayoutPanel2.GetControlFromPosition(coloana - 1, startingRow - 1) as TextBox;
                t.BackColor = Color.White;
                t.ReadOnly = false;
                t.Enabled = true;
                coloana++;
            }
        }

        public void WriteVertically2(string s, int startingRow, int startingColumn)
        {
            int rand = startingRow;
            foreach (char c in s)
            {
                TextBox t = tableLayoutPanel2.GetControlFromPosition(startingColumn - 1, rand - 1) as TextBox;
                t.BackColor = Color.White;
                t.ReadOnly = false;
                t.Enabled = true;
                rand++;
            }
        }

        public void FillTimeLabels(Rebus r)
        {
            int secunde = r.TimpEstimat;
            int h = secunde / 3600;
            secunde -= (3600 * h);
            int m = secunde / 60;
            secunde -= (60 * m);
            textBox1.Text = h.ToString();
            textBox2.Text = m.ToString();
            textBox3.Text = secunde.ToString();

            textBox4.Text = h.ToString();
            textBox5.Text = m.ToString();
            textBox6.Text = secunde.ToString();

            hoursLeft = h; minutesLeft = m; secondsLeft = secunde;
        }

        public void UpdateTimeRemaningLabel()
        {
            secondsLeft--;
            if (secondsLeft == 0)
            {
                minutesLeft--;
                secondsLeft = 59;
            }
            if (minutesLeft == 0)
            {
                hoursLeft--;
                minutesLeft = 59;
            }
            if (hoursLeft == 0 && minutesLeft == 0 && secondsLeft == 0)
            {
                timer1.Stop();
                stareRebus = 2;
            }
            textBox4.Text = hoursLeft.ToString();
            textBox5.Text = minutesLeft.ToString();
            textBox6.Text = secondsLeft.ToString();

        }

        public void displayRebusEditingMode()
        {
            tableLayoutPanel2.Controls.Clear();
            listView2.Items.Clear();

            Rebus rebus_current = Db.rebusuri[comboBox2.SelectedIndex];


            //fill time remaning labels
            FillTimeLabels(rebus_current);

            //fill with placeholder controls
            for (int i = 0; i < tableLayoutPanel2.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel2.ColumnCount; j++)
                {
                    TextBox t = new TextBox();
                    t.Text = "";
                    t.Width = 20;
                    t.Height = 20;
                    t.BackColor = Color.Black;
                    t.ReadOnly = true;
                    t.Enabled = false;
                    t.TextChanged += (sender, e) =>
                    {
                        t.Text = t.Text.ToUpper();
                    };

                    if (tableLayoutPanel2.GetControlFromPosition(j, i) == null)
                    {
                        tableLayoutPanel2.Controls.Add(t, j, i);
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

                //add to grid
                if (directie == "orizontal")
                {
                    WriteHorizontally2(sol, linie, col);
                    //add to list
                    ListViewItem item2 = new ListViewItem(linie.ToString());
                    item2.SubItems.Add(descriere);
                    listView2.Items.Add(item2);
                }
                else //"vertical"
                {
                    WriteVertically2(sol, linie, col);
                    //add to list
                    ListViewItem item2 = new ListViewItem(linie.ToString());
                    item2.SubItems.Add(descriere);
                    listView3.Items.Add(item2);
                }
            }
            reader.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTimeRemaningLabel();
        }

        public int GetMistakes()
        {
            int n = 0;
            for (int i = 0; i < tableLayoutPanel2.RowCount; i++)
                for (int j = 0; j < tableLayoutPanel2.ColumnCount; j++)
                {
                    Label l = tableLayoutPanel1.GetControlFromPosition(j, i) as Label;
                    TextBox t = tableLayoutPanel2.GetControlFromPosition(j, i) as TextBox;
                    if (l.Text != t.Text)
                        n++;
                }
            return n;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nr_litere_gresite = GetMistakes();
            if (nr_litere_gresite > 0)
            {
                stareRebus = 2;

                //aici ai ramas tre sa iei id -ul userului logat si sa daugi in db
                //Db.statistici.Add(new Statistica())
            }
        }
    }
}

