using Grafika.CONST;
using Grafika.Helpers;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Extentions
{
    public static class GraphicsExtentions
    {
        public static void PaintTriangleLines(this Graphics g, Pen pen, Triangle triangle)
        {
            g.DrawLine(pen, triangle.A.X, triangle.A.Y, triangle.B.X, triangle.B.Y);
            g.DrawLine(pen, triangle.B.X, triangle.B.Y, triangle.C.X, triangle.C.Y);
            g.DrawLine(pen, triangle.A.X, triangle.A.Y, triangle.C.X, triangle.C.Y);
        }

        public static void Paint(this Graphics g, Picture picture, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, bool randomKdKsM, bool triangleWeb, (int, int) mouse, double waveDistance)
        {
            var colorToPaint = new Color[CONST.CONST.bitmapX + 1, CONST.CONST.bitmapY + 1];
            PaintHandler(colorToPaint, picture, wypelnienie, sampleImage, normalMap, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, lightSource, randomKdKsM, mouse, waveDistance);

            using (Bitmap processedBitmap = new Bitmap(CONST.CONST.bitmapX, CONST.CONST.bitmapY))
            {
                unsafe
                {
                    BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                    int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                    Parallel.For(0, heightInPixels, y =>
                    {
                        byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            if (colorToPaint[x / 4, y].A == 255)
                            {
                                currentLine[x] = colorToPaint[x / 4, y].B;
                                currentLine[x + 1] = colorToPaint[x / 4, y].G;
                                currentLine[x + 2] = colorToPaint[x / 4, y].R;
                                currentLine[x + 3] = colorToPaint[x / 4, y].A;
                            }
                            else
                            {
                                if (wypelnienie == Wypelnienie.Tekstura)
                                {
                                    currentLine[x] = sampleImage[x / 4, y].B;
                                    currentLine[x + 1] = sampleImage[x / 4, y].G;
                                    currentLine[x + 2] = sampleImage[x / 4, y].R;
                                    currentLine[x + 3] = sampleImage[x / 4, y].A;
                                }
                                else
                                {
                                    currentLine[x] = backColor.B;
                                    currentLine[x + 1] = backColor.G;
                                    currentLine[x + 2] = backColor.R;
                                    currentLine[x + 3] = backColor.A;
                                }
                            }
                        }
                    });
                    processedBitmap.UnlockBits(bitmapData);
                }

                g.DrawImage(processedBitmap, 0, 0);
            }

            if (triangleWeb)
            {
                using (Pen blackPen = new Pen(Color.Black))
                {
                    foreach (var triangle in picture.Triangles)
                    {
                        g.PaintTriangleLines(blackPen, triangle);
                    }
                }
            }

        }

        public static void PaintHandler(Color[,] colorToPaint, Picture picture, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor2, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, bool randomKdKsM, (int, int) mouse, double waveDistance)
        {
            Parallel.For(0, picture.Triangles.Count, (i) =>
            {
                Triangle triangle = picture.Triangles[i];
                PaintTriangle(colorToPaint, triangle, wypelnienie, sampleImage, normalMap, backColor2, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, lightSource, randomKdKsM, mouse, waveDistance);
            });

            //foreach (var triangle in picture.Triangles)
            //{
            //    g.PaintTriangle(colorToPaint, triangle, wypelnienie, sampleImage, normalMap, backColor2, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, lightSource, randomKdKsM);
            //}
        }

        public static void PaintTriangle(Color[,] colorToPaint, Triangle triangle, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, (int, int, int) lightSource, bool randomKdKsM, (int, int) mouse, double waveDistance)
        {
            var data = triangle.GetETTable();
            List<AETPointer>[] ET = data.Item1;
            List<AETPointer> AET = new List<AETPointer>();

            //Random randomKdKsM
            if(randomKdKsM)
            {
                ks = triangle.KS;
                kd = triangle.KD;
                m = triangle.M;
            }

            //GetColors inTriangle
            (Color, (int, int))[] triangleCountedColorsABC = null;
            ((Color, (double, double, double)), (int, int))[] triangleValues = null;
            if (rodzajMalowania == RodzajMalowania.Interpolowane)
            {
                triangleCountedColorsABC = triangle.CountColorsABC(wypelnienie, sampleImage, normalMap, backColor, ks, kd, m, lightColor, opcjaWektoraN, lightSource, trybPracy, mouse, waveDistance);
            }
            else if(rodzajMalowania == RodzajMalowania.Hybrydowe)
            {
                triangleValues = triangle.GetColorsAndVectorsABC(wypelnienie, sampleImage, normalMap, backColor, ks, kd, m, lightColor, opcjaWektoraN, lightSource, trybPracy, mouse, waveDistance);
            }

            for (int y = data.Item2; y <= ET.Length - 1; y++)
            {
                if (rodzajMalowania == RodzajMalowania.Dokladne)
                {
                    FillingHelper.FillDokladne(colorToPaint, AET, y, wypelnienie, sampleImage, normalMap, backColor, ks, kd, m, lightColor, opcjaWektoraN, lightSource, trybPracy, mouse, waveDistance);
                }
                else if(rodzajMalowania == RodzajMalowania.Interpolowane)
                {
                    FillingHelper.FillInterpolowane(colorToPaint, AET, y, triangleCountedColorsABC);
                }
                else if(rodzajMalowania == RodzajMalowania.Hybrydowe)
                {
                    FillingHelper.FillHybrydowe(colorToPaint, AET, y, ks, kd, m, lightColor, opcjaWektoraN, lightSource, trybPracy, triangleValues);
                }

                for (int i = AET.Count - 1; i >= 0; i--)
                {
                    if (AET[i].Ymax == y)
                    {
                        AET.RemoveAt(i);
                    }
                }

                if (ET[y] != null)
                {
                    AET.AddRange(ET[y]);
                    AET = AET.OrderBy(o => o.X).ThenBy(x => x.m).ToList();
                }

                for (int i = 0; i < AET.Count; i++)
                {
                    AET[i].X += AET[i].m;
                }
            }
        }
    }
}
