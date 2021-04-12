using ShadowEngine.Input.Mouse;
using ShadowEngine.Objects.Parameters;
using ShadowEngine.Objects.Texturing;
using ShadowEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowEngine.Objects.UI.Input
{
    public class UISlider : UIObject
    {
        public Color SliderColor = Color.Black;
        public Texture PointerTexture = new ColorTexture(null, Color.LightGray, ColorTextureShape.Ellipse);

        private UIObject pointer;
        private bool moving = false;

        public float Value;
        public float MinValue = 0;
        public float MaxValue = 100;

        public UISlider(string name, Texture texture) : base(name, texture)
        {
            this.pointer = new UIObject(null, PointerTexture);
            this.pointer.SetSize(30, 30);
            pointer.SetParent(this);
            Loop.OnTick += this.OnTick;
        }

        public UISlider(string name, Texture texture, Layer layer) : base(name, texture, layer)
        {
            this.pointer = new UIObject(null, PointerTexture);
            this.pointer.SetSize(30, 30);
            pointer.SetParent(this);
            Loop.OnTick += this.OnTick;
        }

        public void OnTick()
        {
            if (Mouse.ButtonPressed(System.Windows.Forms.MouseButtons.Left))
            {
                if (pointer.MouseOver) moving = true;
            }
            else moving = false;

            if (moving)
            {
                if (this.PositionType == UIPositionType.Global)
                {
                    Vector2D pos = new Vector2D(Mouse.GamePosition.X - this.GetGlobalPosition().X, pointer.Position.Y);
                    if (pos.X < -this.GetRealSize().Width / 2) pos.X = -this.GetRealSize().Width / 2;
                    if (pos.X > this.GetRealSize().Width / 2) pos.X = this.GetRealSize().Width / 2;
                    pointer.SetPosition(pos);
                    float tmpValue = (pos.X + this.GetRealSize().Width / 2)/this.GetRealSize().Width;
                    float valuesOffset = this.MaxValue - this.MinValue;
                    this.Value = this.MinValue + tmpValue * valuesOffset;
                }
                else
                {
                    Vector2D pos = new Vector2D(Mouse.Position.X - this.GetGlobalPosition().X, pointer.Position.Y);
                    if (pos.X < -this.GetRealSize().Width / 2) pos.X = -this.GetRealSize().Width / 2;
                    if (pos.X > this.GetRealSize().Width / 2) pos.X = this.GetRealSize().Width / 2;
                    pointer.SetPosition(pos);
                    float tmpValue = (pos.X + this.GetRealSize().Width / 2) / this.GetRealSize().Width;
                    float valuesOffset = this.MaxValue - this.MinValue;
                    this.Value = this.MinValue + tmpValue * valuesOffset;
                }
            }
                Log.Message(this.Value.ToString());
        }

        public override void Render(Graphics g, Vector2D startPosition)
        {
            base.Render(g, startPosition);

            Vector2D camPosTmp = new Vector2D(0, 0);

            if (this.PositionType == UIPositionType.Global)
                camPosTmp = startPosition;

            Point startPoint = new Point(
                     (int)(this.GetStartPosition().X - camPosTmp.X - this.GetRealSize().Width/2),
                     (int)(this.GetStartPosition().Y - camPosTmp.Y - this.GetRealSize().Height / 2)
                );

            Point endPoint = new Point(
                (int)(startPoint.X + this.GetRealSize().Width),
                (int)(startPoint.Y)
                );

            g.DrawLine(new Pen(SliderColor, this.GetRealSize().Height), startPoint, endPoint);
        }
    }
}
