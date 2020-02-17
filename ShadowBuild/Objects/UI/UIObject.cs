using System.Drawing;
using System.Windows;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects.UI
{
    public class UIObject : TexturedObject
    {
        public UIObject(string name, Texture texture) : base(name, texture)
        {
        }

        public UIObject(string name, Texture texture, Layer layer) : base(name, texture, layer)
        {
        }

        public override void Render(Graphics g, System.Windows.Point startPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
