using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class LineHelper
    {
        public static (double, double) GetStraightLine((int, int) pStart, (int,int) pEnd)
        {
            int A = pStart.Item2 - pEnd.Item2;
            int B = pEnd.Item1 - pStart.Item1;
            int C = pStart.Item2 * (pStart.Item1 - pEnd.Item1) + pStart.Item1 * (pEnd.Item2 - pStart.Item2);

            double a = (double)A / B;
            double b = (double)C / B;
            return (a, b);
        }

        public static ((int,int), (int, int)) GetPointFromLineDistanceAndPoint((double, double) line, double distance, (int, int) point)
        {
            double a = line.Item1;
            double x = Math.Sqrt(Math.Pow(distance, 2) / (Math.Pow(a, 2) + 1));
            double y = a * x;
            int X = (int)x;
            int Y = (int)y;
            return ((point.Item1 + X, point.Item2 - Y),(point.Item1 - X, point.Item2 + Y));
        }

        public static (int, int) GetCloserVerticeFromVertice(((int, int), (int, int)) vertices, (int, int) vertice)
        {
            double dist1 = DistanceBetween(vertices.Item1, vertice);
            double dist2 = DistanceBetween(vertices.Item2, vertice);
            if (dist1 <= dist2)
            {
                return vertices.Item1;
            }
            else
            {
                return vertices.Item2;
            }
        }

        public static double DistanceBetween((int, int) a, (int, int) b)
        {
            return Math.Sqrt(Math.Pow(b.Item1 - a.Item1, 2) + Math.Pow(b.Item2 - a.Item2, 2));
        }
    }
}
