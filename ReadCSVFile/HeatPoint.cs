using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSVFile
{
    public class HeatPoint
    {
        public int X;
        public int Y;
        public byte Intensity;

        public HeatPoint(int X, int Y, byte Intensity)
        {
            this.X = X;
            this.Y = Y;
            this.Intensity = Intensity; 
        }
    }
}
