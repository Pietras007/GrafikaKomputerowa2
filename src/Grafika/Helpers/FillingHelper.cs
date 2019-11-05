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
                    colorToPaint[x, y] = TriangleHelper.CountBarycentricCoordinateSystemColor(triangleColorsABC, x, y);
                }
            }
        }

        public static void FillHybrydowe(this Graphics g, Color[,] colorToPaint, List<AETPointer> AET, int y, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy, (Color, (int, int))[] triangleJustColor, ((double, double, double), (int, int))[] triangleVectorABC)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    Color color = TriangleHelper.CountBarycentricCoordinateSystemColor(triangleJustColor, x, y);
                    (double, double, double) N = VectorHelper.NormalizeVector(TriangleHelper.CountBarycentricCoordinateSystemVector(triangleVectorABC, x, y));

                    (double, double, double) L = (0, 0, 1);
                    if (trybPracy == TrybPracy.SwiatloWedrujace)
                    {
                        L = VectorHelper.CountVectorL(x, y, lightSource);
                    }

                    colorToPaint[x, y] = ColorHelper.CalculateColorToPaint(kd, ks, m, lightColor, color, N, L);
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

    }
}
