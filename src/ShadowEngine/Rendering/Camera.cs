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

        /// <value>Camera background color</value>
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

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="position">Camera position. Position is always in a middle of a camera rendered area.</param>
        /// <param name="size">Size of rendered area.</param>
        public Camera(Point position, Size size)
        {
            this.Position = position;
            this.BaseSize = size;
        }

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="x">Camera X position. Position is always in a middle of a camera rendered area</param>
        /// <param name="y">Camera Y position. Position is always in a middle of a camera rendered area</param>
        /// <param name="width">Width of rendered area.</param>
        /// <param name="height">Height of rendered area.</param>
        public Camera(double x, double y, double width, double height)
        {
            this.Position = new Point(x, y);
            this.BaseSize = new Size(width, height);
        }

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="position">Camera position. Position is always in a middle of a camera rendered area.</param>
        /// <param name="size">Size of rendered area.</param>
        /// <param name="layers">Layers, that this camera will render.</param>
        public Camera(Point position, Size size, List<Layer> layers)
        {
            this.Position = position;
            this.BaseSize = size;
            this.RenderLayers = layers;
        }

        /// <summary>
        /// Chech if camera is rendering specific layer.
        /// </summary>
        /// <param name="layer">Layer to check.</param>
        /// <returns>True if camera renders this layer.</returns>
        public bool IsRendering(Layer layer)
        {
            foreach (Layer l1 in RenderLayers)
            {
                if (l1 == layer) return true;
            }
            return false;
        }
    }
}
