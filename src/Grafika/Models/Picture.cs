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
        public void GenerateSampleTriangle(int n)
        {
            int width = CONST.bitmapX / n;
            int height = CONST.bitmapY / n;
            for(int j=0;j<n;j++)
            {
                for(int i=0;i<n;i++)
                {
                    int x = i/2;
                    Vertice A = new Vertice(x * width, j * height);
                    Vertice B = new Vertice((x + 1) * width, j * height);
                    Vertice C;
                    if (i%2 == 0)
                    {
                        C = new Vertice(x*width, (j + 1) * height);
                    }
                    else
                    {
                        C = new Vertice((x+1) * width, (j + 1) * height);
                    }

                    Triangles.Add(new Triangle(A, B, C));
                }
            }
        }
    }
}
