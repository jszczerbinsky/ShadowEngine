using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public class GameObject : _2DobjectResizeable, IComparable<GameObject>
    {
        public static List<GameObject> AllGameObjects { get; private set; } = new List<GameObject>();
        public uint zIndex;

        public Texture DefaultTexture { get; private set; }
        public Texture ActualTexture
        {
            get { if (playingAnimation) return ActualTexture; else return this.DefaultTexture; }
            private set { this.ActualTexture = value; }
        }
        public bool playingAnimation { get; private set; }
        public bool isRendered { get; private set; }

        public bool collidable = true;

        public GameObject Parent { get; private set; }
        public List<GameObject> Children
        {
            get
            {
                List<GameObject> toReturn = new List<GameObject>();
                foreach (GameObject obj in AllGameObjects)
                {
                    if (obj.Parent == this) toReturn.Add(obj);
                }
                return toReturn;
            }
            private set { }
        }

        public GameObject(Texture texture)
        {
            this.SetPosition(0, 0);
            this.DefaultTexture = texture;
            this.isRendered = true;
            this.SetSize(new _2Dsize(1, 1));
            this.zIndex = 0;
            AllGameObjects.Add(this);
        }

        //Special constructor only for creating tmp objects for collisions calculations
        private GameObject(GameObject obj)
        {
            this.Position = obj.GetGlobalPosition();
            this.DefaultTexture = obj.ActualTexture;
            this.Size = obj.Size;
            this.collidable = true;
        }

        public int CompareTo(GameObject other)
        {
            if (other == null || other.zIndex == this.zIndex) return 1;
            return this.zIndex.CompareTo(other.zIndex);
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

        public void Move(double X, double Y)
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
                tmpObject.SetPosition(_2Dsize.Add(child.GetGlobalPosition(), new _2Dsize(X, Y)));

                foreach (GameObject obj in AllGameObjects)
                {
                    if (obj == child || obj.IsChildOf(child) || child.IsChildOf(obj)) continue;

                    if (CheckCollision(tmpObject, obj)) return;
                }
            }
            this.SetPosition(this.Position.X + X, this.Position.Y + Y);

        }
        public _2Dsize GetGlobalPosition()
        {
            _2Dsize tmpPosition = this.Position;
            if (this.Parent != null)
            {
                tmpPosition = _2Dsize.Add(tmpPosition, this.Parent.GetGlobalPosition());
            }
            return tmpPosition;

        }

        public _2Dsize GetStartPosition()
        {
            double decreseLeft = 0;
            double decreseTop = 0;

            if (ActualTexture is RegularTexture)
            {
                decreseLeft -= this.ActualTexture.Image.Width * this.Size.X / 2;
                decreseTop -= this.ActualTexture.Image.Height * this.Size.Y / 2;
            }
            else if (ActualTexture is ColorTexture)
            {
                decreseLeft -= ((ColorTexture)this.ActualTexture).size.X * this.Size.X / 2;
                decreseTop -= ((ColorTexture)this.ActualTexture).size.Y * this.Size.Y / 2;
            }
            else if (ActualTexture is GridTexture)
            {
                GridTexture tex = (GridTexture)this.ActualTexture;
                decreseLeft -= tex.Image.Width * this.Size.X * tex.xCount / 2;
                decreseTop -= tex.Image.Height * this.Size.Y * tex.yCount / 2;
            }
            _2Dsize decrese = new _2Dsize(decreseLeft, decreseTop);

            return _2Dsize.Add(this.GetGlobalPosition(), decrese);
        }
    }
}
