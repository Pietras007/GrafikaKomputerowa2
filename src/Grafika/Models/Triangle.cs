using System;
using System.Collections.Generic;
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
    }
}
