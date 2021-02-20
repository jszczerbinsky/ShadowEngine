using ShadowEngine.Exceptions;
using System;
using System.Windows;

namespace ShadowEngine.Objects.Collision
{
    /// <summary>
    /// Regular polygon collider constructor.
    /// With this class you can create simple regular polygon colliders.
    /// </summary>
    public class RegularPolygonCollider : PolygonCollider
    {

        /// <summary>
        /// Regular polygon collider constructor.
        /// </summary>
        /// <param name="edges">regular polygon edges count</param>
        /// <param name="x">x position relative to parent object</param>
        /// <param name="y">y position relative to parent object</param>
        /// <param name="width">collider width</param>
        /// <param name="height">collider height</param>
        public RegularPolygonCollider(uint edges, double x, double y, double size) : base()
        {
            if (edges < 3) throw new ColliderException("Regular polygon cannot have less than 3 edges!");
            if (size <= 0) throw new ColliderException("Size has to be a positive number");

            float angle = (float)Math.PI * 2 / edges;
            Point[] vertices = new Point[edges];

            for (int i = 0; i < edges; i++)
            {
                vertices[i] = new Point(size * Math.Sin(i * angle), size * Math.Cos(i * angle));
            }

            this.Vertices = vertices;
        }
    }
}
