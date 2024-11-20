using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2024
{
    public partial class Cosmos_Calendar : Form
    {
        public Cosmos_Calendar()
        {
            InitializeComponent();
        }

        public Cosmos_Calendar(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        //email ul utilizatorului curent
        string email="popescum@yahoo.com";

        private void Cosmos_Calendar_Load(object sender, EventArgs e)
        {
            this.Text = email;
        }
    }
}
