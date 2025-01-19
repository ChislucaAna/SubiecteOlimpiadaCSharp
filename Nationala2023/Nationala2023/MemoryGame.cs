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

                        //Add image label into LabelCard , and full path into ImageCard
                        string[] bucati = file.Split('\\');

                        pairs[i] = new Pair(new ImageCard(file), new LabelCard(bucati.Last()),refresh);
                        
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
        public void CheckMatch(Object sender, EventArgs e)
        {
            Console.WriteLine("Executing match event");
            int labelCardindex = -1;
            int pictureCardindex = -1;
            for(int i=0; i<numberOfPairs; i++)
            {
                if (pairs[i].labelCard.selected == true)
                {
                    labelCardindex = i;
                }
            }
            for (int i = 0; i < numberOfPairs; i++)
            {
                if (pairs[i].pictureCard.selected == true)
                {
                    pictureCardindex = i;
                }
            }
            if (labelCardindex==pictureCardindex && pictureCardindex!=-1) //match was made
            {
                Console.WriteLine("Got a Match");

                //They turn and remain shown and deactivated
                pairs[pictureCardindex].pictureCard.shown = true;
                pairs[labelCardindex].labelCard.shown = true;
                pairs[pictureCardindex].pictureCard.Turn(sender, null);
                pairs[labelCardindex].addIndexToLabelCard();

                pairs[pictureCardindex].pictureCard.box.Enabled = false;
                pairs[labelCardindex].labelCard.box.Enabled = false;

                refresh();
            }
            else
            {
                Console.WriteLine("Not a match");
            }
        }

    }
}
