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
    [Serializable]
    public abstract class RenderableObject : _2Dobject, ISerializableData
    {

        private static uint nextID = 0;
        /// <value>Gets all renderable objects</value>
        public static List<RenderableObject> All { get; private set; } = new List<RenderableObject>();
        private static List<RenderableObject> AddQueue = new List<RenderableObject>();
        private static List<RenderableObject> RemoveQueue = new List<RenderableObject>();

        private uint ID;

        /// <value>Gets object name</value>
        public string Name;

        private string worldName;
        /// <value>Gets world of an object</value>
        [NonSerialized]
        public World World;

        /// <value>
        /// if true - object will be rendered
        /// if false - it will not
        /// </value>
        public bool Visible = true;


        private string renderLayerName;
        /// <value>Gets render layer of an object</value>
        [NonSerialized]
        public Layer RenderLayer;

        private uint parentID;
        /// <value>Gets parent of an object</value>
        [NonSerialized]
        public RenderableObject Parent;

        /// <value>Gets children of an object</value>
        public List<RenderableObject> GetChildren()
        {
            List<RenderableObject> toReturn = new List<RenderableObject>();
            foreach (RenderableObject obj in All)
            {
                if (obj.Parent == this) toReturn.Add(obj);
            }
            return toReturn;
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

        public virtual void Destroy()
        {
            this.addToRemoveQueue();
        }
        public virtual void ReassignToWorld()
        {
            this.addToQueue();
        }

        public virtual void SetVisiblity(bool visiblity)
        {
            this.Visible = visiblity;
        }

        #region constructors

        protected RenderableObject(string name)
        {
            this.ID = nextID;
            nextID++;
            this.Name = name;
            this.RenderLayer = Layer.Default;
            this.World = World.Default;
            this.SetPosition(0, 0);
            this.Visible = true;
            addToQueue();
        }
        protected RenderableObject(string name, Layer layer)
        {
            this.ID = nextID;
            nextID++;
            this.Name = name;
            this.RenderLayer = layer;
            this.World = World.Default;
            this.SetPosition(0, 0);
            this.Visible = true;
            addToQueue();
        }
        protected RenderableObject(string name, World world)
        {
            this.ID = nextID;
            nextID++;
            this.Name = name;
            this.RenderLayer = Layer.Default;
            this.World = world;
            this.SetPosition(0, 0);
            this.Visible = true;
            addToQueue();

        }
        protected RenderableObject(string name, Layer layer, World world)
        {
            this.ID = nextID;
            nextID++;
            this.Name = name;
            this.RenderLayer = layer;
            this.World = world;
            this.SetPosition(0, 0);
            this.Visible = true;
            addToQueue();

        }
        protected RenderableObject() { }

        #endregion

        #region finding

        private RenderableObject findByID(uint id)
        {
            foreach (RenderableObject o in All)
                if (o.ID == id) return o;
            throw new ObjectException("Could not find object with ID " + id);
        }

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

            foreach (RenderableObject child in this.GetChildren())
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
            foreach (RenderableObject obj in this.GetChildren())
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

        #region serialization

        public virtual void BeforeSerialization()
        {
            if (this.Parent == null)
                this.parentID = uint.MaxValue;
            else
                parentID = this.Parent.ID;
            worldName = this.World.Name;
            renderLayerName = this.RenderLayer.Name;
        }

        public virtual void AfterDeserialization()
        {
            if (this.parentID == uint.MaxValue)
                this.Parent = null;
            else
                this.Parent = findByID(this.parentID);
            this.World = World.Find(this.worldName);
            this.RenderLayer = Layer.Find(this.renderLayerName);
        }

        #endregion


        #region adding and removing

        private void addToQueue()
        {
            AddQueue.Add(this);
        }

        private void addToRemoveQueue()
        {
            RemoveQueue.Add(this);
        }

        public static void UpdateAllObjects()
        {
            while(AddQueue.Count != 0)
            {
                All.Add(AddQueue[0]);
                AddQueue[0].World.Objects.Add(AddQueue[0]);
                AddQueue.Remove(AddQueue[0]);
            }

            while (RemoveQueue.Count != 0)
            {
                All.Remove(RemoveQueue[0]);
                RemoveQueue[0].World.Objects.Remove(RemoveQueue[0]);
                RemoveQueue.Remove(RemoveQueue[0]);
            }
        }

        #endregion

        public abstract void Render(System.Drawing.Graphics g, Point startPosition);

        /// <summary>
        /// Checks point is inside this object.
        /// </summary>
        public abstract bool CheckPointInside(Point p);

    }
}
