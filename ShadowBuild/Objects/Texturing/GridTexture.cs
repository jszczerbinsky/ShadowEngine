using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
