using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace ShadowEngine.Objects.Texturing
{
    /// <summary>
    /// Color texture class.
    /// With this class you can create textures with single color.
    /// </summary>
    public class ColorTexture : Texture
    {
        /// <value>Texture color</value>
        [ScriptIgnore]
        public Color Color;

        /// <value>Gets texture color in hex value</value>
        public string HexColor
        {
            get { return "#" + Color.R.ToString("X2") + Color.G.ToString("X2") + Color.B.ToString("X2") + Color.A.ToString("X2"); }
        }

        /// <value>Texture shape</value>
        [ScriptIgnore]
        public ColorTextureShape Shape;

        /// <value>Texture shape as string</value>
        public string ShapeString
        {
            get { return Shape.ToString(); }
            set { Shape = (ColorTextureShape)Enum.Parse(typeof(ColorTextureShape), value); }
        }

        /// <summary>
        /// Constructor of color texture.
        /// </summary>
        /// <param name="name">texture name</param>
        /// <param name="color">texture color</param>
        /// <param name="shape">texture shape</param>
        public ColorTexture(string name, Color color, ColorTextureShape shape)
        {
            this.Name = name;
            this.Shape = shape;
            this.Color = color;
        }

        public override void Render(Graphics g, TexturedObject obj, System.Windows.Point cameraPos)
        {
            ColorTexture tex = (ColorTexture)obj.ActualTexture;
            Brush brush = new SolidBrush(tex.Color);

            Rectangle size = new Rectangle(
                new System.Drawing.Point(
                     (int)(obj.GetStartPosition().X - cameraPos.X),
                     (int)(obj.GetStartPosition().Y - cameraPos.Y)
                ),
                new Size(
                    (int)(obj.SizeMultipler.Width * obj.BaseSize.Width),
                    (int)(obj.SizeMultipler.Height * obj.BaseSize.Height)
                )
            );
            if (tex.Shape == ColorTextureShape.Ellipse)
                g.FillEllipse(brush, size);
            else g.FillRectangle(brush, size);

            g.ResetTransform();
        }
    }
}
