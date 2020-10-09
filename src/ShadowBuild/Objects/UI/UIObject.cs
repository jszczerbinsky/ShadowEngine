using ShadowBuild.Input.Mouse;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System.Drawing;

namespace ShadowBuild.Objects.UI
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
        public System.Windows.Size ContentSize = new System.Windows.Size(100, 100);

        /// <value>format of a text</value>
        public StringFormat ContentFormat = new StringFormat();

        public override bool MouseOver
        {
            get
            {
                if (Mouse.LockCursor) return false;
                if (PositionType == UIPositionType.Global) return base.MouseOver;

                System.Windows.Point p = new System.Windows.Point(
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
        public override void Render(Graphics g, System.Windows.Point startPosition)
        {
            System.Windows.Point camPosTmp = new System.Windows.Point(0, 0);

            if (this.PositionType == UIPositionType.Global)
                camPosTmp = startPosition;

            if (this.ActualTexture != null)
                this.ActualTexture.Render(g, this, camPosTmp);
            this.RenderContent(g, camPosTmp);
        }
        public override bool CheckPointInside(System.Windows.Point p)
        {
            if (this.PositionType == UIPositionType.Global)
                return base.CheckPointInside(p);

            double decreseLeft = this.GetRealSize().Width / 2;
            double decreseTop = this.GetRealSize().Height / 2;

            System.Windows.Point start = new System.Windows.Point(this.Position.X - decreseLeft, this.Position.Y - decreseTop);
            System.Windows.Point end = new System.Windows.Point(start.X + this.GetRealSize().Width, start.Y + this.GetRealSize().Height);

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
