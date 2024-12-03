using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2012
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        public class intrebare
        {
            public string enunt;

            //4 posibile, 4 maxim corecte
            public string[] raspunsuri_posibile= new string[5];
            public string[] raspunsuri_corecte= new string[5];
            public int punctaj;
            public int tip_control; //0 – RadioButton; 1 – CheckBox;
            

            public intrebare(string enunt, string[] raspunsuri_posibile, string[] raspunsuri_corecte, int punctaj, int tip_control)
            {
                this.enunt = enunt;
                this.raspunsuri_posibile = raspunsuri_posibile;
                this.raspunsuri_corecte = raspunsuri_corecte;
                this.punctaj = punctaj;
                this.tip_control = tip_control;
            }   
        }

        intrebare[] intrebari = new intrebare[100];

        int intrebare_curenta=1;
        int nr_intrebari;
        int punctaj = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            int index = 1;
            StreamReader r = new StreamReader("intrebari.txt");
            while(r.Peek() != -1) {

                string enunt = r.ReadLine();
                //MessageBox.Show(enunt);

                string[] raspunsuri_posibile = new string[5];
                for (int i = 1; i <= 4; i++)
                {
                    raspunsuri_posibile[i] = r.ReadLine();
                    //MessageBox.Show(raspunsuri_posibile[i]);
                }

                //Raspunsurile corecte sunt scrise pe aceeasi linie
                //si separate printr-un spatiu
                string[] raspunsuri_corecte = new string[5];
                string line = r.ReadLine();
                string[] bucati = new string[5];
                //MessageBox.Show(line);
                if (line.Contains(" "))
                    bucati = line.Split(' ');
                else
                    bucati[0] = line;
                int nr_corecte = bucati.Length;
                for (int i = 1; i < nr_corecte; i++)
                    raspunsuri_corecte[i] = bucati[i - 1];

                int punctaj;
                punctaj = Convert.ToInt32(r.ReadLine().Trim());

                int tip_control;
                tip_control = Convert.ToInt32(r.ReadLine().Trim());

                intrebari[index++] = new intrebare(enunt, raspunsuri_posibile, raspunsuri_corecte
                    , punctaj, tip_control);
            }
            nr_intrebari = index - 1;
            fill_form(intrebare_curenta);
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if(intrebare_curenta<nr_intrebari)
            {
                intrebare_curenta++;
                fill_form(intrebare_curenta);
            }
            else
            {
                MessageBox.Show("Esti la capatul testului");
            }
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            if (intrebare_curenta > 1)
            {
                intrebare_curenta--;
                fill_form(intrebare_curenta);
            }
            else
            {
                MessageBox.Show("Esti la prima intrebare");
            }
        }

        public void show_controls(int nr_intrebare)
        {
            if (intrebari[nr_intrebare].tip_control == 0)
            {
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                radioButton1.Text = intrebari[nr_intrebare].raspunsuri_posibile[1];
                radioButton2.Text = intrebari[nr_intrebare].raspunsuri_posibile[2];
                radioButton3.Text = intrebari[nr_intrebare].raspunsuri_posibile[3];
                radioButton4.Text = intrebari[nr_intrebare].raspunsuri_posibile[4];
            }
            if (intrebari[nr_intrebare].tip_control == 1)
            {
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                checkBox1.Text = intrebari[nr_intrebare].raspunsuri_posibile[1];
                checkBox2.Text = intrebari[nr_intrebare].raspunsuri_posibile[2];
                checkBox3.Text = intrebari[nr_intrebare].raspunsuri_posibile[3];
                checkBox4.Text = intrebari[nr_intrebare].raspunsuri_posibile[4];

            }
        }

        public void fill_form(int nr_intrebare)
        {
            textBox1.Text = intrebari[nr_intrebare].enunt;
            show_controls(nr_intrebare);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int corect = 1;
            //la raspunsuri ai nr raspunsului care face legautra cu controlul prin tag
            foreach (string tag in intrebari[intrebare_curenta].raspunsuri_corecte)
            {
                if(intrebari[intrebare_curenta].tip_control == 1) //cauti intre Checkboxuri
                {
                    foreach(CheckBox c in this.Controls.OfType<CheckBox>())
                    {
                        if(c.Tag.ToString()==tag)
                        {
                            if (!c.Checked)
                                corect = 0;
                        }
                        else
                        {
                            if (c.Checked)
                            {
                                if (!intrebari[intrebare_curenta].raspunsuri_corecte.Contains(c.Tag.ToString()))
                                {
                                    corect = 0;
                                }
                            }
                        }
                    }
                }
                else //cauti intre radiobutton
                {
                    foreach (RadioButton c in this.Controls.OfType<RadioButton>())
                    {
                        if (c.Tag.ToString() == tag)
                        {
                            if (!c.Checked)
                                corect = 0;
                        }
                        else
                        {
                            if (c.Checked)
                            {
                                if(!intrebari[intrebare_curenta].raspunsuri_corecte.Contains(c.Tag.ToString()))
                                {
                                    corect = 0;
                                }
                            }
                        }
                    }
                }
            }
            if (corect == 1)
            {
                MessageBox.Show("Ai raspuns corect");
                punctaj += intrebari[intrebare_curenta].punctaj;
            }
            else
                MessageBox.Show("Ai raspuns gresit");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("Ai obtinut {0} puncte",punctaj.ToString()));
            this.Close();
        }
    }
}
