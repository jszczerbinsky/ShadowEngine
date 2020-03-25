using System.Drawing;
using System.Web.Script.Serialization;

namespace ShadowBuild.Objects.Texturing.Image
{
    public abstract class ImageTexture : Texture
    {
        [ScriptIgnore]
        public Bitmap Image;
        public string ImagePath;

        public void InitializeImage()
        {
            this.Image = new Bitmap(ImagePath);
        }
    }
}
