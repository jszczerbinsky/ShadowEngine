using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public class _2Dobject
    {
        public _2Dsize position { get; private set; }

        #region position

        public void setPosition(_2Dsize position)
        {
            this.position = position;
        }
        public void setPosition(float X, float Y)
        {
            this.position = new _2Dsize(X, Y);
        }

        #endregion

    }
}
