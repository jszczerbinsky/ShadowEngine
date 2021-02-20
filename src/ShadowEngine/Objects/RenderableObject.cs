using ShadowEngine.Exceptions;
using ShadowEngine.Input.Mouse;
using ShadowEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ShadowEngine.Objects
{
    /// <summary>
    /// Renderable objects class.
    /// Renderable objects can be rendered on game window.
    /// </summary>
    public abstract class RenderableObject : _2Dobject
    {
        /// <value>Gets all renderable objects</value>
        public static List<RenderableObject> All { get; private set; } = new List<RenderableObject>();

        /// <value>Gets object name</value>
        public string Name;

        /// <value>Gets world of an object</value>
        public World World { get; private set; }

        /// <value>
        /// if true - object will be rendered
        /// if false - it will not
        /// </value>
        public bool Visible = true;

        /// <value>Gets render layer of an object</value>
        public Layer RenderLayer;

        /// <value>Gets parent of an object</value>
        public RenderableObject Parent { get; private set; }

        /// <value>Gets children of an object</value>
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

        /// <value>if mouse is over this object it will be true</value>
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

        /// <value>if mouse is over this object and there is left mouse button click it will be true</value>
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

        /// <summary>
        /// Finds object by name
        /// </summary>
        public static RenderableObject Get(string name)
        {
            foreach (RenderableObject o in All)
                if (o.Name == name) return o;
            throw new ObjectException("Could not find object \"" + name + "\"");
        }

        /// <summary>
        /// Finds object by name.
        /// </summary>
        /// <typeparam name="T">Type of object (TexturedObject, GameObject etc.)</typeparam>
        public static T Get<T>(string name) where T : RenderableObject
        {
            foreach (RenderableObject o in All)
                if (o.Name == name && o is T) return (T)o;
            throw new ObjectException("Could not find object \"" + name + "\" with type \"" + typeof(T).FullName + "\"");
        }

        #endregion

        #region genealogic

        /// <summary>
        /// Sets parent of an object
        /// </summary>
        /// <param name="obj">parent</param>
        public void SetParent(RenderableObject obj)
        {
            this.Parent = obj;
        }

        /// <summary>
        /// Gets all grandchildren
        /// </summary>
        /// <returns>
        /// All granchildren of an object (All objects that are children of this object and all their grandchildren)
        /// </returns>
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

        /// <summary>
        /// Checks if object is children of another object.
        /// </summary>
        public bool IsChildOf(RenderableObject parent)
        {
            if (this.Parent == parent) return true;
            if (this.Parent == null) return false;
            return this.Parent.IsChildOf(parent);
        }

        #endregion

        #region global transform

        /// <summary>
        /// Returns global position ignoring rotation and all grandparents rotation.
        /// </summary>
        public Point GetNonRotatedGlobalPosition()
        {
            Point tmpPosition = this.Position;
            if (this.Parent != null)
            {
                tmpPosition = new Point(tmpPosition.X + this.Parent.GetNonRotatedGlobalPosition().X, tmpPosition.Y + this.Parent.GetNonRotatedGlobalPosition().Y);
            }
            return tmpPosition;

        }

        /// <summary>
        /// Returns global position.
        /// </summary>
        public Point GetGlobalPosition()
        {
            Point pos = this.GetNonRotatedGlobalPosition();
            if (this.Parent != null) this.Parent.InheritPosition(ref pos);
            return pos;
        }

        /// <summary>
        /// Returns global rotation.
        /// </summary>
        public float GetGlobalRotation()
        {
            float rot = this.Rotation;
            if (this.Parent != null) Parent.InheritRotation(ref rot);
            return rot;
        }

        #endregion

        #region rotation
        
        public override void RotateTo(Point p)
        {
            float rot = (float)((180 / Math.PI) * Math.Atan2(p.Y - this.GetGlobalPosition().Y, p.X - this.GetGlobalPosition().X));
            if (rot < 0)
            {
                rot = 360 - (-rot);
            }
            rot += 90;

            float increaseRot = this.GetGlobalRotation() - Rotation;
            Rotation = rot - increaseRot;

        }

        #endregion

        #region World

        private void MoveChildrenToWorld(World w)
        {
            foreach (RenderableObject obj in this.Children)
                obj.MoveToWorld(w);
        }

        /// <summary>
        /// Moves object to another world.
        /// </summary>
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

        /// <summary>
        /// Moves object to another world.
        /// </summary>
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

        /// <summary>
        /// Inherit rotation from all grandparents.
        /// </summary>
        public void InheritRotation(ref float rot)
        {
            rot += this.Rotation;
            if (this.Parent != null) this.Parent.InheritRotation(ref rot);
        }

        /// <summary>
        /// Inherit position of all grandparents, including rotation transformations.
        /// </summary>
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

        /// <summary>
        /// Checks point is inside this object.
        /// </summary>
        public abstract bool CheckPointInside(Point p);


    }
}
