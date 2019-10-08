using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Texturing
{
    public class ColorTexture : EmptyTexture
    {
        public Color color { get; private set; }

        public void setColor(Color color)
        {
            this.color = color;
        }
    }
}
