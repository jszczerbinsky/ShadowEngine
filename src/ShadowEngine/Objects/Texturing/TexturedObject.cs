using ShadowEngine.Objects.Parameters;
using ShadowEngine.Objects.Animationing;
using ShadowEngine.Objects.Texturing.Image;
using ShadowEngine.Rendering;
using System;

namespace ShadowEngine.Objects.Texturing
{
    /// <summary>
    /// Textured object class.
    /// Textured objects are renderable objects, that can use textures.
    /// </summary>
    [Serializable]
    public abstract class TexturedObject : RenderableObject
    {

        private string defaultTextureName;
        /// <value>Default object texture</value>
        [NonSerialized]
        public Texture DefaultTexture;

        /// <value>Actual object texture (if there is no animation it returns default texture)</value>
        public Texture GetActualTexture()
        {
            if (ActualAnimation != null) return ActualAnimation.Textures[animationTextureID];
            return this.DefaultTexture;
        }

        private double animationOffset = 0;
        private int animationTextureID = 0;

        public virtual void OnAnimationEnd(Animation anim) { }


        private string actualAnimationName;
        /// <value>Actual animation</value>
        [NonSerialized]
        public Animation ActualAnimation = null;

        protected TexturedObject()
        {
            Loop.OnTick += this.UpdateAnimationTexture;
        }
        protected TexturedObject(string name, Texture texture) : base(name)
        {
            this.DefaultTexture = texture;
            Loop.OnTick += this.UpdateAnimationTexture;

        }
        protected TexturedObject(string name, Texture texture, Layer layer) : base(name, layer)
        {
            this.DefaultTexture = texture;
            Loop.OnTick += this.UpdateAnimationTexture;
        }
        protected TexturedObject(string name, Texture texture, World world) : base(name, world)
        {
            this.DefaultTexture = texture;
            Loop.OnTick += this.UpdateAnimationTexture;
        }
        protected TexturedObject(string name, Texture texture, Layer layer, World world) : base(name, layer, world)
        {
            this.DefaultTexture = texture;
            Loop.OnTick += this.UpdateAnimationTexture;
        }

        public void SetTexture(Texture texture)
        {
            this.DefaultTexture = texture;
        }

        public override Size GetRealSize()
        {
            if (this.GetActualTexture() is GridTexture)
            {
                GridTexture tex = (GridTexture)this.GetActualTexture();
                return new Size(
                    this.Size.Width * this.Scale.Width * tex.xCount,
                    this.Size.Height * this.Scale.Height * tex.yCount);
            }
            return base.GetRealSize();
        }

        /// <summary>
        /// Gets start position of an object
        /// </summary>
        public Vector2D GetStartPosition()
        {
            float decreaseLeft = 0;
            float decreaseTop = 0;
            Texture actualTexture = this.GetActualTexture();

            if (actualTexture is RegularTexture || actualTexture is ColorTexture)
            {
                decreaseLeft -= this.Scale.Width * this.Size.Width / 2;
                decreaseTop -= this.Scale.Height * this.Size.Height / 2;
            }
            else if (actualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.GetActualTexture();
                decreaseLeft -= this.Scale.Width * this.Size.Width * tex.xCount / 2;
                decreaseTop -= this.Scale.Height * this.Size.Height * tex.yCount / 2;
            }
            Vector2D decrease = new Vector2D(decreaseLeft, decreaseTop);

            return this.GetNonRotatedGlobalPosition() + decrease;
        }

        public override bool CheckPointInside(Vector2D p)
        {
            Vector2D start = this.GetStartPosition();

            Size tmp;

            tmp = this.GetRealSize();
            Vector2D end = GetStartPosition() + tmp;

            if (
                p.X > start.X &&
                p.X < end.X &&
                p.Y > start.Y &&
                p.Y < end.Y
             )
                return true;
            return false;
        }

        /// <summary>
        /// Plays animation
        /// </summary>
        /// <param name="animName">name of animation</param>
        public void Play(string animName)
        {
            animationOffset = 0;
            animationTextureID = 0;
            Animation anim = Animation.Get(animName);
            this.ActualAnimation = anim;
        }

        /// <summary>
        /// Plays animation
        /// </summary>
        /// <param name="anim">animation</param>
        public void Play(Animation anim)
        {
            animationOffset = 0;
            animationTextureID = 0;
            this.ActualAnimation = anim;
        }

        /// <summary>
        /// Stops playing actual animationrealsize
        /// </summary>
        public void StopPlaying()
        {
            this.ActualAnimation = null;
        }

        public void UpdateAnimationTexture()
        {
            if (ActualAnimation == null) return;

            try
            {
                animationOffset += Loop.Delay;

                if (animationOffset > ActualAnimation.Length / ActualAnimation.Textures.Count)
                {
                    animationOffset -= ActualAnimation.Length / ActualAnimation.Textures.Count;
                    animationTextureID++;
                    if (animationTextureID >= ActualAnimation.Textures.Count)
                    {
                        animationTextureID = 0;
                        OnAnimationEnd(ActualAnimation);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
                throw e;
            }

        }

        public override void BeforeSerialization()
        {
            base.BeforeSerialization();
            if (this.ActualAnimation == null)
                this.actualAnimationName = null;
            else
                this.actualAnimationName = this.ActualAnimation.Name;
            if (this.DefaultTexture == null)
                this.defaultTextureName = null;
            else
                this.defaultTextureName = this.DefaultTexture.Name;
        }

        public override void AfterDeserialization()
        {
            base.AfterDeserialization();
            if (this.actualAnimationName == null)
                this.ActualAnimation = null;
            else
                this.ActualAnimation = Animation.Get(actualAnimationName);
            if (this.defaultTextureName == null)
                this.DefaultTexture = null;
            else
                this.DefaultTexture = Texture.Get(defaultTextureName);
        }

    }
}
