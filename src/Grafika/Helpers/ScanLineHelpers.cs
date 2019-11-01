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
        public static void ScanLinePaint(this Graphics g, List<AETPointer> AET, int y, Picture picture, Wypelnienie wypelnienie, Bitmap sampleImage, Color backColor, TrybPracy trybPracy, double ks, double kd, int m)
        {
            if(AET.Count %2 != 0)
            {
                int dupa = 2;
            }
            else
            {
                for(int i=0;i<AET.Count;i+=2)
                {
                    for(int x = (int)AET[i].X; x <= (int)AET[i+1].X; x++)
                    {
                        using (SolidBrush solidBrush = new SolidBrush(backColor))
                        {
                            g.FillRectangle(solidBrush, x, y, 1, 1);
                        }
                    }
                }
            }
        }
    }
}
