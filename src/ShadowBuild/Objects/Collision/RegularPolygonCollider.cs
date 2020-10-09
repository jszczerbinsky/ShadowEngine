using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShadowBuild.Objects.Collision
{
    public class RegularPolygonCollider : PolygonCollider
    {
        public RegularPolygonCollider(uint edges, double x, double y, double size) : base()
        {
            float angle = (float)Math.PI* 2 / edges;
            Point[] vertices = new Point[edges];

            for(int i = 0; i < edges; i++)
            {
                vertices[i] = new Point(size * Math.Sin(i * angle), size * Math.Cos(i * angle));
            }
            
            this.Vertices = vertices;
        }
    }
}
