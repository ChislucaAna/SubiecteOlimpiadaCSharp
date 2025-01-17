using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public class Card
    {
        public PictureBox box;
        public bool shown = false;

        public Card()
        {
            box = new PictureBox();
            box.BackColor = System.Drawing.Color.Red;
        }   
    }
}
