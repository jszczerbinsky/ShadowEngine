using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Texturing.Image
{
    public abstract class ImageTexture : Texture
    {
        [JsonIgnore]
        public Bitmap Image;
        public string ImagePath;

        public void InitializeImage()
        {
            this.Image = new Bitmap(ImagePath);
        }
    }
}
