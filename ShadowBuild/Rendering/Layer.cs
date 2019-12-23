using Newtonsoft.Json;
using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using ShadowBuild.Objects;
using System;
using System.Collections.Generic;

namespace ShadowBuild.Rendering
{
    public class Layer : ConfigSavable, IComparable<Layer>
    {
        public static readonly Layer Default = new Layer("default", 0);
        internal static readonly List<Layer> All = new List<Layer>() { Layer.Default };

        [JsonIgnore]
        public List<GameObject> GameObjects
        {
            get
            {
                List<GameObject> objs = new List<GameObject>();
                foreach (GameObject obj in GameObject.All)
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
            foreach (Layer l in All)
            {
                if (l.Name == name)
                    return l;
            }
            throw new LayerException("Cannot find layer \"" + name + "\"");
        }
        public static Layer Find(int zIndex)
        {
            foreach (Layer l in All)
            {
                if (l.zIndex == zIndex)
                    return l;
            }
            throw new LayerException("Cannot find layer with zIndex [" + zIndex + "]");
        }
        public static void Setup(Layer layer)
        {
            if (Layer.Find(layer.Name) != null) throw new LayerException("Layer name \""+layer.Name+"\" is already in use");
            if (Layer.Find(layer.zIndex) != null) throw new LayerException("Layer zIndex ["+layer.zIndex+"] is already in use");
            All.Add(layer);
        }
        public int CompareTo(Layer obj)
        {
            if (this.zIndex > obj.zIndex) return 1;
            return 0;
        }
        public static string GetActualConfig()
        {
            var o = new { layers = new List<Layer>() };
            foreach (Layer l in All)
                if (l != Layer.Default)
                    o.layers.Add(l);
            return JsonConvert.SerializeObject(o);
        }
        public static void LoadConfig(string path, ConfigType cfgType)
        {
            var deserialized = new { layers = new List<Layer>() };
            deserialized = ReadConfigFile(path, deserialized, cfgType);
            foreach (Layer l in deserialized.layers)
                All.Add(l);

        }
    }
}
