using ShadowBuild.Exceptions;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Objects
{
    public class GameObject : TexturedObject
    {
        public bool collidable = true;

        public GameObject(string name, Texture texture) : base(name, texture) { }
        public GameObject(string name, Texture texture, Layer layer) : base(name, texture, layer) { }
        public GameObject(string name, Texture texture, World world) : base(name, texture, world) { }
        public GameObject(string name, Texture texture, Layer layer, World world) : base(name, texture, layer, world) { }
        private GameObject() { }

        private bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            if (obj1.collidable && obj2.collidable && (Collision.Check(obj1, obj2) || Collision.Check(obj2, obj1))) return true;
            return false;
        }

        //This method is only for collision calculations
        private GameObject GetClone(GameObject obj)
        {
            GameObject clone = new GameObject();
            clone.Position = obj.GetGlobalPosition();
            clone.DefaultTexture = obj.ActualTexture;
            clone.BaseSize = obj.BaseSize;
            clone.collidable = true;
            return clone;
        }

        public override void Move(double X, double Y)
        {
            if (X != 0 && Y != 0)
            {
                this.Move(X, 0);
                this.Move(0, Y);
                return;
            }

            List<RenderableObject> childrenWithThis = this.GetAllGrandchildren();
            childrenWithThis.Add(this);

            foreach (RenderableObject child in childrenWithThis)
            {
                if (!(child is GameObject)) continue;
                GameObject childG = (GameObject)child;
                GameObject tmpObject = GetClone(childG);
                tmpObject.SetPosition(new Point(childG.GetGlobalPosition().X + X, childG.GetGlobalPosition().Y + Y));

                foreach (RenderableObject robj in World.Objects)
                {
                    if (!(robj is GameObject)) continue;
                    GameObject obj = (GameObject)robj;
                    if (obj == childG || obj.IsChildOf(childG) || childG.IsChildOf(obj)) continue;

                    if (CheckCollision(tmpObject, obj)) return;
                }
            }
            this.SetPosition(this.Position.X + X, this.Position.Y + Y);

        }

        public override void Render(System.Drawing.Graphics g, Point startPosition)
        {
            if(this.ActualTexture != null)
                this.ActualTexture.Render(g, this, startPosition);
        }


    }
}
