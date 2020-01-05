using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Objects;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ShadowBuild.Rendering
{
    public class Layer : ConfigSavable, IComparable<Layer>
    {
        public static readonly Layer Default = new Layer("default", 0);
        internal static readonly List<Layer> All = new List<Layer>() { Layer.Default };

        [ScriptIgnore]
        public List<RenderableObject> Objects
        {
            get
            {
                List<RenderableObject> objs = new List<RenderableObject>();
                foreach (RenderableObject obj in RenderableObject.All)
                {
                    if (obj.RenderLayer == this) objs.Add(obj);
                }
                return objs;
            }
            private set { }
        }

        public readonly int zIndex;
        public readonly string Name;

        public Layer(string name, int zIndex)
        {
            this.Name = name;
            this.zIndex = zIndex;
        }
        public static Layer Find(string name)
        {
            Layer l = FindWithoutException(name);
            if(l == null)
            throw new LayerException("Cannot find layer \"" + name + "\"");
            return l;
        }
        public static Layer Find(int zIndex)
        {
            Layer l = FindWithoutException(zIndex);
            if (l == null)
                throw new LayerException("Cannot find layer with zIndex " + zIndex);
            return l;
        }
        private static Layer FindWithoutException(string name)
        {
            foreach (Layer l in All)
            {
                if (l.Name == name)
                    return l;
            }return null;
        }
        private static Layer FindWithoutException(int zIndex)
        {
            foreach (Layer l in All)
            {
                if (l.zIndex == zIndex)
                    return l;
            }return null;
        }


        public static void Setup(Layer layer)
        {
            if (Layer.FindWithoutException(layer.Name) != null) throw new LayerException("Layer name \"" + layer.Name + "\" is already in use");
            if (Layer.FindWithoutException(layer.zIndex) != null) throw new LayerException("Layer zIndex [" + layer.zIndex + "] is already in use");
            All.Add(layer);
        }
        public int CompareTo(Layer obj)
        {
            if (this.zIndex > obj.zIndex) return 1;
            return 0;
        }
        public static void SaveConfig(string path)
        {
            var serialized = new { layers = new List<Layer>(All) };
            serialized.layers.Remove(Layer.Find("default"));
            WriteConfigFile(path, serialized);
        }
        public static void LoadConfig(string path)
        {
            dynamic val = ReadConfigFile(path);
            foreach (Dictionary<string, object> dict in val["layers"])
            {
                All.Add(new Layer((string)dict["Name"], (int)dict["zIndex"]));
            }

        }
    }
}
