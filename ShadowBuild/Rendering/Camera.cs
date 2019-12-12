using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Rendering
{
    public class Camera : _2DobjectResizeable
    {
        public static Camera Default;
        public static DefaultCameraMode DefaultMode;

        public readonly List<Layer> RenderLayers = new List<Layer>();
        public Color Background = Color.Gray;

        public Camera(_2Dsize position, _2Dsize size)
        {
            this.Position = position;
            this.Size = size;
        }
        public Camera(double x, double y, double width, double height)
        {
            this.Position = new _2Dsize(x, y);
            this.Size = new _2Dsize(width, height);
        }
    }
}
