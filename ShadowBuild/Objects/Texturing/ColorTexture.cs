using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : Texture
    {
        public Color color;
        public _2Dsize size;
        public Shape shape;

        public ColorTexture(Color color, Shape shape, _2Dsize size)
        {
            this.shape = shape;
            this.color = color;
            this.size = size;
        }

        public override _2Dsize getSize()
        {
            return size; 
        }

        public override void render(Graphics g, GameObject obj)
        {
            ColorTexture tex = (ColorTexture)obj.actualTexture;
            Brush brush = new SolidBrush(tex.color);
            Rectangle size = new Rectangle(
                new Point(
                     (int)(obj.startPosition.X),
                     (int)(obj.startPosition.Y)
                ),
                new Size(
                    (int)(tex.size.X * obj.size.X),
                    (int)(tex.size.Y * obj.size.Y)
                )
            );

            if (tex.shape == Shape.ELLIPSE)
                g.FillEllipse(brush, size);
            else g.FillRectangle(brush, size);
        }
    }
}
