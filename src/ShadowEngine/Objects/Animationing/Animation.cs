using ShadowEngine.Objects.Texturing;
using System.Collections.Generic;

namespace ShadowEngine.Objects.Animationing
{
    /// <summary>
    /// Animation class.
    /// With this class you can do animations from multiple textures.
    /// </summary>
    public class Animation
    {
        
        private static List<Animation> All = new List<Animation>();

        /// <value>Gets name of animation.</value>
        public readonly string Name;

        /// <value>Gets animation textures.</value>
        public readonly List<Texture> Textures;

        /// <value>Animation speed.</value>
        public double Length = 1;

        /// <summary>
        /// Animation constructor.
        /// </summary>
        /// <param name="name">animation name</param>
        /// <param name="textures">list of textures to be used as animation frames</param>
        public Animation(string name, List<Texture> textures)
        {
            this.Name = name;
            this.Textures = textures;
        }

        /// <summary>
        /// Finds animation by name. Animation has to be set up before.
        /// </summary>
        public static Animation Get(string name)
        {
            foreach (Animation anim in All)
            {
                if (anim.Name == name) return anim;
            }
            return null;
        }

        /// <summary>
        /// Sets up animation.
        /// </summary>
        public static void Setup(Animation anim)
        {
            All.Add(anim);
        }
    }
}
