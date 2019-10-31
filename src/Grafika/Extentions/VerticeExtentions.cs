using Grafika.Constans;
using Grafika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Extentions
{
    public static class VerticeExtentions
    {
        public static bool IsVertice(this Vertice vertice, Vertice v)
        {
            if(vertice.X - CONST.pointRound < v.X && v.X < vertice.X + CONST.pointRound && vertice.Y - CONST.pointRound < v.Y && v.Y < vertice.Y + CONST.pointRound)
            {
                return true;
            }
            return false;
        }
    }
}
