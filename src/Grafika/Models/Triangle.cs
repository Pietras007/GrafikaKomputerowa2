using Grafika.CONST;
using Grafika.Extentions;
using Grafika.Helpers;
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
        public double KS { get; set; }
        public double KD { get; set; }
        public int M { get; set; }
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

        public (Color, (int, int))[] GetColorsABC(Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy)
        {
            (Color, (int, int))[] resultColors = new (Color, (int, int))[3];
            for(int i=0;i<vertices.Count;i++)
            {
                var vertice = vertices[i];
                int x = vertice.X;
                int y = vertice.Y;
                //if (x < CONST.CONST.bitmapX && y < CONST.CONST.bitmapY)
                {
                    Color color = backColor;
                    if (wypelnienie == Wypelnienie.Tekstura)
                    {
                        color = sampleImage[x, y];
                    }

                    (double, double, double) N = (0, 0, 1);
                    if (opcjaWektoraN == OpcjaWektoraN.Tekstura)
                    {
                        N = VectorHelper.CountVectorN(normalMap[x, y]);
                    }

                    (double, double, double) L = (0, 0, 1);
                    if (trybPracy == TrybPracy.SwiatloWedrujace)
                    {
                        L = VectorHelper.CountVectorL(x, y, lightSource);
                    }
                    resultColors[i] = (ColorHelper.CalculateColorToPaint(kd, ks, m, lightColor, color, N, L), (x, y));
                }

            }
            
            return resultColors;
        }

        private int RoundX(int x)
        {
            if (x < 0)
                return x;
            else if (x >= CONST.CONST.bitmapX)
                return CONST.CONST.bitmapX - 1;
            else
                return x;
        }

        private int RoundY(int y)
        {
            if (y < 0)
                return y;
            else if (y >= CONST.CONST.bitmapY)
                return CONST.CONST.bitmapY - 1;
            else
                return y;
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
