using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowBuild.Objects.Dimensions;

namespace ShadowBuild.Objects.Texturing
{
    public class RegularTexture : Texture
    {

        public RegularTexture(Bitmap image)
        {
            this.image = image;
        }

        public override _2Dsize getSize()
        {
            return new _2Dsize(this.image.Width, this.image.Height);
        }

        public override void render(Graphics g, GameObject obj)
        {

            RegularTexture tex = (RegularTexture)obj.actualTexture;

            g.DrawImage(
                tex.image,
                new Rectangle(
                    new Point(
                        (int)(obj.startPosition.X),
                        (int)(obj.startPosition.Y)
                    ), new Size(
                        (int)(tex.image.Width * obj.size.X),
                        (int)(tex.image.Height * obj.size.Y)
                        )
                    )
                );
        }
    }
}
