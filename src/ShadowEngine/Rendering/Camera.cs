using ShadowEngine.Objects.Parameters;
using ShadowEngine.Objects;
using System.Collections.Generic;

namespace ShadowEngine.Rendering
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
        public Vector2D StartPosition
        {
            get
            {
                return new Vector2D(this.Position.X - this.Size.Width / 2, this.Position.Y - this.Size.Height / 2);
            }
        }

        /// <value>Gets end point of rendered area of camera</value>
        public Vector2D EndPosition
        {
            get
            {
                return new Vector2D(this.Position.X + this.Size.Width / 2, this.Position.Y + this.Size.Height / 2);
            }
        }

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="position">Camera position. Position is always in a middle of a camera rendered area.</param>
        /// <param name="size">Size of rendered area.</param>
        public Camera(Vector2D position, Size size)
        {
            this.Position = position;
            this.Size = size;
        }

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="x">Camera X position. Position is always in a middle of a camera rendered area</param>
        /// <param name="y">Camera Y position. Position is always in a middle of a camera rendered area</param>
        /// <param name="width">Width of rendered area.</param>
        /// <param name="height">Height of rendered area.</param>
        public Camera(float x, float y, float width, float height)
        {
            this.Position = new Vector2D(x, y);
            this.Size = new Size(width, height);
        }

        /// <summary>
        /// Camera constructor.
        /// </summary>
        /// <param name="position">Camera position. Position is always in a middle of a camera rendered area.</param>
        /// <param name="size">Size of rendered area.</param>
        /// <param name="layers">Layers, that this camera will render.</param>
        public Camera(Vector2D position, Size size, List<Layer> layers)
        {
            this.Position = position;
            this.Size = size;
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
