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
        public System.Windows.Point EndPosition { get; private set; }


        private System.Windows.Point renderPosition;
        private System.Windows.Size renderSize;

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
            this.EndPosition = endPosition;
            SetBoundsFrom2Points(position, endPosition);
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
            this.EndPosition = endPosition;
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
            this.EndPosition = endPosition;
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
            this.EndPosition = endPosition;
            this.Name = name;
            this.RenderLayer = layer;
        }
        public System.Windows.Point GetStartRenderPosition()
        {
            System.Windows.Point tmpPosition = this.renderPosition;
            if (this.Parent != null)
            {
                tmpPosition = new System.Windows.Point(tmpPosition.X + this.Parent.GetGlobalPosition().X, tmpPosition.Y + this.Parent.GetGlobalPosition().Y);
            }
            return tmpPosition;

        }
        private void SetBoundsFrom2Points(System.Windows.Point p1, System.Windows.Point p2)
        {
            double sX=0;
            double sY=0;
            double bX=0;
            double bY=0;

            if (p1.X > p2.X) { bX = p1.X; sX = p2.X; }
            else { bX = p2.X; sX = p1.X; }
            if (p1.Y > p2.Y) { bY = p1.Y; sY = p2.Y; }
            else { bY = p2.Y; sY= p1.Y; } 

            this.renderPosition = new System.Windows.Point(sX, sY);
            this.renderSize = new System.Windows.Size(bX - sX, bY - sY);
        }
        public void SetEndPosition(System.Windows.Point endPosition)
        {
            this.EndPosition = endPosition;
            SetBoundsFrom2Points(this.Position, endPosition);
        }

        public override bool CheckPointInside(System.Windows.Point p)
        {
            throw new NotImplementedException();
        }

        public System.Windows.Point GetStartPosition()
        {
            return this.GetGlobalPosition();
        }
        public System.Windows.Point GetGlobalEndPosition()
        {
            System.Windows.Point tmpPosition = this.EndPosition;
            if (this.Parent != null)
            {
                tmpPosition = new System.Windows.Point(tmpPosition.X + this.Parent.GetGlobalPosition().X, tmpPosition.Y + this.Parent.GetGlobalPosition().Y);
            }
            return tmpPosition;
        }
        public override void Render(Graphics g, System.Windows.Point startPosition)
        {
            System.Drawing.Point pos = new System.Drawing.Point(
                (int)(this.GetStartRenderPosition().X - startPosition.X),
                (int)(this.GetStartRenderPosition().Y - startPosition.Y)
                );
            System.Drawing.Size size = new System.Drawing.Size(
                (int)renderSize.Width,
                (int)renderSize.Height);

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
                    int x1 = (int)(this.GetStartPosition().X - startPosition.X);
                    int y1 = (int)(this.GetStartPosition().Y - startPosition.Y);
                    int x2 = (int)(this.GetGlobalEndPosition().X - startPosition.X);
                    int y2 = (int)(this.GetGlobalEndPosition().Y - startPosition.Y);
                    g.DrawLine(new Pen(this.Color, this.BorderThickness), x1, y1, x2, y2);
                    break;
            }

        }
    }
}
