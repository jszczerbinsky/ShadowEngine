using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Dimensions
{
    public class _2Dsize
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public _2Dsize()
        {
            this.X = 0;
            this.Y = 0;
        }

        public _2Dsize(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static _2Dsize Add(_2Dsize size1, _2Dsize size2)
        {
            return new _2Dsize(size1.X + size2.X, size1.Y+ size2.Y);
        }

        public void SetDimensions(double newX, double newY)
        {
            this.X = newX;
            this.Y = newY;
        }
    }
}
