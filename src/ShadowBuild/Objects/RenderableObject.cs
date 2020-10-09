using ShadowBuild.Exceptions;
using ShadowBuild.Input.Mouse;
using ShadowBuild.Objects;
using ShadowBuild.Rendering;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class RenderableObject : _2Dobject
    {
        public static List<RenderableObject> All { get; private set; } = new List<RenderableObject>();

        public string Name;
        public World World { get; private set; } 

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
        public virtual bool MouseOver
        {
            get
            {
                if (Mouse.LockCursor || this.World != World.ActualWorld) return false;
                Point p = new Point(
                    Mouse.Position.X + Camera.Default.StartPosition.X,
                    Mouse.Position.Y + Camera.Default.StartPosition.Y
                );
                return CheckPointInside(p);
            }
        }
        public bool Click
        {
            get
            {
                if (MouseOver && Mouse.ButtonClick(System.Windows.Forms.MouseButtons.Left))
                    return true;
                return false;
            }
        }

        #region constructors

        protected RenderableObject(string name)
        {
            this.Name = name;
            this.RenderLayer = Layer.Default;
            this.World = World.Default;
            this.SetPosition(0, 0);
            this.Visible = true;
            All.Add(this);
            this.World.Objects.Add(this);
        }
        protected RenderableObject(string name, Layer layer)
        {
            this.Name = name;
            this.RenderLayer = layer;
            this.World = World.Default;
            this.SetPosition(0, 0);
            this.Visible = true;
            All.Add(this);
            this.World.Objects.Add(this);
        }
        protected RenderableObject(string name, World world)
        {
            this.Name = name;
            this.RenderLayer = Layer.Default;
            this.World = world;
            this.SetPosition(0, 0);
            this.Visible = true;
            All.Add(this);
            this.World.Objects.Add(this);

        }
        protected RenderableObject(string name, Layer layer, World world)
        {
            this.Name = name;
            this.RenderLayer = layer;
            this.World = world;
            this.SetPosition(0, 0);
            this.Visible = true;
            All.Add(this);
            this.World.Objects.Add(this);

        }
        protected RenderableObject() { }

        #endregion

        #region finding

        public static RenderableObject Get(string name)
        {
            foreach (RenderableObject o in All)
                if (o.Name == name) return o;
            throw new ObjectException("Could not find object \"" + name + "\"");
        }
        public static T Get<T>(string name) where T : RenderableObject
        {
            foreach (RenderableObject o in All)
                if (o.Name == name && o is T) return (T)o;
            throw new ObjectException("Could not find object \"" + name + "\" with type \"" + typeof(T).FullName + "\"");
        }

        #endregion

        #region genealogic

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
            if (this.Parent == null) return false;
            return this.Parent.IsChildOf(parent);
        }

        #endregion

        #region global transform

        public Point GetNonRotatedGlobalPosition()
        {
            Point tmpPosition = this.Position;
            if (this.Parent != null)
            {
                tmpPosition = new Point(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
            }
            return tmpPosition;

        }

        public Point GetGlobalPosition()
        {
            Point pos = this.GetNonRotatedGlobalPosition();
            if (this.Parent != null) this.Parent.InheritPosition(ref pos);
            return pos;
        }

        public float GetGlobalRotation()
        {
            float rot = this.Rotation;
            if (this.Parent != null) Parent.InheritRotation(ref rot);
            return rot;
        }

        #endregion

        #region World
        private void MoveChildrenToWorld(World w)
        {
            foreach (RenderableObject obj in this.Children)
                obj.MoveToWorld(w);
        }
        public void MoveToWorld(string worldName)
        {
            foreach (World w in World.All)
                if (w.Name == worldName)
                {
                    this.World.Objects.Remove(this);
                    this.World = w;
                    this.World.Objects.Add(this);
                    MoveChildrenToWorld(w);
                    return;
                }
            throw new WorldNameException("There is no world with name " + worldName);
        }
        public void MoveToWorld(World world)
        {
            this.World.Objects.Remove(this);
            this.World = world;
            this.World.Objects.Add(this);
            MoveChildrenToWorld(world);
        }
        #endregion

        #region inherit

        public void InheritGraphicsTransform(System.Drawing.Graphics g, Point startPos)
        {
            if (this.Parent != null) this.Parent.InheritGraphicsTransform(g, startPos);

            g.TranslateTransform((float)(this.GetNonRotatedGlobalPosition().X - startPos.X), (float)(this.GetNonRotatedGlobalPosition().Y - startPos.Y));
            g.RotateTransform(this.Rotation);
            g.TranslateTransform(-(float)(this.GetNonRotatedGlobalPosition().X - startPos.X), -(float)(this.GetNonRotatedGlobalPosition().Y - startPos.Y));
        }

        public void InheritRotation(ref float rot)
        {
            rot += this.Rotation;
            if (this.Parent != null) this.Parent.InheritRotation(ref rot);
        }

        public void InheritPosition(ref Point p)
        {
            double angleInRadians = this.Rotation * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            p = new Point()
            {
                X =
                    (int)
                    (cosTheta * (p.X - this.GetNonRotatedGlobalPosition().X) -
                    sinTheta * (p.Y - this.GetNonRotatedGlobalPosition().Y) + this.GetNonRotatedGlobalPosition().X),
                Y =
                    (int)
                    (sinTheta * (p.X - GetNonRotatedGlobalPosition().X) +
                    cosTheta * (p.Y - GetNonRotatedGlobalPosition().Y) + GetNonRotatedGlobalPosition().Y)
            };
            if (this.Parent != null) this.Parent.InheritPosition(ref p);
        }

        #endregion

        public abstract void Render(System.Drawing.Graphics g, Point startPosition);

        public abstract bool CheckPointInside(Point p);


    }
}
