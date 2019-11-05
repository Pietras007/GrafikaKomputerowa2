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

        public static (Color,(double, double, double)) CountBarycentricCoordinateSystemColorAndVector(((Color, (double, double, double)), (int, int))[] triangleColorAndVectorABC, int x, int y)
        {
            double ABC = TriangleArea(triangleColorAndVectorABC[0].Item2, triangleColorAndVectorABC[1].Item2, triangleColorAndVectorABC[2].Item2);
            double alpha = TriangleArea((x, y), triangleColorAndVectorABC[1].Item2, triangleColorAndVectorABC[2].Item2) / ABC;
            double beta = TriangleArea(triangleColorAndVectorABC[0].Item2, (x, y), triangleColorAndVectorABC[2].Item2) / ABC;
            double gamma = TriangleArea(triangleColorAndVectorABC[0].Item2, triangleColorAndVectorABC[1].Item2, (x, y)) / ABC;
            int R_ = (int)Round255(alpha * triangleColorAndVectorABC[0].Item1.Item1.R + beta * triangleColorAndVectorABC[1].Item1.Item1.R + gamma * triangleColorAndVectorABC[2].Item1.Item1.R);
            int G_ = (int)Round255(alpha * triangleColorAndVectorABC[0].Item1.Item1.G + beta * triangleColorAndVectorABC[1].Item1.Item1.G + gamma * triangleColorAndVectorABC[2].Item1.Item1.G);
            int B_ = (int)Round255(alpha * triangleColorAndVectorABC[0].Item1.Item1.B + beta * triangleColorAndVectorABC[1].Item1.Item1.B + gamma * triangleColorAndVectorABC[2].Item1.Item1.B);

            double X = alpha * triangleColorAndVectorABC[0].Item1.Item2.Item1 + beta * triangleColorAndVectorABC[1].Item1.Item2.Item1 + gamma * triangleColorAndVectorABC[2].Item1.Item2.Item1;
            double Y = alpha * triangleColorAndVectorABC[0].Item1.Item2.Item2 + beta * triangleColorAndVectorABC[1].Item1.Item2.Item2 + gamma * triangleColorAndVectorABC[2].Item1.Item2.Item2;
            double Z = alpha * triangleColorAndVectorABC[0].Item1.Item2.Item3 + beta * triangleColorAndVectorABC[1].Item1.Item2.Item3 + gamma * triangleColorAndVectorABC[2].Item1.Item2.Item3;
            return (Color.FromArgb(R_, G_, B_), VectorHelper.NormalizeVector((X, Y, Z)));
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
