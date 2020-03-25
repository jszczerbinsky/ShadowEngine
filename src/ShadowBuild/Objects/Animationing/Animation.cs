using ShadowBuild.Objects.Texturing;
using System.Collections.Generic;

namespace ShadowBuild.Objects.Animationing
{
    public class Animation
    {
        private static List<Animation> All = new List<Animation>();

        public readonly string Name;
        public readonly List<Texture> Textures;
        public Texture ActualTexture
        {
            get
            {
                return Textures[ActualTextureID];
            }
            private set { }
        }
        public double Speed = 1;

        private int ActualTextureID = 0;
        private double ActualOffset = 0;

        public Animation(string name, List<Texture> textures)
        {
            this.Name = name;
            this.Textures = textures;
        }
        public static void OnTick()
        {
            foreach (Animation anim in All)
            {
                anim.ActualOffset += Loop.delay;
                if (anim.ActualOffset > 1.0 / anim.Speed)
                {
                    anim.ActualTextureID++;
                    if (anim.ActualTextureID >= anim.Textures.Count)
                        anim.ActualTextureID = 0;
                    anim.ActualOffset = anim.ActualOffset % (1 / anim.Speed);
                }
            }
        }
        public static Animation Get(string name)
        {
            foreach (Animation anim in All)
            {
                if (anim.Name == name) return anim;
            }
            return null;
        }
        public static void Setup(Animation anim)
        {
            All.Add(anim);
        }
    }
}
