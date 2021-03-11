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
        public Point[] Vertices { get; protected set; }

        protected PolygonCollider()
        {
        }

        /// <summary>
        /// Polygon collider constructor.
        /// </summary>
        /// <param name="vertices">Vertices locations relative to parent</param>
        public PolygonCollider(Point[] vertices)
        {
            this.Vertices = vertices;
        }

        /// <summary>
        /// Gets vertices locations.
        /// </summary>
        public Point[] GetGlobalPoints(GameObject parent)
        {
            Point[] globalPoints = new Point[this.Vertices.Length];
            int i = 0;
            foreach (Point p in this.Vertices)
            {
                Point newP = new Point(p.X + parent.GetNonRotatedGlobalPosition().X, p.Y + parent.GetNonRotatedGlobalPosition().Y);
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
                    Point p1 = polygon.GetGlobalPoints(parent)[i1];
                    Point p2 = polygon.GetGlobalPoints(parent)[i2];

                    Point normal = new Point(p2.Y - p1.Y, p1.X - p2.X);

                    double minA = Double.PositiveInfinity;
                    double maxA = Double.NegativeInfinity;

                    foreach (Point p in col1.GetGlobalPoints(parent1))
                    {
                        double projected = normal.X * p.X + normal.Y * p.Y;

                        if (projected < minA)
                            minA = projected;
                        if (projected > maxA)
                            maxA = projected;
                    }

                    double minB = Double.PositiveInfinity;
                    double maxB = Double.NegativeInfinity;

                    foreach (Point p in col2.GetGlobalPoints(parent2))
                    {
                        double projected = normal.X * p.X + normal.Y * p.Y;

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
