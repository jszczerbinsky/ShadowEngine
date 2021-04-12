using ShadowEngine.Objects.Parameters;
using ShadowEngine.Objects.Texturing;
using ShadowEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowEngine.Objects.UI
{
    public class Label : UIObject
    {
        /// <value>Text on UI object</value>
        public string Content;

        /// <value>size of a text</value>
        public Parameters.Size ContentSize = new Parameters.Size(100, 100);

        /// <value>format of a text</value>
        public StringFormat ContentFormat = new StringFormat();

        public Label(string name, Texture texture, string content) : base(name, texture)
        {
            this.Content = content;
            ContentFormat.LineAlignment = StringAlignment.Center;
            ContentFormat.Alignment = StringAlignment.Center;
        }

        public Label(string name, Texture texture, Layer layer, string content) : base(name, texture, layer)
        {
            this.Content = content;
            ContentFormat.LineAlignment = StringAlignment.Center;
            ContentFormat.Alignment = StringAlignment.Center;
        }

        private void RenderContent(Graphics g, Vector2D camPos)
        {
            Rectangle rect = new Rectangle(
                    (int)(this.GetStartPosition().X - camPos.X),
                    (int)(this.GetStartPosition().Y - camPos.Y),
                    (int)(this.ContentSize.Width * this.Scale.Width),
                    (int)(this.ContentSize.Height * this.Scale.Height)
                    );
            g.DrawString(this.Content, SystemFonts.MenuFont, new SolidBrush(Color.Black), rect, ContentFormat);
        }

        public override void Render(Graphics g, Vector2D startPosition)
        {
            base.Render(g, startPosition);

            Vector2D camPosTmp = new Vector2D(0, 0);

            if (this.PositionType == UIPositionType.Global)
                camPosTmp = startPosition;
            this.RenderContent(g, camPosTmp);
        }

    }
}
