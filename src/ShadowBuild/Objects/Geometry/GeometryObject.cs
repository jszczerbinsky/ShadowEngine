using ShadowBuild.Rendering;
using System;
using System.Drawing;
using System.Windows;

namespace ShadowBuild.Objects.Geometry
{
    public class GeometryObject : RenderableObject
    {
        public bool Fill = true;
        public Shape Shape;
        public Color Color = Color.Black;
        public float BorderThickness = 1;

        public GeometryObject(string name, Shape shape, Color color, float borderThickness, bool fill, System.Windows.Point position, System.Windows.Size size) : base(name)
        {
            this.Shape = shape;
            this.Position = position;
            this.BaseSize = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
        }
        public GeometryObject(string name, Shape shape, Color color, float borderThickness, bool fill, System.Windows.Point position, System.Windows.Point endPosition) : base(name)
        {
            this.Shape = shape;
            this.Position = position;
            this.BaseSize = new System.Windows.Size(endPosition.X - position.X, endPosition.Y - position.Y);
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;

        }
        public GeometryObject(string name, Shape shape, System.Windows.Point position, System.Windows.Size size) : base(name)
        {
            this.Shape = shape;
            this.Position = position;
            this.BaseSize = size;
            this.Name = name;

        }
        public GeometryObject(string name, Shape shape, System.Windows.Point position, System.Windows.Point endPosition) : base(name)
        {
            this.Shape = shape;
            SetBoundsFrom2Points(position, endPosition);
            this.Name = name;

        }
        public GeometryObject(string name, Layer layer, Shape shape, Color color, float borderThickness, bool fill, System.Windows.Point position, System.Windows.Size size) : base(name, layer)
        {
            this.Shape = shape;
            this.Position = position;
            this.BaseSize = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
            this.RenderLayer = layer;

        }
        public GeometryObject(string name, Layer layer, Shape shape, Color color, float borderThickness, bool fill, System.Windows.Point position, System.Windows.Point endPosition) : base(name, layer)
        {
            this.Shape = shape;
            SetBoundsFrom2Points(position, endPosition);
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
            this.RenderLayer = layer;

        }
        public GeometryObject(string name, Layer layer, Shape shape, System.Windows.Point position, System.Windows.Size size) : base(name, layer)
        {
            this.Shape = shape;
            this.Position = position;
            this.BaseSize = size;
            this.Name = name;
            this.RenderLayer = layer;

        }
        public GeometryObject(string name, Layer layer, Shape shape, System.Windows.Point position, System.Windows.Point endPosition) : base(name, layer)
        {
            this.Shape = shape;
            SetBoundsFrom2Points(position, endPosition);
            this.Name = name;
            this.RenderLayer = layer;
        }
        private void SetBoundsFrom2Points(System.Windows.Point p1, System.Windows.Point p2)
        {
            double sX=0;
            double sY=0;
            double bX=0;
            double bY=0;

            if (p1.X > p2.X) { bX = p1.X; sX = p2.X; }
            else { bX = p2.X; sX = p1.X; }
            if (p1.Y > p2.Y) { bX = p1.Y; sX = p2.Y; }
            else { bX = p2.Y; sX = p1.Y; } 

            this.Position = new System.Windows.Point(sX, sY);
            this.BaseSize = new System.Windows.Size(bX - sX, bY - sY);
        }
        public void SetEndPosition(System.Windows.Point endPosition)
        {
            SetBoundsFrom2Points(this.Position, endPosition);
        }

        public override bool CheckPointInside(System.Windows.Point p)
        {
            throw new NotImplementedException();
        }

        public override System.Windows.Point GetStartPosition()
        {
            return this.GetGlobalPosition();
        }

        public override void Render(Graphics g, System.Windows.Point startPosition)
        {
            System.Drawing.Point pos = new System.Drawing.Point(
                (int)(this.GetStartPosition().X - startPosition.X),
                (int)(this.GetStartPosition().Y - startPosition.Y)
                );
            System.Drawing.Size size = new System.Drawing.Size(
                (int)this.BaseSize.Width,
                (int)this.BaseSize.Height);

            Rectangle rect = new Rectangle(pos, size);

            switch (this.Shape)
            {
                case Shape.Ellipse:
                    if (this.Fill)
                        g.FillEllipse(new SolidBrush(this.Color), rect);
                    else
                        g.DrawEllipse(new Pen(this.Color, this.BorderThickness), rect);
                    break;

                case Shape.Rectangle:
                    if (this.Fill)
                        g.FillRectangle(new SolidBrush(this.Color), rect);
                    else
                        g.DrawRectangle(new Pen(this.Color, this.BorderThickness), rect);
                    break;

                default:
                    g.DrawLine(new Pen(this.Color, this.BorderThickness), pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Height);
                    break;
            }

        }
    }
}
