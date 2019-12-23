using ShadowBuild.Objects;
using ShadowBuild.Objects.Dimensions;
using System.Collections.Generic;
using System.Drawing;

namespace ShadowBuild.Rendering
{
    public class Camera : _2DobjectResizable
    {
        public static Camera Default;
        public static DefaultCameraMode DefaultMode;

        public readonly List<Layer> RenderLayers = new List<Layer>() { Layer.Default };
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
        public Camera(_2Dsize position, _2Dsize size, List<Layer> layers)
        {
            this.Position = position;
            this.Size = size;
            this.RenderLayers = layers;
        }

        public bool IsRendering(Layer l)
        {
            foreach (Layer l1 in RenderLayers)
            {
                if (l1 == l) return true;
            }
            return false;
        }
    }
}
