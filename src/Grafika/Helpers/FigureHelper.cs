using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Helpers
{
    public static class FigureHelper
    {
        public static void InitializePicture(this Picture picture, int sizeX, int sizeY)
        {
            picture.Triangles.Clear();
            picture.GenerateSampleTriangle(sizeX, sizeY);
        }
    }
}
