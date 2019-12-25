using ShadowBuild.Objects.Animationing;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Objects.Texturing.Image;
using ShadowBuild.Rendering;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Objects
{
    public class GameObject : _2DobjectResizable
    {
        public static List<GameObject> All { get; private set; } = new List<GameObject>();
        public uint zIndex;

        public string Name;

        public Texture DefaultTexture { get; private set; }
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
        public readonly Layer RenderLayer;

        public bool collidable = true;

        public GameObject Parent { get; private set; }
        public List<GameObject> Children
        {
            get
            {
                List<GameObject> toReturn = new List<GameObject>();
                foreach (GameObject obj in All)
                {
                    if (obj.Parent == this) toReturn.Add(obj);
                }
                return toReturn;
            }
            private set { }
        }

        public GameObject(string name, Texture texture)
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
        public GameObject(string name, Texture texture, Layer layer)
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

        //Special constructor only for creating tmp objects for collisions calculations
        private GameObject(GameObject obj)
        {
            this.Position = obj.GetGlobalPosition();
            this.DefaultTexture = obj.ActualTexture;
            this.Size = obj.Size;
            this.collidable = true;
        }

        private bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            if (obj1.collidable && obj2.collidable && (Collision.Check(obj1, obj2) || Collision.Check(obj2, obj1))) return true;
            return false;
        }

        public void SetParent(GameObject obj)
        {
            this.Parent = obj;
        }
        public List<GameObject> GetAllGrandchildren()
        {
            List<GameObject> objs = new List<GameObject>();

            foreach (GameObject child in this.Children)
            {
                List<GameObject> toMerge = child.GetAllGrandchildren();
                toMerge.Add(child);
                foreach (GameObject toMergeObj in toMerge)
                    objs.Add(toMergeObj);

            }
            return objs;
        }
        public bool IsChildOf(GameObject parent)
        {
            if (this.Parent == parent) return true;
            else if (this.Parent == null) return false;
            else return this.Parent.IsChildOf(parent);
        }

        public override void Move(double X, double Y)
        {
            if (X != 0 && Y != 0)
            {
                this.Move(X, 0);
                this.Move(0, Y);
                return;
            }

            List<GameObject> childrenWithThis = this.GetAllGrandchildren();
            childrenWithThis.Add(this);

            foreach (GameObject child in childrenWithThis)
            {
                GameObject tmpObject = new GameObject(child);
                tmpObject.SetPosition(new Point(child.GetGlobalPosition().X + X, child.GetGlobalPosition().Y + Y));

                foreach (GameObject obj in All)
                {
                    if (obj == child || obj.IsChildOf(child) || child.IsChildOf(obj)) continue;

                    if (CheckCollision(tmpObject, obj)) return;
                }
            }
            this.SetPosition(this.Position.X + X, this.Position.Y + Y);

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
