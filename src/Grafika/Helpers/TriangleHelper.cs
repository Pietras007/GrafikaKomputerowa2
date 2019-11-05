using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class TriangleHelper
    {
        public static Color CountBarycentricCoordinateSystemColor((Color, (int, int))[] triangleColorsABC, int x, int y)
        {
            double ABC = TriangleArea(triangleColorsABC[0].Item2, triangleColorsABC[1].Item2, triangleColorsABC[2].Item2);
            double alpha = TriangleArea((x, y), triangleColorsABC[1].Item2, triangleColorsABC[2].Item2) / ABC;
            double beta = TriangleArea(triangleColorsABC[0].Item2, (x, y), triangleColorsABC[2].Item2) / ABC;
            double gamma = TriangleArea(triangleColorsABC[0].Item2, triangleColorsABC[1].Item2, (x, y)) / ABC;
            int R_ = (int)Round255(alpha * triangleColorsABC[0].Item1.R + beta * triangleColorsABC[1].Item1.R + gamma * triangleColorsABC[2].Item1.R);
            int G_ = (int)Round255(alpha * triangleColorsABC[0].Item1.G + beta * triangleColorsABC[1].Item1.G + gamma * triangleColorsABC[2].Item1.G);
            int B_ = (int)Round255(alpha * triangleColorsABC[0].Item1.B + beta * triangleColorsABC[1].Item1.B + gamma * triangleColorsABC[2].Item1.B);
            return Color.FromArgb(R_, G_, B_);
        }

        public static (double, double, double) CountBarycentricCoordinateSystemVector(((double, double, double), (int, int))[] triangleVectorABC, int x, int y)
        {
            double ABC = TriangleArea(triangleVectorABC[0].Item2, triangleVectorABC[1].Item2, triangleVectorABC[2].Item2);
            double alpha = TriangleArea((x, y), triangleVectorABC[1].Item2, triangleVectorABC[2].Item2) / ABC;
            double beta = TriangleArea(triangleVectorABC[0].Item2, (x, y), triangleVectorABC[2].Item2) / ABC;
            double gamma = TriangleArea(triangleVectorABC[0].Item2, triangleVectorABC[1].Item2, (x, y)) / ABC;
            double X = alpha * triangleVectorABC[0].Item1.Item1 + beta * triangleVectorABC[1].Item1.Item1 + gamma * triangleVectorABC[2].Item1.Item1;
            double Y = alpha * triangleVectorABC[0].Item1.Item2 + beta * triangleVectorABC[1].Item1.Item2 + gamma * triangleVectorABC[2].Item1.Item2;
            double Z = alpha * triangleVectorABC[0].Item1.Item3 + beta * triangleVectorABC[1].Item1.Item3 + gamma * triangleVectorABC[2].Item1.Item3;
            return (X, Y, Z);
        }

        public static double TriangleArea((int, int) A, (int, int) B, (int, int) C)
        {
            return (double)Math.Abs((B.Item1 - A.Item1)*(C.Item2 - A.Item2) - (B.Item2 - A.Item2)*(C.Item1 - A.Item1))/2;
        }

        public static double Round255(double x)
        {
            if (x > 255)
            {
                return 255;
            }
            else if (x < 0)
            {
                return 0;
            }
            else
            {
                return x;
            }
        }
    }
}
