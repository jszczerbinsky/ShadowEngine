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
        public static List<GameObject> allGameObjects { get; private set; } = new List<GameObject>();
        public uint zIndex;

        #region constructors

        public GameObject(Texture texture)
        {
            this.setPosition(0, 0);
            this.defaultTexture = texture;
            this.isRendered = true;
            this.setSize(new _2Dsize(1, 1));
            this.zIndex = 0;
            allGameObjects.Add(this);
        }
        
        //Special constructor only for creating tmp objects for collisions calculations
        private GameObject(GameObject obj)
        {
            this.position = obj.globalPosition;
            this.defaultTexture = obj.actualTexture;
            this.size = obj.size;
            this.collidable = true;
        }

        #endregion

        #region IComparable

        public int CompareTo(GameObject other)
        {
            if (other == null) return 1;
            return this.zIndex.CompareTo(other.zIndex);
        }

        #endregion

        #region collisions

        public bool collidable = true;

        #endregion

        #region collisions - methods

        private bool checkCollision(GameObject obj1, GameObject obj2)
        {
            if (obj1.collidable && obj2.collidable && (Collision.check(obj1, obj2) || Collision.check(obj2, obj1))) return true;
            return false;
        }

        #endregion

        #region parents and children

        public GameObject parent { get; private set; }
        public List<GameObject> children
        {
            get
            {
                List<GameObject> toReturn = new List<GameObject>();
                foreach (GameObject obj in allGameObjects)
                {
                    if (obj.parent == this) toReturn.Add(obj);
                }
                return toReturn;
            }
            private set { }
        }

        #endregion

        #region parents and children - methods

        public void setParent(GameObject obj)
        {
            this.parent = obj;
        }
        public List<GameObject> getAllGrandchildren()
        {
            List<GameObject> objs = new List<GameObject>();

            foreach (GameObject child in this.children)
            {
                List<GameObject> toMerge = child.getAllGrandchildren();
                toMerge.Add(child);
                foreach (GameObject toMergeObj in toMerge)
                    objs.Add(toMergeObj);

            }
            return objs;
        }
        public bool isChildOf(GameObject parent)
        {
            if (this.parent == parent) return true;
            else if (this.parent == null) return false;
            else return this.parent.isChildOf(parent);
        }

        #endregion

        #region position

        public _2Dsize globalPosition
        {
            get
            {
                _2Dsize tmpPosition = this.position;
                if (this.parent != null)
                {
                    tmpPosition = _2Dsize.add(tmpPosition, this.parent.globalPosition);
                }
                return tmpPosition;
            }
            private set { }
        }
        public _2Dsize startPosition
        {
            get
            {
                double decreseLeft = 0;
                double decreseTop = 0;

                if (actualTexture is RegularTexture)
                {
                    decreseLeft -= this.actualTexture.image.Width * this.size.X / 2;
                    decreseTop -= this.actualTexture.image.Height * this.size.Y / 2;
                }
                else if (actualTexture is ColorTexture)
                {
                    decreseLeft -= ((ColorTexture)this.actualTexture).size.X * this.size.X / 2;
                    decreseTop -= ((ColorTexture)this.actualTexture).size.Y * this.size.Y / 2;
                }
                else if (actualTexture is GridTexture)
                {
                    GridTexture tex = (GridTexture)this.actualTexture;
                    decreseLeft -= tex.image.Width * this.size.X * tex.xCount / 2;
                    decreseTop -= tex.image.Height * this.size.Y * tex.yCount / 2;
                }
                _2Dsize decrese = new _2Dsize(decreseLeft, decreseTop);

                return _2Dsize.add(this.globalPosition, decrese);
            }
            private set { }
        }

        #endregion

        #region position - methods

        public void move(double X, double Y)
        {
            if(X != 0 && Y != 0)
            {
                this.move(X, 0);
                this.move(0, Y);
                return;
            }

            List<GameObject> childrenWithThis = this.getAllGrandchildren();
            Log.say(childrenWithThis.Count.ToString());
            childrenWithThis.Add(this);

            foreach(GameObject child in childrenWithThis)
            {
                GameObject tmpObject = new GameObject(child);
                tmpObject.setPosition(_2Dsize.add(child.globalPosition, new _2Dsize(X, Y)));

                foreach(GameObject obj in allGameObjects)
                {
                    if (obj == child || obj.isChildOf(child) || child.isChildOf(obj)) continue;

                    if (checkCollision(tmpObject, obj)) return;
                }
            }
            this.setPosition(this.position.X + X, this.position.Y + Y);

        }

        

        #endregion

        #region textures and animations

        public Texture defaultTexture { get; private set; }
        public Texture actualTexture
        {
            get { if (playingAnimation) return actualTexture; else return this.defaultTexture; }
            private set { this.actualTexture = value; }
        }
        public bool playingAnimation { get; private set; }
        public bool isRendered { get; private set; }

        #endregion
    }
}
