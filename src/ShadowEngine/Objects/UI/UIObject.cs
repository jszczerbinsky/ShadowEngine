using ShadowEngine.Objects.Parameters;
using ShadowEngine.Input.Mouse;
using ShadowEngine.Objects.Texturing;
using ShadowEngine.Rendering;
using System.Drawing;

namespace ShadowEngine.Objects.UI
{

    /// <summary>
    /// UI object class.
    /// With this class you can create UI objects.
    /// </summary>
    public class UIObject : TexturedObject
    {
        /// <value>UI object position type</value>
        public UIPositionType PositionType = UIPositionType.RelativeToScreen;

        /// <value>Text on UI object</value>
        public string Content;

        /// <value>size of a text</value>
        public Parameters.Size ContentSize = new Parameters.Size(100, 100);

        /// <value>format of a text</value>
        public StringFormat ContentFormat = new StringFormat();

        public override bool MouseOver
        {
            get
            {
                if (Mouse.LockCursor) return false;
                if (PositionType == UIPositionType.Global) return base.MouseOver;

                Vector2D p = new Vector2D(
                                    Mouse.Position.X,
                                    Mouse.Position.Y
                                );
                return CheckPointInside(p);
            }
        }

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
            Vector2D camPosTmp = new Vector2D(0, 0);

            if (this.PositionType == UIPositionType.Global)
                camPosTmp = startPosition;

            if (this.GetActualTexture() != null)
                this.GetActualTexture().Render(g, this, camPosTmp);
            this.RenderContent(g, camPosTmp);
        }
        public override bool CheckPointInside(Vector2D p)
        {
            if (this.PositionType == UIPositionType.Global)
                return base.CheckPointInside(p);

            float decreseLeft = this.GetRealSize().Width / 2;
            float decreseTop = this.GetRealSize().Height / 2;

            Vector2D start = new Vector2D(this.GetGlobalPosition().X - decreseLeft, this.GetGlobalPosition().Y - decreseTop);
            Vector2D end = start + this.GetRealSize();

            if (
                p.X > start.X &&
                p.X < end.X &&
                p.Y > start.Y &&
                p.Y < end.Y
             )
                return true;
            return false;
        }
    }
}
