using ShadowBuild.Rendering;
using System;
using System.Drawing;

namespace ShadowBuild.Objects.Geometry
{
    /// <summary>
    /// Geometry objects class.
    /// With this class you can create simple, non-collidable geometry objects
    /// </summary>
    public class GeometryObject : RenderableObject
    {
        /// <value>
        /// if true - object will be filled with color
        /// if false - there will be only borders rendered
        /// </value>
        public bool Fill = true;

        /// <value>geometry shape</value>
        public Shape Shape;

        /// <value>geometry object color</value>
        public Color Color = Color.Black;

        /// <value>thickness of geometry object's border</value>
        public float BorderThickness = 1;

        /// <value>Gets end position of an object</value>
        public System.Windows.Point EndPosition { get; private set; }


        private System.Windows.Point renderPosition;
        private System.Windows.Size renderSize;

        #region constructors

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

        #endregion


        /// <summary>
        /// Gets global position of object start
        /// </summary>
        public System.Windows.Point GetStartRenderPosition()
        {
            System.Windows.Point tmpPosition = this.renderPosition;
            if (this.Parent != null)
            {
                tmpPosition = new System.Windows.Point(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
            }
            return tmpPosition;

        }
        private void SetBoundsFrom2Points(System.Windows.Point p1, System.Windows.Point p2)
        {
            double sX = 0;
            double sY = 0;
            double bX = 0;
            double bY = 0;

            if (p1.X > p2.X) { bX = p1.X; sX = p2.X; }
            else { bX = p2.X; sX = p1.X; }
            if (p1.Y > p2.Y) { bY = p1.Y; sY = p2.Y; }
            else { bY = p2.Y; sY = p1.Y; }

            this.renderPosition = new System.Windows.Point(sX, sY);
            this.renderSize = new System.Windows.Size(bX - sX, bY - sY);
        }

        /// <summary>
        /// Sets end position of an object
        /// </summary>
        public void SetEndPosition(System.Windows.Point endPosition)
        {
            this.EndPosition = endPosition;
            SetBoundsFrom2Points(this.Position, endPosition);
        }

        public override bool CheckPointInside(System.Windows.Point p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets start position of an object
        /// </summary>
        public System.Windows.Point GetStartPosition()
        {
            return this.GetNonRotatedGlobalPosition();
        }

        /// <summary>
        /// Gets global end position of an object
        /// </summary>
        public System.Windows.Point GetGlobalEndPosition()
        {
            System.Windows.Point tmpPosition = this.EndPosition;
            if (this.Parent != null)
            {
                tmpPosition = new System.Windows.Point(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
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
