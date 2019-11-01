using Grafika.CONST;
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

        public static void PaintDokladne(this Graphics g, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m)
        {

        }

        public static void PaintHybrydowe(this Graphics g, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m)
        {

        }

        public static void PaintInterpolowane(this Graphics g, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m)
        {

        }
    }
}
