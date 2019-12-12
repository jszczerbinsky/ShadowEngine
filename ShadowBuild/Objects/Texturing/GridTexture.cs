using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class GridTexture : Texture
    {
        public int xCount;
        public int yCount;

        public GridTexture(Bitmap image, int xCount, int yCount)
        {
            this.Image = image;
            this.xCount = xCount;
            this.yCount = yCount;
        }

        public override _2Dsize GetSize()
        {
            return new _2Dsize(Image.Size.Width * xCount, Image.Size.Height * yCount);
        }

        public override void Render(Graphics g, GameObject obj)
        {
            GridTexture tex = (GridTexture)obj.ActualTexture;

            for (int x = 0; x < tex.xCount; x++)
            {
                for (int y = 0; y < tex.yCount; y++)
                {

                    g.DrawImage(
                        tex.Image,
                        new Rectangle(
                            new Point(
                                (int)(obj.GetStartPosition().X + x * tex.Image.Width * obj.Size.X),
                                (int)(obj.GetStartPosition().Y + y * tex.Image.Height * obj.Size.Y)
                            ), new Size(
                                (int)(tex.Image.Width * obj.Size.X),
                                (int)(tex.Image.Height * obj.Size.Y)
                                )
                            )
                        );
                }
            }
        }
    }
}
