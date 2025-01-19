using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Nationala2023
{
    public class LabelCard: Card
    {
        public LabelCard(string data) : base(data)
        {
            box.Paint += Turn;
        }

        public override async Task SelectCard(Object sender, EventArgs e)
        {
            selected = true;

            Console.WriteLine("Showing label card for 2 sec");
            shown = true;
            box.Refresh();

            await Task.Delay(1000); //better than thread.sleep

            Console.WriteLine("Turning the card back around");
            shown = false;
            box.Refresh();
        }

        public override void Turn(object sender, PaintEventArgs e)
        {
            if (shown)
            {
                e.Graphics.DrawString(data, new Font("Arial", 13), new SolidBrush(Color.Black), new Point(0, 0));
                Console.WriteLine(data);

            }
            else
            {
                e.Graphics.Clear(Color.Red);
            }
        }
    }
}
