using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Collision
{
    public sealed class RectCollider : PolygonCollider
    {
        public RectCollider(double x, double y, double width, double height) : base()
        {
            Point[] vertices = new Point[4];

            vertices[0] = new Point(x - width / 2, y + height / 2);
            vertices[1] = new Point(x + width / 2, y+ height / 2);
            vertices[2] = new Point(x + width / 2, y - height / 2);
            vertices[3] = new Point(x - width / 2, y - height / 2);

            this.Vertices = vertices;
        }
    }
}
