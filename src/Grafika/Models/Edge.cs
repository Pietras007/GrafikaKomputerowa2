using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class Edge
    {
        public Vertice start { get; set; }
        public Vertice end { get; set; }
        public Edge(Vertice _start, Vertice _end)
        {
            start = _start;
            end = _end;
        }
    }
}
