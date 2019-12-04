using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowBuild.Objects.Dimensions;

namespace ShadowBuild.Objects.Texturing
{
    public class GridTexture : EmptyTexture
    {
        public int xCount;
        public int yCount;

        public GridTexture(Bitmap image, int xCount, int yCount)
        {
            this.image = image;
            this.xCount = xCount;
            this.yCount = yCount;
        }

        public override _2Dsize getSize()
        {
            return new _2Dsize(image.Size.Width * xCount, image.Size.Height * yCount);
        }

        public override void render(Graphics g, GameObject obj)
        {
            GridTexture tex = (GridTexture)obj.actualTexture;

            for (int x = 0; x < tex.xCount; x++)
            {
                for (int y = 0; y < tex.yCount; y++)
                {

                    g.DrawImage(
                        tex.image,
                        new Rectangle(
                            new Point(
                                (int)(obj.startPosition.X + x * tex.image.Width * obj.size.X),
                                (int)(obj.startPosition.Y + y * tex.image.Height * obj.size.Y)
                            ), new Size(
                                (int)(tex.image.Width * obj.size.X),
                                (int)(tex.image.Height * obj.size.Y)
                                )
                            )
                        );
                }
            }
        }
    }
}
