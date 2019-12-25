using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShadowBuild.Input.Keyboard
{
    [Serializable]
    public class Axis : ConfigSavable
    {
        private static List<Axis> Axes = new List<Axis>();

        public string name;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("-")]
        internal Keys minusValue;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("+")]
        internal Keys plusValue;

        public Axis(string name, Keys minusValue, Keys plusValue)
        {
            this.minusValue = minusValue;
            this.plusValue = plusValue;
            this.name = name;
        }

        public static void Setup(Axis axis)
        {
            Axes.Add(axis);
        }
        private static Axis Get(string name)
        {
            foreach (Axis axis in Axes)
                if (axis.name == name) return axis;
            return null;
        }
        public static double GetValue(string name)
        {
            Axis axis = Get(name);
            if (axis == null) throw new AxisException("Cannot find axis \"" + name + "\"");

            double value = 0;

            if (Keyboard.KeyPressed(axis.minusValue))
                value--;
            if (Keyboard.KeyPressed(axis.plusValue))
                value++;

            return value;
        }
        public static string GetActualConfig()
        {
            List<Axis> k = new List<Axis>();
            foreach (Axis a in Axes)
                k.Add(a);

            return JsonConvert.SerializeObject(k);
        }
        public static void SaveConfig(string path, ConfigType cfgType)
        {
            var serialized = new List<Axis>();
            foreach (Axis a in Axes)
                serialized.Add(a);

            WriteConfigFile(path, serialized, cfgType);
        }
        public static void LoadConfig(string path, ConfigType cfgType)
        {
            var deserialized = new List<Axis>();

            deserialized = ReadConfigFile(path, deserialized, cfgType);

            Axes = new List<Axis>();

            foreach (Axis a in deserialized)
                Axes.Add(a);

        }
    }
}
