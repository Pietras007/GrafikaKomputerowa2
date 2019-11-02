using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class AETPointer
    {
        public int Ymax { get; set; }
        public double X { get; set; }
        public double m { get; set; }

        public AETPointer(int _Ymax, double _X, double _m)
        {
            Ymax = _Ymax;
            X = _X;
            m = _m;
        }

        public AETPointer(Vertice A, Vertice B)
        {
            if(A.Y > B.Y)
            {
                Ymax = A.Y;
                X = B.X;
            }
            else
            {
                Ymax = B.Y;
                X = A.X;
            }

            if((B.Y > A.Y && B.X > A.X) || (B.Y < A.Y && B.X < A.X))
            {
                m = 1 / (Math.Abs(B.Y - A.Y) / (double)Math.Abs(B.X - A.X));
            }
            else
            {
                m = -1 / (Math.Abs(B.Y - A.Y) / (double)Math.Abs(B.X - A.X));
            }
        }
    }
}
