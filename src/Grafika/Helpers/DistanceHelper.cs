using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class DistanceHelper
    {
        public static double Distance((int, int) A, (int, int) B)
        {
            return Math.Sqrt(Math.Pow(A.Item1 - B.Item1, 2) + Math.Pow(A.Item2 - B.Item2, 2));
        }

        public static double HeightOfN(double distance)
        {
            return Math.Sqrt(Math.Pow(CONST.CONST.radial, 2) - Math.Pow(distance, 2));
        }

        public static double HeightOfNInWave(double distance, double waveDistance)
        {
            return Math.Sqrt(Math.Pow(CONST.CONST.waveLength, 2) - Math.Pow(distance - waveDistance, 2));
        }
    }
}
