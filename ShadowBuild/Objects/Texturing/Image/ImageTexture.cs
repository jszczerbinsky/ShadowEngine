using Newtonsoft.Json;
using System.Drawing;

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
