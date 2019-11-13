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
        public static void FillDokladne(Color[,] colorToPaint, List<AETPointer> AET, int y, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy, (int, int) mouse, double waveDistance)
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
                    else if(opcjaWektoraN == OpcjaWektoraN.Babelek)
                    {
                        double distance = DistanceHelper.Distance((x, y), mouse);
                        if (distance < CONST.CONST.radial)
                        {
                            double h = DistanceHelper.HeightOfN(distance);
                            N = VectorHelper.NormalizeVector((x - mouse.Item1, y - mouse.Item2, h));
                        }
                    }
                    else if(opcjaWektoraN == OpcjaWektoraN.Fala)
                    {
                        double distance = DistanceHelper.Distance((x, y), mouse);
                        if(distance > waveDistance - CONST.CONST.waveLength && distance < waveDistance + CONST.CONST.waveLength)
                        {
                            double h = DistanceHelper.HeightOfNInWave(distance, waveDistance);
                            (double, double) line = LineHelper.GetStraightLine((x, y), mouse);
                            ((int, int), (int, int)) vertices = LineHelper.GetPointFromLineDistanceAndPoint(line, waveDistance, mouse);
                            (int, int) centreOfWave = (-1, -1);
                            if (x == mouse.Item1)
                            {
                                centreOfWave = LineHelper.GetCloserVerticeFromVertice(((mouse.Item1, mouse.Item2 - (int)waveDistance),(mouse.Item1, mouse.Item2 + (int)waveDistance)), (x, y));
                            }
                            else
                            {
                                centreOfWave = LineHelper.GetCloserVerticeFromVertice(vertices, (x, y));
                            }
                            N = VectorHelper.NormalizeVector((x - centreOfWave.Item1, y - centreOfWave.Item2, h));
                        }
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

        public static void FillInterpolowane(Color[,] colorToPaint, List<AETPointer> AET, int y, (Color, (int, int))[] triangleColorsABC)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    colorToPaint[x, y] = TriangleHelper.CountBarycentricCoordinateSystemColor(triangleColorsABC, x, y);
                }
            }
        }

        public static void FillHybrydowe(Color[,] colorToPaint, List<AETPointer> AET, int y, double ks, double kd, int m, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, TrybPracy trybPracy, ((Color, (double, double, double)), (int, int))[] triangleValues)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    (Color, (double, double, double)) colorAndN = TriangleHelper.CountBarycentricCoordinateSystemColorAndVector(triangleValues, x, y);
                    (double, double, double) L = (0, 0, 1);
                    if (trybPracy == TrybPracy.SwiatloWedrujace)
                    {
                        L = VectorHelper.CountVectorL(x, y, lightSource);
                    }

                    colorToPaint[x, y] = ColorHelper.CalculateColorToPaint(kd, ks, m, lightColor, colorAndN.Item1, colorAndN.Item2, L);
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
