using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2022
{
    public partial class AdaugaMasurare : Form
    {
        public AdaugaMasurare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = Vizualizare.dataselectata.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);

            Console.WriteLine(dt.ToString());   
            Db.masurari.Add(new Masurare(Db.masurari.Count(), Vizualizare.indexHarta,
                Vizualizare.clickedX,Vizualizare.clickedY,Convert.ToInt32(textBox1.Text),dt));
            this.Close();
        }
    }
}
