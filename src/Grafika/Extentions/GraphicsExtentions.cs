using Grafika.CONST;
using Grafika.Helpers;
using Grafika.Models;
using System;
using System.Collections.Generic;
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

        public static void Paint(this Graphics g, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor2, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, OpcjaWektoraN opcjaWektoraN, Vector vectorL)
        {
            List<(Color, int, int)> wholeList = new List<(Color, int, int)>();
            foreach (var triangle in picture.Triangles)
            {
                Random random = new Random();
                int red = random.Next(0, 255);
                int green = random.Next(0, 255);
                int blue = random.Next(0, 255);
                Color backColor = Color.FromArgb(red, green, blue);
                List<AETPointer>[] ET = triangle.GetETTable();
                List<AETPointer> AET = new List<AETPointer>();
                List<(Color, int, int)> list = new List<(Color, int, int)>();
                for (int y = 0; y <= ET.Length - 1; y++)
                {
                    list.AddRange(g.Fill(AET, y, picture, wypelnienie, sampleImage, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, triangle, vectorL));

                    for (int i = AET.Count - 1; i >= 0; i--)
                    {
                        if (AET[i].Ymax == y)
                        {
                            AET.RemoveAt(i);
                        }
                    }
                    
                    if (ET[y].Count > 0)
                    {
                        AET.AddRange(ET[y]);
                        AET = AET.OrderBy(o => o.X).ThenBy(x => x.m).ToList();
                    }

                    for (int i = 0; i < AET.Count; i++)
                    {
                        AET[i].X += AET[i].m;
                    }
                }
                wholeList.AddRange(list);
            }

            using (Bitmap processedBitmap = new Bitmap(CONST.CONST.bitmapX, CONST.CONST.bitmapY))
            {
                unsafe
                {
                    BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
                    int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                    int heightInPixels = bitmapData.Height;
                    int widthInBytes = bitmapData.Width * bytesPerPixel;
                    byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                    for (int i = 0; i < wholeList.Count; i++)
                    {
                        int x = wholeList[i].Item2 * bytesPerPixel;
                        int y = wholeList[i].Item3;
                        byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);

                        currentLine[x] = wholeList[i].Item1.B;
                        currentLine[x + 1] = wholeList[i].Item1.G;
                        currentLine[x + 2] = wholeList[i].Item1.R;
                        currentLine[x + 3] = wholeList[i].Item1.A;
                    }
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
    }
}
