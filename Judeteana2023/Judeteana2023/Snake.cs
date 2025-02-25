using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Judeteana2023
{
    public class Snake
    {
        public const int cellSize= 20;

        public class SnakeBody
        {
            public Rectangle cell;
            public string direction;

            public SnakeBody(Rectangle cell, string direction)
            {
                this.cell = cell;
                this.direction = direction;
            }
        }

        public List<SnakeBody> body;

        Dictionary<string,Action<SnakeBody>> schimba_pozitie = new Dictionary<string, Action<SnakeBody>>()
        {
            {"in sus",in_sus},
            {"in jos",in_jos},
            {"in stanga",in_stanga},
            {"in dreapta",in_dreapta}
        };

        public static Action<SnakeBody> in_sus = (r) => r.cell.Y-=10;
        public static Action<SnakeBody> in_jos = (r) => r.cell.Y+=10;
        public static Action<SnakeBody> in_stanga = (r) => r.cell.X -= 10;
        public static Action<SnakeBody> in_dreapta = (r) => r.cell.X += 10;
        public Snake(Point head)
        {
            body = new List<SnakeBody>();
            body.Add(new SnakeBody(new Rectangle(head, new Size(cellSize, cellSize)),"in jos"));
        }
        public void UpdatePosition()
        {
            for (int i = body.Count-1; i >=1; i--)
            {
                body[i].cell.Location = body[i - 1].cell.Location;
            }
            schimba_pozitie[body[0].direction](body[0]);
        }

        public void Grow()
        {
            Console.WriteLine("Growing");
            var oldlast = body.Last();
            body.Add(new SnakeBody(oldlast.cell,oldlast.direction)); //facem o copie a ultimei celule
            var newlast = body.Last();
            Console.WriteLine(body.Count);
            switch (oldlast.direction)
            {
                case "in sus":
                    newlast.cell.Y += 20;
                    break;
                case "in jos":
                    newlast.cell.Y -= 20;
                    break;
                case "in stanga":
                    newlast.cell.X += 20;
                    break;
                case "in dreapta":
                    newlast.cell.X -= 20;
                    break;
                default:
                    break;
            }
        }
    }
}
