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
using System.Runtime.InteropServices;
using System.Linq.Expressions;

namespace Nationala2023
{
    public class MemoryGame
    {
        public int n;
        public static int numberOfPairs;
        public TimeSpan remainingTime = TimeSpan.FromSeconds(100);
        public bool game_was_won = false;
        public Pair[] pairs;
        public bool[] usedSourceImage = new bool[14];
        public static int PairsFormed=0;
        Action refresh;

        public static Card selectedImage;
        public static Card selectedLabel;
        public MemoryGame(int n,Action refresh)
        {
            this.n = n;
            numberOfPairs = F(n);
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
                        pairs[i] = new Pair(new ImageCard(file,refresh), new LabelCard(file,refresh),refresh);      
                    }
                    index--;
                }
            }
        }

        public static int F(int n)
        {
            if (n <= 2)
                return 1;
            else
                return (F(n - 1) + F(n - 2));
        }

        public static void CheckMatch()
        {
            Console.WriteLine("Executing match event");
            if (selectedImage == null || selectedLabel == null)
                return;
            if (selectedImage.data == selectedLabel.data) //match was made
            {
                Console.WriteLine("Got a Match");

                //They turn and remain shown and deactivated
                selectedImage.shown = true;
                selectedImage.Turn();
                (selectedLabel as LabelCard).PaintLabel();

                //if you disable the control, it forces the page to refresh causing a bug
                selectedLabel.RemoveListener();
                selectedImage.RemoveListener();

                PairsFormed++;
            }
            else
            {
                Console.WriteLine("Not a match");
                selectedImage.selected = false;
                selectedLabel.selected = false;
            }
            selectedImage = null;
            selectedLabel = null;
        }

        public static bool CheckWin()
        {
            if (PairsFormed == numberOfPairs)
            {
                MessageBox.Show("You won. Congrats");
                TesteazaMemoria.n++;
                return true;
            }
            return false;
        }

    }
}
