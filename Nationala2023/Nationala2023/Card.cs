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
        public Action refresh;

        public Card(string data, Action refresh)
        {
            box = new PictureBox();
            box.BackColor = System.Drawing.Color.Red;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            //box.MouseClick -= async (s, e) => await SelectCard(s, e);
            //daca folosesti lambda nu poti sai dai remove pt ca creeaza o instanta diferita each time
            box.MouseClick += SelectCardHandler;
            this.data = data;
            this.refresh = refresh;
        }

        private async void SelectCardHandler(object sender, EventArgs e)
        {
            await SelectCard(sender, e);
        }

        public abstract Task SelectCard(Object sender, EventArgs e); //click event

        public abstract void Turn();//turning the card on one side or another

        public void RemoveListener()
        {
            box.MouseClick -= SelectCardHandler;
        }


    }
}
