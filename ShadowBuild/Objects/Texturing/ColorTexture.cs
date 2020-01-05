using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : Texture
    {
        [ScriptIgnore]
        public Color Color;
        public string HexColor
        {
            get { return "#" + Color.R.ToString("X2") + Color.G.ToString("X2") + Color.B.ToString("X2") + Color.A.ToString("X2"); }
        }
        public System.Windows.Point Size;
        [ScriptIgnore]
        public Shape Shape;
        public string ShapeString
        {
            get { return Shape.ToString(); }
            set { Shape = (Shape)Enum.Parse(typeof(Shape), value); }
        }

        public ColorTexture(string name, Color color, Shape shape, System.Windows.Point size)
        {
            this.Name = name;
            this.Shape = shape;
            this.Color = color;
            this.Size = size;
        }

        public override System.Windows.Point GetSize()
        {
            return Size;
        }

        public override void Render(Graphics g, RenderableObject obj, System.Windows.Point cameraPos)
        {
            ColorTexture tex = (ColorTexture)obj.ActualTexture;
            Brush brush = new SolidBrush(tex.Color);
            Rectangle size = new Rectangle(
                new System.Drawing.Point(
                     (int)(obj.GetStartPosition().X - cameraPos.X),
                     (int)(obj.GetStartPosition().Y - cameraPos.Y)
                ),
                new Size(
                    (int)(tex.Size.X * obj.Size.X),
                    (int)(tex.Size.Y * obj.Size.Y)
                )
            );

            if (tex.Shape == Shape.ELLIPSE)
                g.FillEllipse(brush, size);
            else g.FillRectangle(brush, size);
        }
    }
}
