using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public class _2DobjectResizeable : _2Dobject
    {
        public _2Dsize size { get; protected set; }

        #region size

        public void setSize(_2Dsize size)
        {
            this.size = size;
        }
        public void setSize(double X, double Y)
        {
            this.size = new _2Dsize(X, Y);
        }
        #endregion
    }
}
