using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class Vertice
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vertice(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveVerticeTo(Vertice vertice)
        {
            if (vertice.X >= 0 && vertice.X <= CONST.CONST.bitmapX && vertice.Y >= 0 && vertice.Y <= CONST.CONST.bitmapY)
            {
                X = vertice.X;
                Y = vertice.Y;
            }
        }
    }
}
