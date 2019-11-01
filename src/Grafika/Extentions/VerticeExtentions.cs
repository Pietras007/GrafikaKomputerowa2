using Grafika.CONST;
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
            if(vertice.X - CONST.CONST.pointRound < v.X && v.X < vertice.X + CONST.CONST.pointRound && vertice.Y - CONST.CONST.pointRound < v.Y && v.Y < vertice.Y + CONST.CONST.pointRound)
            {
                return true;
            }
            return false;
        }
    }
}
