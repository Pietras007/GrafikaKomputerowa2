using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class VectorHelper
    {
        public static Vector CreateVectorR(Vector N, Vector L)
        {
            //return new Vector(2 * N.X - L.X, 2 * N.Y - L.Y, 2 * N.Z - L.Z);
            double cosinus = CountCosunis(N, L);
            return new Vector(2 *cosinus* N.X - L.X, 2 * cosinus * N.Y - L.Y, 2 * cosinus * N.Z - L.Z);
        }

        public static double CountCosunis(Vector A, Vector B)
        {
            return A.X * B.X + A.Y * B.Y + A.Z * B.Z;
        }

        public static (double, double, double) CountVectorN(Color color)
        {
            double R = (double)(color.R - 127) / 127;
            double G = -(double)(color.G - 127) / 127;
            double B = (double)(color.B) / 255;
            return NormalizeVector((R, G, B));
        }

        public static Vector CountVectorL(int x, int y, (int, int, int) lightSource)
        {
            Vector L = new Vector(lightSource.Item1 - x, lightSource.Item2 - y, lightSource.Item3);
            return NormalizeVector(L);
        }



        public static Vector NormalizeVector(Vector vector)
        {
            double length = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
            vector.X /= length;
            vector.Y /= length;
            vector.Z /= length;
            return vector;
        }

        public static (double, double, double) NormalizeVector((double, double, double) vector)
        {
            double length = Math.Sqrt(Math.Pow(vector.Item1, 2) + Math.Pow(vector.Item2, 2) + Math.Pow(vector.Item3, 2));
            vector.Item1 /= length;
            vector.Item2 /= length;
            vector.Item3 /= length;
            return vector;
        }
    }
}
