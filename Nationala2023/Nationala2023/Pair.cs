using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public class Pair
    {

        public Card pictureCard;
        public Card labelCard;
        public bool paired = false;
        Action refresh;

        public Pair(Card pictureCard, Card labelCard,Action refresh)
        {
            this.refresh = refresh;
            this.pictureCard = pictureCard;
            this.labelCard = labelCard;
        }


    }
}
