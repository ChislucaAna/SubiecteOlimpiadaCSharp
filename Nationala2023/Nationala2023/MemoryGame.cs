using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Reflection.Emit;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Nationala2023
{
    public class MemoryGame
    {
        public int n;
        public int numberOfPairs;
        public TimeSpan remainingTime = TimeSpan.FromSeconds(100);
        public bool game_was_won = false;
        public Pair[] pairs;
        public bool[] usedSourceImage = new bool[14];
        Action refresh;

        public static Card selectedImage;
        public static Card selectedLabel;
        public MemoryGame(int n,Action refresh)
        {
            this.n = n;
            this.numberOfPairs = F(n);
            this.refresh= refresh;
            pairs = new Pair[numberOfPairs];
            for (int i = 0; i < numberOfPairs; i++)
            {

                int index;
                do
                {
                    Random rnd = new Random();
                    index = rnd.Next(0, 14);
                    Thread.Sleep(50);
                } while (usedSourceImage[index] == true);
                var files = Directory.GetFiles(AppContext.BaseDirectory + "\\Imagini");
                foreach (var file in files)
                {
                    if (index == 0)
                    {
                        pairs[i] = new Pair(new ImageCard(file), new LabelCard(file),refresh);      
                    }
                    index--;
                }
            }
        }

        public int F(int n)
        {
            if (n <= 2)
                return 1;
            else
                return (F(n - 1) + F(n - 2));
        }

        public static void CheckMatch()
        {
            Console.WriteLine("Executing match event");
            if (selectedImage != null && selectedLabel != null)
            {
                if (selectedImage.data == selectedLabel.data) //match was made
                {
                    Console.WriteLine("Got a Match");

                    //They turn and remain shown and deactivated
                    selectedImage.shown = true;
                    selectedLabel.shown = true;
                    selectedImage.Turn();
                    selectedLabel.Turn();

                    TesteazaMemoria.ActiveForm.Refresh();

                    selectedImage.box.Enabled = false;
                    selectedLabel.box.Enabled = false;

                    selectedImage = null;
                    selectedLabel = null;
                }
                else
                {
                    Console.WriteLine("Not a match");
                    selectedImage.selected = false;
                    selectedLabel.selected = false;
                    selectedImage = null;
                    selectedLabel = null;
                }
            }
        }

    }
}
