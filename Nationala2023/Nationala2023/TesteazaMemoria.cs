using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public partial class TesteazaMemoria : Form
    {
        public TesteazaMemoria()
        {
            InitializeComponent();
        }
        MemoryGame session;
        int n = 3;

        private void TesteazaMemoria_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            session = new MemoryGame(3);

            //Init right size acording to panel size
            foreach (Pair pair in session.pairs)
            {
                pair.pictureCard.box.Size = new Size((flowLayoutPanel1.Width / session.numberOfPairs) - 20, flowLayoutPanel1.Height / 3);
                pair.labelCard.box.Size = new Size((flowLayoutPanel1.Width / session.numberOfPairs) - 20, flowLayoutPanel1.Height / 3);
            }
            //Add controls
            foreach (Pair pair in session.pairs)
            {
                flowLayoutPanel1.Controls.Add(pair.pictureCard.box);
            }
            foreach (Pair pair in session.pairs)
            {
                flowLayoutPanel1.Controls.Add(pair.labelCard.box);
            }
            //flowLayoutPanel1.CreateControl();
        }
    }
}
