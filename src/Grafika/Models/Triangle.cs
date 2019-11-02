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
        public List<Edge> edges { get; set; }
        public List<Vertice> vertices { get; set; }
        public Triangle(Vertice A, Vertice B, Vertice C)
        {
            edges = new List<Edge>();
            vertices = new List<Vertice>();
            this.A = A;
            this.B = B;
            this.C = C;
            edges.Add(new Edge(A, B));
            edges.Add(new Edge(B, C));
            edges.Add(new Edge(C, A));
            vertices.Add(A);
            vertices.Add(B);
            vertices.Add(C);
        }
        public Vertice GetVertice(Vertice vertice)
        {
            foreach(var v in vertices)
            {
                if(v.IsVertice(vertice))
                {
                    return v;
                }
            }
            return null;
        }

        public (List<AETPointer>[], int) GetETTable()
        {
            List<AETPointer>[] aETPointers = new List<AETPointer>[GetYmax() + 1];
            foreach(var e in edges)
            {
                AETPointer aETPointer = new AETPointer(e.start, e.end);
                if(1/aETPointer.m != 0)
                {
                    if(aETPointers[GetYmin(e.start, e.end)] == null)
                    {
                        aETPointers[GetYmin(e.start, e.end)] = new List<AETPointer>();
                    }
                    aETPointers[GetYmin(e.start, e.end)].Add(aETPointer);
                }
            }


            return (aETPointers, GetYminFromAll());
        }

        public int GetYmax()
        {
            int yMax = 0;
            foreach(var v in vertices)
            {
                if(v.Y > yMax)
                {
                    yMax = v.Y;
                }
            }
            return yMax;
        }

        public int GetYminFromAll()
        {
            int yMin = 0;
            foreach (var v in vertices)
            {
                if (v.Y < yMin)
                {
                    yMin = v.Y;
                }
            }
            return yMin;
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
