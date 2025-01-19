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
            Action refreshUi = this.Refresh;
            session = new MemoryGame(3,refreshUi);

            //Init right size acording to panel size
            foreach (Pair pair in session.pairs)
            {
                pair.pictureCard.box.Size = new Size((flowLayoutPanel1.Width / session.numberOfPairs) - 20, (flowLayoutPanel1.Height / 2)-20);
                pair.labelCard.box.Size = new Size((flowLayoutPanel1.Width / session.numberOfPairs) - 20, (flowLayoutPanel1.Height / 2)-20);
            }
            //Add controls
            foreach (Pair pair in session.pairs)
            {
                pair.pictureCard.box.MouseClick += session.CheckMatch;
                flowLayoutPanel1.Controls.Add(pair.pictureCard.box);
            }
            foreach (Pair pair in session.pairs)
            {
                pair.labelCard.box.MouseClick += session.CheckMatch;
                flowLayoutPanel1.Controls.Add(pair.labelCard.box);
            }
            //flowLayoutPanel1.CreateControl();
        }
    }
}
