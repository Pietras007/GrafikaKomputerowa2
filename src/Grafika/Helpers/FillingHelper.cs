using Grafika.CONST;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class FillingHelper
    {
        public static void Fill(this Graphics g, List<AETPointer> AET, int y, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m, RodzajMalowania rodzajMalowania, Color lightColor, WektorN wektorN, Triangle triangle, double LX, double LY, double LZ)
        {

            double Lx = 0;
            double Ly = 0;
            double Lz = 1;
            if (trybPracy == TrybPracy.SwiatloWedrujace)
            {
                Lx = LX;
                Ly = LY;
                Lz = LZ;
            }

            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    if (x < 720 && y < 576)
                    {
                        Color color = backColor;
                        if (wypelnienie == Wypelnienie.Tekstura)
                        {
                            color = sampleImage.GetPixel(x, y);
                        }

                        double Nx = 0;
                        double Ny = 0;
                        double Nz = 1;
                        if(wektorN == WektorN.Tekstura)
                        {
                            Nx = 0;
                            Ny = 0;
                            Nz = 1;
                        }

                        double Vx = 0;
                        double Vy = 0;
                        double Vz = 1;
                        double Rx = 2 * Nx - Lx;
                        double Ry = 2 * Ny - Ly;
                        double Rz = 2 * Nz - Lz;

                        double cosNL = Nx * Lx + Ny * Ly + Nz * Lz;
                        double cosVR = Vx * Rx + Vy * Ry + Vz * Rz;

                        double Ir = kd * ((double)lightColor.R / 255) * ((double)color.R / 255) * cosNL + ks * ((double)lightColor.R / 255) * ((double)color.R / 255) * Math.Pow(cosVR, m);
                        double Ig = kd * ((double)lightColor.G / 255) * ((double)color.G / 255) * cosNL + ks * ((double)lightColor.G / 255) * ((double)color.G / 255) * Math.Pow(cosVR, m);
                        double Ib = kd * ((double)lightColor.B / 255) * ((double)color.B / 255) * cosNL + ks * ((double)lightColor.B / 255) * ((double)color.B / 255) * Math.Pow(cosVR, m);
                        if (Ir > 1)
                        {
                            Ir = 1;
                        }
                        if (Ig > 1)
                        {
                            Ig = 1;
                        }
                        if (Ib > 1)
                        {
                            Ib = 1;
                        }
                        using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb((int)(Ir*255), (int)(Ig*255), (int)(Ib*255))))
                        {
                            g.FillRectangle(solidBrush, x, y, 1, 1);
                        }
                    }
                }
            }
        }
    }
}
