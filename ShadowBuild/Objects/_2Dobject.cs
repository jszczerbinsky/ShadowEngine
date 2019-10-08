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
        public double rotation { get; private set; }

        #region position

        public void setPosition(_2Dsize position)
        {
            this.position = position;
        }
        public void setPosition(int X, int Y)
        {
            this.position = new _2Dsize(X, Y);
        }
        public void move(int X, int Y)
        {
            //Check collision
            this.position.setDimensions(this.position.X + X, this.position.Y + Y);
        }

        #endregion

       

        #region rotation

        public void setRotation(double rot)
        {
            this.rotation = rot;
        }
        public void rotate(double rot)
        {
            //Check collision
            this.rotation += rot;
        }

        #endregion

    }
}
