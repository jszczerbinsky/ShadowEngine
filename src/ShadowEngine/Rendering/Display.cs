using System.Drawing;
using System.Windows.Forms;

namespace ShadowBuild.Rendering
{
    public class Display : PictureBox
    {
        public Graphics DisplayGraphics { get; private set; }

        public void Initialize(Size Resolution)
        {
            if (this.Image != null) ((Bitmap)this.Image).Dispose();
            if (this.DisplayGraphics != null) this.DisplayGraphics.Dispose();
            this.Image = new Bitmap(Resolution.Width, Resolution.Height);
            DisplayGraphics = Graphics.FromImage(this.Image);
        }
    }
}
