using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class ColorHelper
    {
        public static Color CalculateColorToPaint(double kd, double ks, int m, Color lightColor, Color color, Vector N, Vector L, Vector V, Vector R)
        {
            double cosNL = VectorHelper.CountCosunis(N, L);
            double cosVR = VectorHelper.CountCosunis(V, R);
            double Ir = kd * ((double)lightColor.R / 255) * ((double)color.R / 255) * cosNL + ks * ((double)lightColor.R / 255) * ((double)color.R / 255) * Math.Pow(cosVR, m);
            double Ig = kd * ((double)lightColor.G / 255) * ((double)color.G / 255) * cosNL + ks * ((double)lightColor.G / 255) * ((double)color.G / 255) * Math.Pow(cosVR, m);
            double Ib = kd * ((double)lightColor.B / 255) * ((double)color.B / 255) * cosNL + ks * ((double)lightColor.B / 255) * ((double)color.B / 255) * Math.Pow(cosVR, m);
            Ir = FillingHelper.Round01(Ir);
            Ig = FillingHelper.Round01(Ig);
            Ib = FillingHelper.Round01(Ib);

            return Color.FromArgb((int)(Ir * 255), (int)(Ig * 255), (int)(Ib * 255));
        }
    }
}
