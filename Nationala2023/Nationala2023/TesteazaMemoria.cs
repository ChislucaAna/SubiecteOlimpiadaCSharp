﻿using System;
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
        public static int n = 3;

        private void TesteazaMemoria_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewGame();

        }

        public void CreateNewGame()
        {
            flowLayoutPanel1.Controls.Clear();
            Action refreshUi = this.CreateNewGame;
            session = new MemoryGame(n, refreshUi);

            //Init right size acording to panel size
            foreach (Pair pair in session.pairs)
            {
                pair.pictureCard.box.Size = new Size((flowLayoutPanel1.Width / MemoryGame.F(n)) - 20, (flowLayoutPanel1.Height / 2) - 20);
                pair.labelCard.box.Size = new Size((flowLayoutPanel1.Width / MemoryGame.F(n)) - 20, (flowLayoutPanel1.Height / 2) - 20);
                flowLayoutPanel1.Controls.Add(pair.pictureCard.box);
                flowLayoutPanel1.Controls.Add(pair.labelCard.box);
            }
        }
    }
}
