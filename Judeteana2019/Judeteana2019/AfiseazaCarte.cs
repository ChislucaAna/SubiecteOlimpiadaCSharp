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

namespace Judeteana2019
{
    public partial class AfiseazaCarte : Form
    {
        public AfiseazaCarte()
        {
            InitializeComponent();
        }

        private void AfiseazaCarte_Load(object sender, EventArgs e)
        {
            string path = (MeniuFreeBook.carte_selectata + 1).ToString(); //eu am indexat de la 0
            path += ".pdf";
            string fullpath=Path.GetFullPath(path);
            Uri uri = new Uri(fullpath);
            Console.WriteLine(uri.ToString());    
            webBrowser1.Navigate(uri);
        }
    }
}
