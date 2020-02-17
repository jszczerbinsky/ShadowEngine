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
                else return this.DefaultTexture;
            }
            private set { }
        }
        public Animation ActualAnimation { get; private set; } = null;

        protected TexturedObject() : base() { }
        public TexturedObject(string name, Texture texture) : base(name)
        {
            this.DefaultTexture = texture;
        }
        public TexturedObject(string name, Texture texture, Layer layer) : base(name, layer)
        {
            this.DefaultTexture = texture;
        }

        public override Point GetStartPosition()
        {
            double decreseLeft = 0;
            double decreseTop = 0;

            if (ActualTexture is RegularTexture)
            {
                decreseLeft -= ((RegularTexture)this.ActualTexture).Image.Width * this.Size.Width / 2;
                decreseTop -= ((RegularTexture)this.ActualTexture).Image.Height * this.Size.Height / 2;
            }
            else if (ActualTexture is ColorTexture)
            {
                decreseLeft -= ((ColorTexture)this.ActualTexture).Size.Width * this.Size.Width / 2;
                decreseTop -= ((ColorTexture)this.ActualTexture).Size.Height * this.Size.Height / 2;
            }
            else if (ActualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.ActualTexture;
                decreseLeft -= tex.Image.Width * this.Size.Width * tex.xCount / 2;
                decreseTop -= tex.Image.Height * this.Size.Height * tex.yCount / 2;
            }
            Point decrese = new Point(decreseLeft, decreseTop);

            return new Point(this.GetGlobalPosition().X + decrese.X, this.GetGlobalPosition().Y + decrese.Y);
        }
        public override bool CheckPointInside(Point p)
        {
            Point start = this.GetStartPosition();

            Point tmp;

            tmp = new Point(
                this.ActualTexture.GetSize().Width * this.Size.Width,
                this.ActualTexture.GetSize().Height * this.Size.Height);

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
