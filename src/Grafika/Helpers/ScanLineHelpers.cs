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
    public static class ScanLineHelpers
    {
        public static void ScanLinePaint(this Graphics g, List<AETPointer> AET, int y, Color color)
        {
            for (int i = 0; i < AET.Count; i += 2)
            {
                for (int x = (int)Math.Round(AET[i].X) + 1; x <= Math.Round(AET[i + 1].X); x++)
                {
                    using (SolidBrush solidBrush = new SolidBrush(color))
                    {
                        g.FillRectangle(solidBrush, x, y, 1, 1);
                    }
                }
            }
        }
    }
}
