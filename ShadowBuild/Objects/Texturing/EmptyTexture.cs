using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class EmptyTexture
    {
        public Bitmap image;

        public static _2Dsize getSize(EmptyTexture tex)
        {
            if (tex is RegularTexture) return new _2Dsize(tex.image.Width, tex.image.Height);
            else return null;
        }
        public abstract void render(Graphics g, GameObject obj);
        public static void renderObjectBorders(Graphics g, GameObject obj)
        {
            Random rand = new Random();
            Color fillColor = Color.FromArgb(100, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            g.FillRectangle(
                    new SolidBrush(
                            fillColor
                    ),
                new Rectangle(
                    new Point(
                        (int)(obj.startPosition.X),
                        (int)(obj.startPosition.Y)
                    ),
                    new Size(
                        (int)(EmptyTexture.getSize(obj.actualTexture).X * obj.size.X), (int)(EmptyTexture.getSize(obj.actualTexture).Y * obj.size.Y))
                    ));

            Color drawColor = Color.FromArgb(100, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            g.DrawRectangle(
                new Pen(
                    new SolidBrush(
                            drawColor
                    ),
                    3),
                new Rectangle(
                    new Point(
                        (int)(obj.startPosition.X),
                        (int)(obj.startPosition.Y)
                    ),
                    new Size(
                        (int)(EmptyTexture.getSize(obj.actualTexture).X * obj.size.X), (int)(EmptyTexture.getSize(obj.actualTexture).Y * obj.size.Y))
                    ));
        }
    }
}
