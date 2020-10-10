using ShadowBuild.Objects.Collision;
using ShadowBuild.Objects.Texturing;
using ShadowBuild.Rendering;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Objects
{
    /// <summary>
    /// Game object class;
    /// Game objects are renderable objects with textures and collisions
    /// </summary>
    public class GameObject : TexturedObject
    {
        /// <value>Object collider</value>
        public PolygonCollider Collider = null;

        public GameObject(string name, Texture texture) : base(name, texture) { }
        public GameObject(string name, Texture texture, Layer layer) : base(name, texture, layer) { }
        public GameObject(string name, Texture texture, World world) : base(name, texture, world) { }
        public GameObject(string name, Texture texture, Layer layer, World world) : base(name, texture, layer, world) { }
        private GameObject() { }

        private bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            PolygonCollider col1 = obj1.Collider;
            PolygonCollider col2 = obj2.Collider;

            if (col1 == null || col2 == null || col1 == col2)
                return false;

            return PolygonCollider.CheckCollision(obj1, col1, obj2, col2);
        }

        public override void Rotate(float rotation)
        {
            List<RenderableObject> childrenWithThis = this.GetAllGrandchildren();
            childrenWithThis.Add(this);

            this.Rotation += rotation;
            foreach (RenderableObject child in childrenWithThis)
            {
                if (!(child is GameObject)) continue;

                GameObject childG = (GameObject)child;

                foreach (RenderableObject robj in World.Objects)
                {
                    if (!(robj is GameObject)) continue;
                    GameObject obj = (GameObject)robj;
                    if (obj == childG || obj.IsChildOf(childG) || childG.IsChildOf(obj)) continue;

                    if (CheckCollision(childG, obj))
                    {
                        this.Rotation -= rotation;
                        return;
                    }
                }
            }
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

            this.SetPosition(this.Position.X + X, this.Position.Y + Y);
            foreach (RenderableObject child in childrenWithThis)
            {
                if (!(child is GameObject)) continue;

                GameObject childG = (GameObject)child;

                foreach (RenderableObject robj in World.Objects)
                {
                    if (!(robj is GameObject)) continue;
                    GameObject obj = (GameObject)robj;
                    if (obj == childG || obj.IsChildOf(childG) || childG.IsChildOf(obj)) continue;

                    if (CheckCollision(childG, obj))
                    {
                        this.SetPosition(this.Position.X - X, this.Position.Y - Y);
                        return;
                    }
                }
            }

        }

        public override void Render(System.Drawing.Graphics g, Point startPosition)
        {
            if (this.ActualTexture != null)
                this.ActualTexture.Render(g, this, startPosition);
        }


    }
}
