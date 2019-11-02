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

        public static void PaintBrak(this Graphics g, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m)
        {
            if (wypelnienie == Wypelnienie.Tekstura)
            {
                g.DrawImage(sampleImage, 0, 0);
            }
            else
            {
                using (SolidBrush colorBrush = new SolidBrush(backColor))
                {
                    g.FillRectangle(colorBrush, 0, 0, CONST.CONST.bitmapX, CONST.CONST.bitmapY);
                }
            }
            using (Pen blackPen = new Pen(Color.Black))
            {
                foreach (var triangle in picture.Triangles)
                {
                    g.PaintTriangleLines(blackPen, triangle);
                }
            }
        }

        public static void Paint(this Graphics g, Picture picture, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, Vector vectorL, bool randomKdKsM)
        {
            var colorToPaint = new Color[CONST.CONST.bitmapX, CONST.CONST.bitmapY];
            g.PaintHandler(colorToPaint, picture, wypelnienie, sampleImage, normalMap, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, vectorL, randomKdKsM);


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
                            currentLine[x] = colorToPaint[x/4,y].B;
                            currentLine[x + 1] = colorToPaint[x/4, y].G;
                            currentLine[x + 2] = colorToPaint[x/4, y].R;
                            currentLine[x + 3] = colorToPaint[x/4, y].A;
                        }
                    });
                    processedBitmap.UnlockBits(bitmapData);
                }

                g.DrawImage(processedBitmap, 0, 0);
            }
            using (Pen blackPen = new Pen(Color.Black))
            {
                foreach (var triangle in picture.Triangles)
                {
                    g.PaintTriangleLines(blackPen, triangle);
                }
            }
        }

        public static void PaintHandler(this Graphics g, Color[,] colorToPaint, Picture picture, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor2, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, Vector vectorL, bool randomKdKsM)
        {
            Parallel.ForEach(picture.Triangles, (triangle) =>
            {
                g.PaintTriangle(colorToPaint, triangle, picture, wypelnienie, sampleImage, normalMap, backColor2, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, vectorL, randomKdKsM);
            });
        }

        public static void PaintTriangle(this Graphics g, Color[,] colorToPaint, Triangle triangle, Picture picture, Wypelnienie wypelnienie, Color[,] sampleImage, Color[,] normalMap, Color backColor, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, Vector vectorL, bool randomKdKsM)
        {
            //Random random = new Random();
            //int red = random.Next(0, 255);
            //int green = random.Next(0, 255);
            //int blue = random.Next(0, 255);
            //Color backColor = Color.FromArgb(red, green, blue);
            //For painting
            var data = triangle.GetETTable();
            List<AETPointer>[] ET = data.Item1;
            List<AETPointer> AET = new List<AETPointer>();

            //Vector L N V
            Vector L = new Vector(0, 0, 1);
            Vector N = new Vector(0, 0, 1);
            Vector V = new Vector(0, 0, 1);
            if (trybPracy == TrybPracy.SwiatloWedrujace)
            {
                L = vectorL;
            }

            //Counting data for triangle
            (Color, Color, Color) triangleColorsABC = (Color.White, Color.White, Color.White);
            if (rodzajMalowania == RodzajMalowania.Interpolowane)
            {
                triangleColorsABC = triangle.GetColorsABC(N, L, V, opcjaWektoraN, wypelnienie, sampleImage, normalMap, backColor, kd, ks, m, lightColor);
            }

            //Random randomKdKsM
            if(randomKdKsM)
            {
                ks = triangle.KS;
                kd = 1.0 - ks;
                m = triangle.M;
            }

            for (int y = data.Item2; y <= ET.Length - 1; y++)
            {
                if (rodzajMalowania == RodzajMalowania.Dokladne)
                {
                    g.FillDokladne(colorToPaint, AET, y, wypelnienie, sampleImage, normalMap, backColor, ks, kd, m, lightColor, opcjaWektoraN, L, N, V);
                }
                else if(rodzajMalowania == RodzajMalowania.Interpolowane)
                {
                    g.FillInterpolowane(colorToPaint, AET, y, triangleColorsABC, triangle);
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
