using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

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
        public MemoryGame(int n)
        {
            this.n = n;
            this.numberOfPairs = F(n);
            pairs = new Pair[numberOfPairs];
            for (int i = 0; i < numberOfPairs; i++)
            {
                pairs[i]= new Pair(new Card(), new Card());
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
                        pairs[i].path = file;
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
                return(F(n - 1) + F(n-2));
        }
    }
}
