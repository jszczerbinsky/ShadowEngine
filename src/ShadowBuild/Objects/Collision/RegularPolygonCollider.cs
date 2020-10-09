using ShadowBuild.Exceptions;
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
            if (edges < 3) throw new ColliderException("Regular polygon cannot have less than 3 edges!");
            if (size <= 0) throw new ColliderException("Size has to be a positive number");

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
