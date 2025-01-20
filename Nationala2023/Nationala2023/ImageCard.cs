using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Nationala2023
{
    public class ImageCard:Card
    {
        public ImageCard(string data) : base(data)
        {
        }

        public async override Task SelectCard(Object sender, EventArgs e)
        {
            selected = true;

            Console.WriteLine("Showing image card for 2 sec");

            shown = true;
            Turn();

            await Task.Delay(1000);

            shown = false;
            Turn();

            if (MemoryGame.selectedImage == null)
                MemoryGame.selectedImage = this;

            MemoryGame.CheckMatch();
        }

        public override void Turn()
        {
            if (shown)
            {
                box.Image = Image.FromFile(data);
            }
            else
            {
                box.Image = null;
            }
        }
    }
}
