using Grafika.CONST;
using Grafika.Helpers;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            foreach (var triangle in picture.Triangles)
            {
                Random random = new Random();
                int red = random.Next(0, 255);
                int green = random.Next(0, 255);
                int blue = random.Next(0, 255);
                Color backColor = Color.FromArgb(red, green, blue);
                List<AETPointer>[] ET = triangle.GetETTable();
                List<AETPointer> AET = new List<AETPointer>();
                for (int y = 0; y <= ET.Length - 1; y++)
                {
                    g.Fill(AET, y, picture, wypelnienie, sampleImage, backColor, trybPracy, ks, kd, m, rodzajMalowania, lightColor, opcjaWektoraN, triangle, vectorL);

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
