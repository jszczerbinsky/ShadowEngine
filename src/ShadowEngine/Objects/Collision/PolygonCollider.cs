using ShadowEngine.Objects.Parameters;
using System;
using System.Windows;

namespace ShadowEngine.Objects.Collision
{
    /// <summary>
    /// Polygon collider class.
    /// With this class you can make custom colliders to your game objects
    /// </summary>
    [Serializable]
    public abstract class PolygonCollider
    {
        /// <value>Gets vertices locations relative to parent object</value>
        public Vector2D[] Vertices { get; protected set; }

        protected PolygonCollider()
        {
        }

        /// <summary>
        /// Polygon collider constructor.
        /// </summary>
        /// <param name="vertices">Vertices locations relative to parent</param>
        public PolygonCollider(Vector2D[] vertices)
        {
            this.Vertices = vertices;
        }

        /// <summary>
        /// Gets vertices locations.
        /// </summary>
        public Vector2D[] GetGlobalPoints(GameObject parent)
        {
            Vector2D[] globalPoints = new Vector2D[this.Vertices.Length];
            int i = 0;
            foreach (Vector2D p in this.Vertices)
            {
                Vector2D newP = new Vector2D(p.X + parent.GetNonRotatedGlobalPosition().X, p.Y + parent.GetNonRotatedGlobalPosition().Y);
                parent.InheritPosition(ref newP);
                globalPoints[i] = newP;
                i++;
            }
            return globalPoints;
        }

        public static bool CheckCollision(GameObject parent1, PolygonCollider col1, GameObject parent2, PolygonCollider col2)
        {
            PolygonCollider[] cols = { col1, col2 };
            GameObject[] objects = { parent1, parent2 };

            for (int x = 0; x < 2; x++)
            {
                PolygonCollider polygon = cols[x];
                GameObject parent = objects[x];

                for (int i1 = 0; i1 < polygon.GetGlobalPoints(parent).Length; i1++)
                {
                    int i2 = (i1 + 1) % polygon.GetGlobalPoints(parent).Length;
                    Vector2D p1 = polygon.GetGlobalPoints(parent)[i1];
                    Vector2D p2 = polygon.GetGlobalPoints(parent)[i2];

                    Vector2D normal = new Vector2D(p2.Y - p1.Y, p1.X - p2.X);

                    float minA = float.PositiveInfinity;
                    float maxA = float.NegativeInfinity;

                    foreach (Vector2D p in col1.GetGlobalPoints(parent1))
                    {
                        float projected = normal.X * p.X + normal.Y * p.Y;

                        if (projected < minA)
                            minA = projected;
                        if (projected > maxA)
                            maxA = projected;
                    }

                    float minB = float.PositiveInfinity;
                    float maxB = float.NegativeInfinity;

                    foreach (Vector2D p in col2.GetGlobalPoints(parent2))
                    {
                        float projected = normal.X * p.X + normal.Y * p.Y;

                        if (projected < minB)
                            minB = projected;
                        if (projected > maxB)
                            maxB = projected;
                    }

                    if (maxA < minB || maxB < minA)
                        return false;
                }
            }

            return true;
        }
    }
}
