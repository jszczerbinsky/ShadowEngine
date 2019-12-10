using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild
{
    public class Camera : _2DobjectResizeable
    {
        public static Camera defaultCam;

        public Camera(_2Dsize position, _2Dsize size)
        {
            this.position = position;
            this.size = size;
        }
        public Camera(double x, double y, double width, double height)
        {
            this.position = new _2Dsize(x, y);
            this.size = new _2Dsize(width, height);
        }
    }
}
