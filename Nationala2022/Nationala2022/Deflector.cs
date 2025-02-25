using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2022
{
    public class Deflector
    {
        public static int width;
        public static int height;
        public static Bitmap current;
        public static Dictionary<int, (Point, Point, Point)> types = new Dictionary<int, (Point, Point, Point)>();

        public static void Init()
        {
            Point upperLeft = new Point(0, 0);
            Point upperRight = new Point(width, 0);
            Point lowerLeft = new Point(0, height);
            Point lowerRight = new Point(width, height);

            types.Add(0, (upperLeft, upperRight, lowerLeft));
            types.Add(1, (upperLeft, upperRight, lowerRight));
            types.Add(2, (lowerLeft, upperRight, lowerRight));
            types.Add(3, (lowerLeft, upperLeft, lowerRight));
        }

        public static void resetBitmap()
        {
            current = new Bitmap(width, height);
        }
    }
}
