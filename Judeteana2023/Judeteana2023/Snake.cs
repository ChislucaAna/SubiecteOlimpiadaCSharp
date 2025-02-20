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

        public static Action<SnakeBody> in_sus = (r) => r.cell.Y-=20;
        public static Action<SnakeBody> in_jos = (r) => r.cell.Y+=20;
        public static Action<SnakeBody> in_stanga = (r) => r.cell.X -= 20;
        public static Action<SnakeBody> in_dreapta = (r) => r.cell.X += 20;
        public Snake(Point head)
        {
            body = new List<SnakeBody>();
            body.Add(new SnakeBody(new Rectangle(head, new Size(cellSize, cellSize)),"in jos"));
        }

        public bool isHead(Rectangle r)
        {
            if (r.Equals(body.First().cell))
                return true;
            return false;
        }

        public void UpdateDirection()
        {
            for(int i = 0; i < body.Count-1; i++)
            {
                var current = body[i];
                body[i].direction = current.direction;
                current.direction = body[i + 1].direction;
                
            }
        }

        public void UpdatePosition()
        {
            for (int i = 0; i < body.Count; i++)
            {
                var current = body[i];
                schimba_pozitie[current.direction](current);
                Console.WriteLine(current.cell.X.ToString());
            }
        }
    }
}
