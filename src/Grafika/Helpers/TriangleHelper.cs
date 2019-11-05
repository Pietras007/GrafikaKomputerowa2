using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class TriangleHelper
    {
        public static double TriangleArea((int, int) A, (int, int) B, (int, int) C)
        {
            return (double)Math.Abs((B.Item1 - A.Item1)*(C.Item2 - A.Item2) - (B.Item2 - A.Item2)*(C.Item1 - A.Item1))/2;
        }
    }
}
