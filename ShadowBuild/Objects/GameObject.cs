using ShadowBuild.Objects.Collisions;
using ShadowBuild.Objects.Dimensions;
using ShadowBuild.Objects.Texturing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public class GameObject : _2DobjectResizeable
    {
        public static List<GameObject> allGameObjects { get; private set; } = new List<GameObject>();

        public GameObject parent { get; private set; }

        #region position

        public _2Dsize globalPosition
        {
            get
            {
                _2Dsize tmpPosition = this.position;
                if(this.parent != null)
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

                if(actualTexture is RegularTexture)
                {
                    decreseLeft -= this.actualTexture.image.Width*this.size.X / 2;
                    decreseTop -= this.actualTexture.image.Height*this.size.Y / 2;
                }else if (actualTexture is ColorTexture)
                {
                    decreseLeft -= ((ColorTexture)this.actualTexture).size.X *this.size.X / 2;
                    decreseTop -= ((ColorTexture)this.actualTexture).size.Y  * this.size.Y/ 2;
                }else if (actualTexture is GridTexture)
                {
                    GridTexture tex = (GridTexture)this.actualTexture;
                    decreseLeft -= tex.image.Width * this.size.X * tex.xCount/2;
                    decreseTop -= tex.image.Height * this.size.Y * tex.yCount/2;
                }
                _2Dsize decrese = new _2Dsize(decreseLeft, decreseTop);

                return _2Dsize.add(this.globalPosition, decrese);
            }
            private set { }
        }

        #endregion

        #region textures and animations

        public EmptyTexture defaultTexture { get; private set; }
        public EmptyTexture actualTexture
        {
            get { if (playingAnimation) return actualTexture; else return this.defaultTexture; }
            private set { this.actualTexture = value; }
        }
        public bool playingAnimation { get; private set; }
        public bool isRendered { get; private set; }

        #endregion

        public List<Collider> colliders { get; private set; } = new List<Collider>();

        public GameObject(EmptyTexture texture)
        {
            this.setPosition(0, 0);
            this.defaultTexture = texture;
            this.isRendered = true;
            this.setSize(new _2Dsize(1, 1));
            allGameObjects.Add(this);
        }
        public void setParent(GameObject obj)
        {
            this.parent = obj;
        }
        public void move(double X, double Y)
        {
            //Check collision
            this.position.setDimensions(this.position.X + X, this.position.Y + Y);
        }

    }
}
