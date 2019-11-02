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
            return new Vector(2 * N.X - L.X, 2 * N.Y - L.Y, 2 * N.Z - L.Z);
        }

        public static double CountCosunis(Vector A, Vector B)
        {
            return A.X * B.X + A.Y * B.Y + A.Z * B.Z;
        }

        public static Vector CountVectorN(Color color)
        {
            int R = color.R - 127;
            int G = color.G - 127;
            int B = color.B - 127;
            double length = Math.Sqrt(Math.Pow(R, 2) + Math.Pow(G, 2) + Math.Pow(B, 2));
            return new Vector(R / length, G / length, B / length);
            //return new Vector((color.R - 127) / 127, (color.G - 127) / 127, (color.B - 127) / 127);
        }
    }
}
