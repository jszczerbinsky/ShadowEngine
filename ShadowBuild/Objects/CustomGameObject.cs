using ShadowBuild.Interfaces;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;

namespace ShadowBuild.Objects
{
    public abstract class CustomGameObject : GameObject, ILoopCall
    {
        public CustomGameObject(string name, Texture texture) : base(name, texture)
        {
        }

        public CustomGameObject(string name, Texture texture, Layer layer) : base(name, texture, layer)
        {
        }

        public abstract void OnStart();

        public abstract void OnTick();
    }
}
