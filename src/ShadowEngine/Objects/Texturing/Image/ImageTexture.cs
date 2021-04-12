using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Script.Serialization;

namespace ShadowEngine.Objects.Texturing.Image
{
    /// <summary>
    /// Class for all image textures.
    /// </summary>
    public abstract class ImageTexture : Texture
    {
        [ScriptIgnore]
        public Bitmap Image;
        public string ImagePath;
        public InterpolationMode InterpolationMode = InterpolationMode.Default;

        public void InitializeImage()
        {
            this.Image = new Bitmap(ImagePath);
        }

        public void SetBitmap(Bitmap bmp)
        {
            this.Image = bmp;
            this.ImagePath = null;
        }
    }
}
