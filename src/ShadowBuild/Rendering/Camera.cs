using ShadowBuild.Objects;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Rendering
{

    /// <summary>
    /// Camera class.
    /// You can use cameras to render different parts of a world.
    /// </summary>
    public class Camera : _2Dobject
    {
        /// <value>Default camera</value>
        public static Camera Default;

        /// <value>Gets all render layers, that this camera renders</value>
        public readonly List<Layer> RenderLayers = new List<Layer>() { Layer.Default };

        /// <value>Camera background</value>
        public System.Drawing.Color Background = System.Drawing.Color.Gray;

        /// <value>Gets start point of rendered area of camera</value>
        public Point StartPosition
        {
            get
            {
                return new Point(this.Position.X - this.BaseSize.Width / 2, this.Position.Y - this.BaseSize.Height / 2);
            }
        }

        /// <value>Gets end point of rendered area of camera</value>
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

        /// <value>Returns true if camera renders a layer</value>
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
