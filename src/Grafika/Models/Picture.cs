using Grafika.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class Picture
    {
        public List<Triangle> Triangles { get; set; }
        private Vertice[,] listVertices;
        public Picture()
        {
            Triangles = new List<Triangle>();
        }
        public void GenerateSampleTriangle(int n)
        {
            int width = CONST.bitmapX / n;
            int height = CONST.bitmapY / n;
            listVertices = new Vertice[n + 1, n + 1];
            for (int j = 0; j < n + 1; j++)
            {
                for (int i = 0; i < n + 1; i++)
                {
                    listVertices[i, j] = new Vertice(i * width, j * height);
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < 2 * n; i++)
                {
                    int x = i / 2;
                    Vertice A;
                    Vertice B;
                    Vertice C;
                    if (i % 2 == 0)
                    {
                        A = listVertices[x,j];
                        B = listVertices[x + 1, j];
                        C = listVertices[x, j + 1];
                    }
                    else
                    {
                        A = listVertices[x + 1, j + 1];
                        B = listVertices[x + 1, j];
                        C = listVertices[x, j + 1];
                    }

                    Triangles.Add(new Triangle(A, B, C));
                }
            }
        }
    }
}
