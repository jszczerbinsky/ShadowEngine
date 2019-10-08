using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Dimensions
{
    public class _2Dsize
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public _2Dsize()
        {
            this.X = 0;
            this.Y = 0;
        }

        public _2Dsize(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void setDimensions(int newX, int newY)
        {
            this.X = newX;
            this.Y = newY;
        }
    }
}
