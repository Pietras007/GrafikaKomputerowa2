using Grafika.Extentions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class Triangle
    {
        public Vertice A { get; set; }
        public Vertice B { get; set; }
        public Vertice C { get; set; }
        public Triangle(Vertice A, Vertice B, Vertice C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }
        public Vertice GetVertice(Vertice vertice)
        {
            if(A.IsVertice(vertice))
            {
                return A;
            }
            else if(B.IsVertice(vertice))
            {
                return B;
            }
            else if (C.IsVertice(vertice))
            {
                return C;
            }
            else
            {
                return null;
            }
        }

        public List<AETPointer>[] GetETTable()
        {
            List<AETPointer>[] aETPointers = new List<AETPointer>[GetYmax() + 1];
            for (int i = 0; i < aETPointers.Length; i++)
            {
                aETPointers[i] = new List<AETPointer>();
            }

            AETPointer AB = new AETPointer(A, B);
            AETPointer BC = new AETPointer(B, C);
            AETPointer CA = new AETPointer(C, A);
            if (1/AB.m != 0)
            {
                aETPointers[GetYmin(A, B)].Add(AB);
            }
            if (1/BC.m != 0)
            {
                aETPointers[GetYmin(B, C)].Add(BC);
            }
            if (1/CA.m != 0)
            {
                aETPointers[GetYmin(C, A)].Add(CA);
            }
            for (int i=0;i<aETPointers.Length;i++)
            {
                aETPointers[i] = aETPointers[i].OrderBy(o => o.X).ThenBy(x => x.m).ToList();
            }
            return aETPointers;
        }

        public int GetYmax()
        {
            int yMax = 0;
            if(A.Y > yMax)
            {
                yMax = A.Y;
            }
            if(B.Y > yMax)
            {
                yMax = B.Y;
            }
            if(C.Y > yMax)
            {
                yMax = C.Y;
            }
            return yMax;
        }

        public int GetYmin(Vertice A, Vertice B)
        {
            if(A.Y < B.Y)
            {
                return A.Y;
            }
            else
            {
                return B.Y;
            }
        }
    }
}
