using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2013
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Init();
            Db.Save();
        }

        public static int TipUtilizatorLogat = 0;
        public static int IDUtilizatorLogat;
        private void Autentificare_Click(object sender, EventArgs e)
        {
            var query = (from u in Db.utilizatori
                        where u.Nickname == textBox1.Text && u.Parola == textBox2.Text  
                        select u).FirstOrDefault();
            if(query==null)
            {
                MessageBox.Show("Datele de autentificare nu sunt corecte");
                return;
            }
            TipUtilizatorLogat = query.TipUtilizator;
            IDUtilizatorLogat = query.ID;
            if(TipUtilizatorLogat == 1)
            {
                Jucator jucator = new Jucator();
                this.Hide();
                jucator.ShowDialog();
                this.Show();
            }
            else
            {
                Administrator administrator = new Administrator();
                this.Hide();
                administrator.ShowDialog();
                this.Show();
            }
        }

        private void Autentificare_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
