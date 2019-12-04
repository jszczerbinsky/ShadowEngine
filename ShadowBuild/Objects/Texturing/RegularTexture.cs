using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Texturing
{
    public class RegularTexture : EmptyTexture
    {

        public RegularTexture(Bitmap image)
        {
            this.image = image;
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
