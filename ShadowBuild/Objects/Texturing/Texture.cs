using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class Texture
    {
        public Bitmap image;

        public abstract _2Dsize getSize();
        public abstract void render(Graphics g, GameObject obj);
        public static void renderObjectCenters(Graphics g, GameObject obj)
        {
            if (obj.isRendered)
            {
                g.FillEllipse(
                    new SolidBrush(
                            Color.Red
                    ),
                    new Rectangle(
                        new Point(
                            (int)(obj.globalPosition.X - 2),
                            (int)(obj.globalPosition.Y - 2)
                        ),
                        new Size(
                            5, 5)
                        ));
            }
        }
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
                        (int)(obj.actualTexture.getSize().X * obj.size.X), (int)(obj.actualTexture.getSize().Y * obj.size.Y))
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
                        (int)(obj.actualTexture.getSize().X * obj.size.X), (int)(obj.actualTexture.getSize().Y * obj.size.Y))
                    ));
        }
    }
}
