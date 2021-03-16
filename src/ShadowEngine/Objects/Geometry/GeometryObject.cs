using ShadowEngine.Objects.Parameters;
using ShadowEngine.Rendering;
using System;
using System.Drawing;

namespace ShadowEngine.Objects.Geometry
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
        public Vector2D EndPosition { get; private set; }


        private Vector2D renderPosition;
        private Vector2D renderSize;

        #region constructors

        public GeometryObject(string name, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, ShadowEngine.Objects.Parameters.Size size) : base(name)
        {
            this.Shape = shape;
            this.Position = position;
            this.Size = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
        }
        public GeometryObject(string name, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Vector2D endPosition) : base(name)
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
        public GeometryObject(string name, Layer layer, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Parameters.Size size) : base(name, layer)
        {
            this.Shape = shape;
            this.Position = position;
            this.Size = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
            this.RenderLayer = layer;
        }
        public GeometryObject(string name, Layer layer, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Vector2D endPosition) : base(name, layer)
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
        public GeometryObject(string name, World world, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Parameters.Size size) : base(name, world)
        {
            this.Shape = shape;
            this.Position = position;
            this.Size = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
        }
        public GeometryObject(string name, World world, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Vector2D endPosition) : base(name, world)
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
        public GeometryObject(string name, World world, Layer layer, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Parameters.Size size) : base(name, layer, world)
        {
            this.Shape = shape;
            this.Position = position;
            this.Size = size;
            this.Color = color;
            this.Fill = fill;
            this.BorderThickness = borderThickness;
            this.Name = name;
            this.RenderLayer = layer;

        }
        public GeometryObject(string name, World world, Layer layer, Shape shape, Color color, float borderThickness, bool fill, Vector2D position, Vector2D endPosition) : base(name, layer, world)
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

        #endregion


        /// <summary>
        /// Gets global position of object start
        /// </summary>
        public Vector2D GetStartRenderPosition()
        {
            Vector2D tmpPosition = this.renderPosition;
            if (this.Parent != null)
            {
                tmpPosition = new Vector2D(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
            }
            return tmpPosition;

        }
        private void SetBoundsFrom2Points(Vector2D p1, Vector2D p2)
        {
            float sX = 0;
            float sY = 0;
            float bX = 0;
            float bY = 0;

            if (p1.X > p2.X) { bX = p1.X; sX = p2.X; }
            else { bX = p2.X; sX = p1.X; }
            if (p1.Y > p2.Y) { bY = p1.Y; sY = p2.Y; }
            else { bY = p2.Y; sY = p1.Y; }

            this.renderPosition = new Vector2D(sX, sY);
            this.renderSize = new Vector2D(bX - sX, bY - sY);
        }

        /// <summary>
        /// Sets end position of an object
        /// </summary>
        public void SetEndPosition(Vector2D endPosition)
        {
            this.EndPosition = endPosition;
            SetBoundsFrom2Points(this.Position, endPosition);
        }

        public override bool CheckPointInside(Vector2D p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets start position of an object
        /// </summary>
        public Vector2D GetStartPosition()
        {
            return this.GetNonRotatedGlobalPosition();
        }

        /// <summary>
        /// Gets global end position of an object
        /// </summary>
        public Vector2D GetGlobalEndPosition()
        {
            Vector2D tmpPosition = this.EndPosition;
            if (this.Parent != null)
            {
                tmpPosition = new Vector2D(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
            }
            return tmpPosition;
        }
        public override void Render(Graphics g, Vector2D startPosition)
        {
            System.Drawing.Point pos = new System.Drawing.Point(
                (int)(this.GetStartRenderPosition().X - startPosition.X),
                (int)(this.GetStartRenderPosition().Y - startPosition.Y)
                );
            System.Drawing.Size size = new System.Drawing.Size(
                (int)renderSize.X,
                (int)renderSize.Y);

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
