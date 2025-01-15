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

namespace Nationala2023
{
    public partial class AlegeJoc : Form
    {
        public AlegeJoc()
        {
            InitializeComponent();
        }

        private void AlegeJoc_Load(object sender, EventArgs e)
        {
            label1.Text = "Salut ";
            label1.Text += Autentificare.utilizatorLogat.nume.ToString();
            label1.Text += " ";
            label1.Text+= Autentificare.utilizatorLogat.email.ToString();
            label1.Text += "!";
            
            //selectezi toate datele distincte la care s-au facut inregistrari
            //la cel putin un tip de joc
            IEnumerable<DateTime> date_inregistrari =
            from rezultat in Autentificare.rezultate
            where (rezultat.EmailUtilizator == Autentificare.utilizatorLogat.email)
            select rezultat.data.Date;
            date_inregistrari = date_inregistrari.Distinct();

            foreach(DateTime data in date_inregistrari)
            {
                //pentru fiecare data iei toate rezultatele si le sortezi crescator
                //pentru fiecare joc separat
                Console.WriteLine(String.Format("Se cauta informatii despre data {0}", data));
                //Jocul1 : Testeaza memoria
                IEnumerable<Rezultat> inregistrari =
                from rezultat in Autentificare.rezultate
                where (DateTime.Compare(rezultat.data.Date,data)==0 && rezultat.TipJoc==0 
                && (rezultat.EmailUtilizator == Autentificare.utilizatorLogat.email))
                select rezultat;

                if (inregistrari.Any())
                {
                    var sortedNumbers = inregistrari.OrderByDescending(n => n.Punctajjoc);
                    Rezultat maxim = sortedNumbers.First();

                    chart1.Series["Testeaza Memoria"].Points.AddXY(maxim.data.Date, maxim.Punctajjoc);
                    Console.WriteLine( maxim.Punctajjoc.ToString());
                }

                //Jocul2: Popice cu litere
                inregistrari =
                from rezultat in Autentificare.rezultate
                where (DateTime.Compare(rezultat.data.Date, data) == 0 && rezultat.TipJoc == 1
                && (rezultat.EmailUtilizator == Autentificare.utilizatorLogat.email))
                select rezultat;

                if (inregistrari.Any())
                {
                    var sortedNumbers = inregistrari.OrderByDescending(n => n.Punctajjoc);

                    var maxim = sortedNumbers.First();

                    chart1.Series["Popice cu litere"].Points.AddXY(maxim.data.Date, maxim.Punctajjoc);
                    Console.WriteLine(maxim.Punctajjoc.ToString());
                }
            }
        }
    }
}
