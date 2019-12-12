using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public abstract class _2Dobject
    {
        public _2Dsize Position { get; protected set; }

        #region position

        public void SetPosition(_2Dsize position)
        {
            this.Position = position;
        }
        public void SetPosition(double X, double Y)
        {
            this.Position = new _2Dsize(X, Y);
        }

        #endregion

    }
}
