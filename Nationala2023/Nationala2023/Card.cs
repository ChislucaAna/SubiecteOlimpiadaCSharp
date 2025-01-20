using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public abstract class Card
    {
        public PictureBox box;
        public bool shown = false;
        public bool selected = false;
        public string data; //path for IamgeCard or label for LabelCard

        public Card(string data)
        {
            box = new PictureBox();
            box.BackColor = System.Drawing.Color.Red;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.MouseClick += async (s, e) => await SelectCard(s,e);
            this.data = data;
        }

        public abstract Task SelectCard(Object sender, EventArgs e); //click event

        public abstract void Turn();//turning the card on one side or another


    }
}
