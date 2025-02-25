using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2023
{
    public partial class FormlarSarpe : Form
    {
        public FormlarSarpe()
        {
            InitializeComponent();
            this.KeyPreview = true; 
        }

        SuprafataJoc suprafataJoc;
        Dictionary<Keys, string> directie = new Dictionary<Keys, string>()
        { 
            {Keys.W,"in sus"},
            {Keys.S,"in jos"},
            {Keys.A,"in stanga"},
            {Keys.D,"in dreapta"}
        };

        public void UpdateButtons()
        {
            button1.Enabled = !button1.Enabled;
            button2.Enabled = !button2.Enabled;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateButtons();

            suprafataJoc = new SuprafataJoc(pictureBox1.Width, pictureBox1.Height);
            var head = suprafataJoc.snake.body.First();
            head.direction = "in jos";

            this.Refresh();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (suprafataJoc == null) return; //game has not started yet
            e.Graphics.DrawImage(suprafataJoc.bmp, new Point(0, 0));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            suprafataJoc.CheckFood();
            suprafataJoc.refresh();
            this.Refresh();
        }

        private void FormlarSarpe_KeyDown(object sender, KeyEventArgs e)
        {
            if (suprafataJoc == null) return; //game has not started yet

            var head = suprafataJoc.snake.body.First();
            head.direction = directie[e.KeyCode];
        }

        private void FormlarSarpe_Load(object sender, EventArgs e)
        {
        }
    }
}
