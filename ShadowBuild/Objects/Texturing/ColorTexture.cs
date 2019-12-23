using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : Texture
    {
        public Color color;
        public _2Dsize size;
        public Shape shape;

        public ColorTexture(string name, Color color, Shape shape, _2Dsize size)
        {
            this.Name = name;
            this.shape = shape;
            this.color = color;
            this.size = size;
        }

        public override _2Dsize GetSize()
        {
            return size;
        }

        public override void Render(Graphics g, GameObject obj, _2Dsize cameraPos)
        {
            ColorTexture tex = (ColorTexture)obj.ActualTexture;
            Brush brush = new SolidBrush(tex.color);
            Rectangle size = new Rectangle(
                new Point(
                     (int)(obj.GetStartPosition().X - cameraPos.X),
                     (int)(obj.GetStartPosition().Y - cameraPos.Y)
                ),
                new Size(
                    (int)(tex.size.X * obj.Size.X),
                    (int)(tex.size.Y * obj.Size.Y)
                )
            );

            if (tex.shape == Shape.ELLIPSE)
                g.FillEllipse(brush, size);
            else g.FillRectangle(brush, size);
        }
    }
}
