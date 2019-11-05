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
        public static (double, double, double) CreateVectorR((double, double, double) N, (double, double, double) L, double cosinusNL)
        {
            return (2 * cosinusNL * N.Item1 - L.Item1, 2 * cosinusNL * N.Item2 - L.Item2, 2 * cosinusNL * N.Item3 - L.Item3);
        }

        public static double CountCosunis((double, double, double) A, (double, double, double) B)
        {
            return A.Item1 * B.Item1 + A.Item2 * B.Item2 + A.Item3 * B.Item3;
        }

        public static (double, double, double) CountVectorN(Color color)
        {
            double R = (double)(color.R - 127) / 127;
            double G = -(double)(color.G - 127) / 127;
            double B = (double)(color.B) / 255;
            return NormalizeVector((R, G, B));
        }

        public static (double, double, double) CountVectorL(int x, int y, (int, int, int) lightSource)
        {
            lightSource.Item1 = lightSource.Item1 - x;
            lightSource.Item2 = lightSource.Item2 - y;
            return NormalizeVector(lightSource);
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
