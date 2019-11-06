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
        public static Color CalculateColorToPaint(double kd, double ks, int m, Color lightColor, Color color, (double, double, double) N, (double, double, double) L)
        {
            (double, double, double) V = (0, 0, 1);
            double cosNL = FillingHelper.Round01(VectorHelper.CountCosunis(N, L));
            (double, double, double) R = VectorHelper.NormalizeVector(VectorHelper.CreateVectorR(N, L, cosNL));
            double cosVR = FillingHelper.Round01(VectorHelper.CountCosunis(V, R));
            double Ir = FillingHelper.Round01(kd * ((double)lightColor.R / 255) * ((double)color.R / 255) * cosNL + ks * ((double)lightColor.R / 255) * ((double)color.R / 255) * Math.Pow(cosVR, m));
            double Ig = FillingHelper.Round01(kd * ((double)lightColor.G / 255) * ((double)color.G / 255) * cosNL + ks * ((double)lightColor.G / 255) * ((double)color.G / 255) * Math.Pow(cosVR, m));
            double Ib = FillingHelper.Round01(kd * ((double)lightColor.B / 255) * ((double)color.B / 255) * cosNL + ks * ((double)lightColor.B / 255) * ((double)color.B / 255) * Math.Pow(cosVR, m));
            return Color.FromArgb((int)(Ir * 255), (int)(Ig * 255), (int)(Ib * 255));
        }
    }
}
