using ShadowEngine.Exceptions;
using ShadowEngine.Objects.Parameters;
using System;
using System.Windows;

namespace ShadowEngine.Objects.Collision
{
    /// <summary>
    /// Regular polygon collider constructor.
    /// With this class you can create simple regular polygon colliders.
    /// </summary>
    [Serializable]
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
        public RegularPolygonCollider(uint edges, float x, float y, float size) : base()
        {
            if (edges < 3)
            {
                Exception ex = new ColliderException("Regular polygon cannot have less than 3 edges!");
                Log.Exception(ex);
                throw ex;
            }
            if (size <= 0)
            {
                Exception ex = new ColliderException("Size has to be a positive number");
                Log.Exception(ex);
                throw ex;
            }

            float angle = (float)Math.PI * 2 / edges;
            Vector2D[] vertices = new Vector2D[edges];

            for (int i = 0; i < edges; i++)
            {
                vertices[i] = new Vector2D((float)(size * Math.Sin(i * angle))+x, (float)(size * Math.Cos(i * angle))+y);
            }

            this.Vertices = vertices;
        }
    }
}
