using ShadowBuild.Objects.Dimensions;
using System;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class Texture
    {
        public Bitmap Image;

        public abstract _2Dsize GetSize();
        public abstract void Render(Graphics g, GameObject obj);
        public static void RenderObjectCenters(Graphics g, GameObject obj)
        {
            if (obj.Visible)
            {
                g.FillEllipse(
                    new SolidBrush(
                            Color.Red
                    ),
                    new Rectangle(
                        new Point(
                            (int)(obj.GetGlobalPosition().X - 2),
                            (int)(obj.GetGlobalPosition().Y - 2)
                        ),
                        new Size(
                            5, 5)
                        ));
            }
        }
        public static void RenderObjectBorders(Graphics g, GameObject obj)
        {
            Random rand = new Random();
            Color fillColor = Color.FromArgb(100, rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

            g.FillRectangle(
                    new SolidBrush(
                            fillColor
                    ),
                new Rectangle(
                    new Point(
                        (int)(obj.GetStartPosition().X),
                        (int)(obj.GetStartPosition().Y)
                    ),
                    new Size(
                        (int)(obj.ActualTexture.GetSize().X * obj.Size.X), (int)(obj.ActualTexture.GetSize().Y * obj.Size.Y))
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
                        (int)(obj.GetStartPosition().X),
                        (int)(obj.GetStartPosition().Y)
                    ),
                    new Size(
                        (int)(obj.ActualTexture.GetSize().X * obj.Size.X), (int)(obj.ActualTexture.GetSize().Y * obj.Size.Y))
                    ));
        }
    }
}
