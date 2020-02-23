using System.Drawing;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects.UI
{
    public class UIObject : TexturedObject
    {

        public string Content;
        public System.Windows.Size ContentSize = new System.Windows.Size(100, 100);
        public StringFormat ContentFormat = new StringFormat();

        public UIObject(string name, Texture texture, string content) : base(name, texture)
        {
            this.Content = content;
            ContentFormat.LineAlignment = StringAlignment.Center;
            ContentFormat.Alignment = StringAlignment.Center;
        }
        public UIObject(string name, Texture texture, Layer layer, string content) : base(name, texture, layer)
        {
            this.Content = content;
            ContentFormat.LineAlignment = StringAlignment.Center;
            ContentFormat.Alignment = StringAlignment.Center;
        }

        private void RenderContent(Graphics g, System.Windows.Point camPos)
        {
            Rectangle rect = new Rectangle(
                    (int)(this.GetStartPosition().X - camPos.X),
                    (int)(this.GetStartPosition().Y - camPos.Y),
                    (int)(this.ContentSize.Width * this.SizeMultipler.Width),
                    (int)(this.ContentSize.Height * this.SizeMultipler.Height)
                    );
            g.DrawString(this.Content, SystemFonts.MenuFont, new SolidBrush(Color.Black), rect, ContentFormat);
        }
        public override void Render(Graphics g, System.Windows.Point camPos)
        {
            if (this.ActualTexture != null)
                this.ActualTexture.Render(g, this, camPos);
            this.RenderContent(g, camPos);
        }
    }
}
