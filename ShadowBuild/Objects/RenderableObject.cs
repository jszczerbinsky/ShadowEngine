using ShadowBuild.Objects.Animationing;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Objects.Texturing.Image;
using ShadowBuild.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class RenderableObject : _2DobjectResizable
    {
        public static List<RenderableObject> All { get; private set; } = new List<RenderableObject>();

        public uint zIndex;

        public string Name;

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
        public bool Visible = true;
        public Layer RenderLayer { get; protected set; }

        public RenderableObject Parent { get; private set; }
        public List<RenderableObject> Children
        {
            get
            {
                List<RenderableObject> toReturn = new List<RenderableObject>();
                foreach (RenderableObject obj in All)
                {
                    if (obj.Parent == this) toReturn.Add(obj);
                }
                return toReturn;
            }
            private set { }
        }

        public RenderableObject(string name, Texture texture)
        {
            this.Name = name;
            this.RenderLayer = Layer.Default;
            this.SetPosition(0, 0);
            this.DefaultTexture = texture;
            this.Visible = true;
            this.SetSize(new Point(1, 1));
            this.zIndex = 0;
            All.Add(this);
        }
        public RenderableObject(string name, Texture texture, Layer layer)
        {
            this.Name = name;
            this.RenderLayer = layer;
            this.SetPosition(0, 0);
            this.DefaultTexture = texture;
            this.Visible = true;
            this.SetSize(new Point(1, 1));
            this.zIndex = 0;
            All.Add(this);

        }
        protected RenderableObject() { }

        public void SetParent(RenderableObject obj)
        {
            this.Parent = obj;
        }
        public List<RenderableObject> GetAllGrandchildren()
        {
            List<RenderableObject> objs = new List<RenderableObject>();

            foreach (RenderableObject child in this.Children)
            {
                List<RenderableObject> toMerge = child.GetAllGrandchildren();
                toMerge.Add(child);
                foreach (RenderableObject toMergeObj in toMerge)
                    objs.Add(toMergeObj);

            }
            return objs;
        }
        public bool IsChildOf(RenderableObject parent)
        {
            if (this.Parent == parent) return true;
            else if (this.Parent == null) return false;
            else return this.Parent.IsChildOf(parent);
        }
        
        public Point GetGlobalPosition()
        {
            Point tmpPosition = this.Position;
            if (this.Parent != null)
            {
                tmpPosition = new Point(tmpPosition.X + this.Parent.GetGlobalPosition().X, tmpPosition.Y + this.Parent.GetGlobalPosition().Y);
            }
            return tmpPosition;

        }

        public Point GetStartPosition()
        {
            double decreseLeft = 0;
            double decreseTop = 0;

            if (ActualTexture is RegularTexture)
            {
                decreseLeft -= ((RegularTexture)this.ActualTexture).Image.Width * this.Size.X / 2;
                decreseTop -= ((RegularTexture)this.ActualTexture).Image.Height * this.Size.Y / 2;
            }
            else if (ActualTexture is ColorTexture)
            {
                decreseLeft -= ((ColorTexture)this.ActualTexture).Size.X * this.Size.X / 2;
                decreseTop -= ((ColorTexture)this.ActualTexture).Size.Y * this.Size.Y / 2;
            }
            else if (ActualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.ActualTexture;
                decreseLeft -= tex.Image.Width * this.Size.X * tex.xCount / 2;
                decreseTop -= tex.Image.Height * this.Size.Y * tex.yCount / 2;
            }
            Point decrese = new Point(decreseLeft, decreseTop);

            return new Point(this.GetGlobalPosition().X + decrese.X, this.GetGlobalPosition().Y + decrese.Y);
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
