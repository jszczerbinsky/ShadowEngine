using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects.UI
{
    public class UIObject : RenderableObject
    {
        public UIObject(string name, Texture texture) : base(name, texture)
        {
        }

        public UIObject(string name, Texture texture, Layer layer) : base(name, texture, layer)
        {
        }
    }
}
