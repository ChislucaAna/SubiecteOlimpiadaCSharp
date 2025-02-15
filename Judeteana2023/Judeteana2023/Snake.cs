using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Judeteana2023
{
    public class Snake
    {
        public List<Rectangle> body;
        const int cellSize= 20;

        public Snake(int maxx,int maxy)
        {
            body = new List<Rectangle>();

            //generate head
            Random rnd = new Random();
            int x = rnd.Next(0, maxx);
            Thread.Sleep(100);
            int y = rnd.Next(0, maxy);
            body.Add(new Rectangle(x, y, cellSize, cellSize));
        }
    }
}
