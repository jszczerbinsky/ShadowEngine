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
        public TextureMode mode;

        public RegularTexture(Bitmap image, TextureMode mode)
        {
            this.image = image;
            this.mode = mode;
        }
    }
}
