using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Judeteana2023
{
    public class SuprafataJoc
    {
        public int Width;
        public int Height;
        public Snake snake;
        public Bitmap bmp;
        public Rectangle food;

        public SuprafataJoc(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;

            bmp = new Bitmap(Width, Height);
            snake = new Snake(GenerateRandomPosition(0,0,Width,Height/2));
            food = new Rectangle(GenerateRandomPosition(0,Height/2,Width,Height),
                new Size(Snake.cellSize,Snake.cellSize));
            refresh();
        }

        public Point GenerateRandomPosition(int lowerx, int lowery, int upperx, int uppery)
        {
            Random rnd = new Random();
            int x = rnd.Next(lowerx, upperx);
            int y = rnd.Next(lowery,uppery);
            return new Point(x,y);
        }

        public void CheckFood()
        {
            if (snake.body.First().cell.IntersectsWith(food))
            {
                food = new Rectangle(GenerateRandomPosition(0, 0, Width, Height),
                new Size(Snake.cellSize, Snake.cellSize)); 
                snake.Grow();
            }
        }

        public void refresh()
        {
            Graphics g = Graphics.FromImage(bmp);

            Rectangle background = new Rectangle(0,0,Width,Height);
            g.FillRectangle(new SolidBrush(Color.Black), background);

            foreach(var r in snake.body)
            {
                Console.WriteLine(r.cell.X.ToString());  
                if(r.Equals(snake.body.First())) //head
                    g.FillEllipse(new SolidBrush(Color.White), r.cell);
                else
                    g.FillEllipse(new SolidBrush(Color.Green), r.cell);
            }

            g.FillEllipse(new SolidBrush(Color.Red), food);
        }
    }
}
