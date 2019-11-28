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
        public EmptyTexture defaultTexture { get; private set; }
        public EmptyTexture actualTexture
        {
            get { if (playingAnimation) return actualTexture; else return this.defaultTexture; }
            private set { this.actualTexture = value; }
        }
        public bool playingAnimation { get; private set; }
        public List<Collider> colliders { get; private set; } = new List<Collider>();
        public bool isRendered { get; private set; }

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
    }
}
