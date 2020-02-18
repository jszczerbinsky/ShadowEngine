using System.Drawing;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects.UI
{
    public class UIObject : TexturedObject
    {

        public string Content;
        public System.Windows.Size ContentSize = new System.Windows.Size(100,100);

        public UIObject(string name, Texture texture, string content) : base(name, texture)
        {
            this.Content = content;
        }
        public UIObject(string name, Texture texture, Layer layer, string content) : base(name, texture, layer)
        {
            this.Content = content;
        }

        private void RenderContent(Graphics g, System.Windows.Point camPos)
        {
            Rectangle rect = new Rectangle(
                    (int)(this.GetGlobalPosition().X - camPos.X),
                    (int)(this.GetGlobalPosition().Y - camPos.Y),
                    (int)this.ContentSize.Width,
                    (int)this.ContentSize.Height
                    );
            g.DrawString(this.Content, SystemFonts.MenuFont, new SolidBrush(Color.Black),rect);
        }
        public override void Render(Graphics g, System.Windows.Point camPos)
        {
            if (this.ActualAnimation != null)
                this.ActualTexture.Render(g, this, camPos);
            this.RenderContent(g, camPos);
        }
    }
}
