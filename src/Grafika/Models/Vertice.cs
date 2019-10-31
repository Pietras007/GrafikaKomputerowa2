﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika.Models
{
    public class Vertice
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vertice(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveVerticeTo(Vertice vertice)
        {
            X = vertice.X;
            Y = vertice.Y;
        }
    }
}
