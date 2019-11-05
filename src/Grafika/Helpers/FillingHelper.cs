using Grafika.CONST;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grafika.Helpers;

namespace Grafika.Helpers
{
    public static class FillingHelper
    {
        public static void FillDokladne(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
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

                    colorToPaint[x, y] = ColorHelper.CalculateColorToPaint(kd, ks, m, lightColor, color, N, L);
                }
            }
        }

        public static void FillInterpolowane(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, (Color, (int, int))[] triangleColorsABC)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    double ABC = TriangleHelper.TriangleArea(triangleColorsABC[0].Item2, triangleColorsABC[1].Item2, triangleColorsABC[2].Item2);
                    double alpha = TriangleHelper.TriangleArea((x, y), triangleColorsABC[1].Item2, triangleColorsABC[2].Item2) / ABC;
                    double beta = TriangleHelper.TriangleArea(triangleColorsABC[0].Item2, (x, y), triangleColorsABC[2].Item2) / ABC;
                    double gamma = TriangleHelper.TriangleArea(triangleColorsABC[0].Item2, triangleColorsABC[1].Item2, (x, y)) / ABC;
                    int R_ = (int)Round255(alpha * triangleColorsABC[0].Item1.R + beta * triangleColorsABC[1].Item1.R + gamma * triangleColorsABC[2].Item1.R);
                    int G_ = (int)Round255(alpha * triangleColorsABC[0].Item1.G + beta * triangleColorsABC[1].Item1.G + gamma * triangleColorsABC[2].Item1.G);
                    int B_ = (int)Round255(alpha * triangleColorsABC[0].Item1.B + beta * triangleColorsABC[1].Item1.B + gamma * triangleColorsABC[2].Item1.B);
                    colorToPaint[x, y] = Color.FromArgb(R_, G_, B_);
                }
            }
        }

        public static void FillHybrydowe(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, (Color, Color, Color) triangleColorsABC, Triangle triangle)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    if (x < CONST.CONST.bitmapX && y < CONST.CONST.bitmapY)
                    {
                        var A = triangle.A;
                        var B = triangle.B;
                        var C = triangle.C;
                        double alpha = (x - C.X - (C.Y - y) / (C.Y - B.Y) * B.X - (y - C.Y) / (C.Y - B.Y) * C.X) / (A.X + (A.Y - C.Y) / (C.Y - B.Y) * B.X - C.X - (A.Y - C.Y) / (C.Y - B.Y) * C.X);
                        double beta = alpha * (A.Y - C.Y) / (C.Y - B.Y) + (C.Y - y) / (C.Y - B.Y);
                        double gamma = 1 - alpha - beta;
                        int R_ = (int)Round255(alpha * triangleColorsABC.Item1.R + beta * triangleColorsABC.Item2.R + gamma * triangleColorsABC.Item3.R);
                        int G_ = (int)Round255(alpha * triangleColorsABC.Item1.G + beta * triangleColorsABC.Item2.G + gamma * triangleColorsABC.Item3.G);
                        int B_ = (int)Round255(alpha * triangleColorsABC.Item1.B + beta * triangleColorsABC.Item2.B + gamma * triangleColorsABC.Item3.B);
                        colorToPaint[x, y] = Color.FromArgb(R_, G_, B_);
                    }
                }
            }
        }


        public static double Round01(double x)
        {
            if (x > 1)
            {
                return 1;
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
