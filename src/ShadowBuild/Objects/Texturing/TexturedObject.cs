using ShadowBuild.Objects.Animationing;
using ShadowBuild.Objects.Texturing.Image;
using ShadowBuild.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShadowBuild.Objects.Texturing
{
    public abstract class TexturedObject : RenderableObject
    {
        public Texture DefaultTexture { get; protected set; }
        public Texture ActualTexture
        {
            get
            {
                if (ActualAnimation != null) return ActualAnimation.ActualTexture;
                return this.DefaultTexture;
            }
            private set { }
        }
        public Animation ActualAnimation { get; private set; } = null;

        protected TexturedObject() { }
        protected TexturedObject(string name, Texture texture) : base(name)
        {
            this.DefaultTexture = texture;
        }
        protected TexturedObject(string name, Texture texture, Layer layer) : base(name, layer)
        {
            this.DefaultTexture = texture;
        }
        protected TexturedObject(string name, Texture texture, World world) : base(name, world)
        {
            this.DefaultTexture = texture;
        }
        protected TexturedObject(string name, Texture texture, Layer layer, World world) : base(name, layer, world)
        {
            this.DefaultTexture = texture;
        }

        public override Size GetRealSize()
        {
            if (this.ActualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.ActualTexture;
                return new Size(
                    this.BaseSize.Width * this.SizeMultipler.Height * tex.xCount,
                    this.BaseSize.Height * this.SizeMultipler.Height * tex.yCount);
            }
            return base.GetRealSize();
        }
        public Point GetStartPosition()
        {
            double decreaseLeft = 0;
            double decreaseTop = 0;

            if (ActualTexture is RegularTexture || ActualTexture is ColorTexture)
            {
                decreaseLeft -= this.SizeMultipler.Width * this.BaseSize.Width / 2;
                decreaseTop -= this.SizeMultipler.Height * this.BaseSize.Height / 2;
            }
            else if (ActualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.ActualTexture;
                decreaseLeft -= this.SizeMultipler.Width * this.BaseSize.Width * tex.xCount / 2;
                decreaseTop -= this.SizeMultipler.Height * this.BaseSize.Height * tex.yCount / 2;
            }
            Point decrease = new Point(decreaseLeft, decreaseTop);

            return new Point(this.GetNonRotatedGlobalPosition().X + decrease.X, this.GetNonRotatedGlobalPosition().Y + decrease.Y);
        }
        public override bool CheckPointInside(Point p)
        {
            Point start = this.GetStartPosition();

            Point tmp;

            tmp = new Point(
                this.GetRealSize().Width * this.BaseSize.Width,
                this.GetRealSize().Height * this.BaseSize.Height);

            Point end = new Point(
                this.GetStartPosition().X + tmp.X,
                this.GetStartPosition().Y + tmp.Y);

            if (
                p.X > start.X &&
                p.X < end.X &&
                p.Y > start.Y &&
                p.Y < end.Y
             )
                return true;
            return false;
        }
        public void Play(string animName)
        {
            this.ActualAnimation = Animation.Get(animName);
        }
        public void StopPlaying()
        {
            this.ActualAnimation = null;
        }
    }
}
