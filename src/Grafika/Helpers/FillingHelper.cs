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
        public static void FillDokladne(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy, Vector N, Vector V)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    if (x < CONST.CONST.bitmapX && y < CONST.CONST.bitmapY)
                    {
                        Color color = backColor;
                        if (wypelnienie == Wypelnienie.Tekstura)
                        {
                            color = sampleImage[x, y];
                        }

                        if (opcjaWektoraN == OpcjaWektoraN.Tekstura)
                        {
                            var a = VectorHelper.CountVectorN(normalMap[x, y]);
                            N = new Vector( a.Item1, a.Item2,a.Item3);
                        }
                        Vector L = new Vector(0, 0, 1);
                        if(trybPracy == TrybPracy.SwiatloWedrujace)
                        {
                            L = VectorHelper.CountVectorL(x, y, lightSource);
                        }
                        Vector R = VectorHelper.CreateVectorR(N, L);
                        colorToPaint[x, y] = ColorHelper.CalculateColorToPaint(kd, ks, m, lightColor, color, N, L, V, R);
                    }
                }
            }
        }

        public static void FillInterpolowane(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, (Color, Color, Color) triangleColorsABC, Triangle triangle)
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
            if(x > 1)
            {
                return 1;
            }
            else if(x < 0)
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
