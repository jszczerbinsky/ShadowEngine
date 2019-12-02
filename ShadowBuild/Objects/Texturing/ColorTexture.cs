using ShadowBuild.Objects.Dimensions;
using System.Drawing;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : EmptyTexture
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
    }
}
