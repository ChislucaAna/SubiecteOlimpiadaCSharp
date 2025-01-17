using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public class Pair
    {

        public Card pictureCard;
        public Card labelCard;
        public bool paired = false;
        public string path;
        public string label;

        public Pair(Card pictureCard, Card labelCard)
        {
            this.pictureCard = pictureCard;
            this.labelCard = labelCard;
        }
    }
}
