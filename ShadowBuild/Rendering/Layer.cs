using ShadowBuild.Objects;
using System;
using System.Collections.Generic;

namespace ShadowBuild.Rendering
{
    public class Layer:IComparable<Layer>
    {
        public static readonly Layer Default = new Layer("default", 0);
        internal static readonly List<Layer> All = new List<Layer>() { Layer.Default };

        public List<GameObject> GameObjects
        {
            get
            {
                List<GameObject> objs = new List<GameObject>();
                foreach(GameObject obj in GameObject.All)
                {
                    if (obj.RenderLayer == this) objs.Add(obj);
                }
                return objs;
            }
            private set { }
        }

        public bool Visible = true;

        public readonly int zIndex;
        public readonly string Name;

        public Layer(string name, int zIndex)
        {
            this.Name = name;
            this.zIndex = zIndex;
        }
        public static Layer Find(string name)
        {
            foreach (Layer l in All)
            {
                if (l.Name == name)
                    return l;
            }
            return null;
        }
        public static void Setup(Layer layer)
        {
            All.Add(layer);
        }
        public int CompareTo(Layer obj)
        {
            if (this.zIndex > obj.zIndex) return 1;
            return 0;
        }
    }
}
