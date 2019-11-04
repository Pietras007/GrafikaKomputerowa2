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

        public static Vector CountVectorN(Color color)
        {
            double R = (double)(color.R - 127) / 127;
            double G = (double)(color.G - 127) / 127;
            double B = (double)(color.B) / 255;
            return NormalizeVector(new Vector(R, G, B));
        }

        public static Vector NormalizeVector(Vector vector)
        {
            double length = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2) + Math.Pow(vector.Z, 2));
            return new Vector(vector.X / length, vector.Y / length, vector.Z / length);
        }
    }
}
