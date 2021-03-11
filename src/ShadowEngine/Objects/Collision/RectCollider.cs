using System;
using System.Windows;

namespace ShadowEngine.Objects.Collision
{
    /// <summary>
    /// Rectangle collider class.
    /// With this class you can create simple, rectangular colliders.
    /// </summary>
    [Serializable]
    public sealed class RectCollider : PolygonCollider
    {
        /// <summary>
        /// Rectangle collider constructor.
        /// </summary>
        /// <param name="x">x position relative to parent object</param>
        /// <param name="y">y position relative to parent object</param>
        /// <param name="width">collider width</param>
        /// <param name="height">collider height</param>
        public RectCollider(double x, double y, double width, double height) : base()
        {
            Point[] vertices = new Point[4];

            vertices[0] = new Point(x - width / 2, y + height / 2);
            vertices[1] = new Point(x + width / 2, y + height / 2);
            vertices[2] = new Point(x + width / 2, y - height / 2);
            vertices[3] = new Point(x - width / 2, y - height / 2);

            this.Vertices = vertices;
        }
    }
}
