using ShadowBuild.Objects;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Rendering
{
    public class Camera : _2Dobject
    {
        public static Camera Default;

        public readonly List<Layer> RenderLayers = new List<Layer>() { Layer.Default };
        public System.Drawing.Color Background = System.Drawing.Color.Gray;
        public Point StartPosition
        {
            get
            {
                return new Point(this.Position.X - this.BaseSize.Width / 2, this.Position.Y - this.BaseSize.Height / 2);
            }
        }
        public Point EndPosition
        {
            get
            {
                return new Point(this.Position.X + this.BaseSize.Width / 2, this.Position.Y + this.BaseSize.Height / 2);
            }
        }

        public Camera(Point position, Size size)
        {
            this.Position = position;
            this.BaseSize = size;
        }
        public Camera(double x, double y, double width, double height)
        {
            this.Position = new Point(x, y);
            this.BaseSize = new Size(width, height);
        }
        public Camera(Point position, Size size, List<Layer> layers)
        {
            this.Position = position;
            this.BaseSize = size;
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
