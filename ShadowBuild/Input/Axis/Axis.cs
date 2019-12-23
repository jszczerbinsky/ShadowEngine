using Newtonsoft.Json;
using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;

namespace ShadowBuild.Input.Axis
{
    [Serializable]
    public abstract class Axis : ConfigSavable
    {
        private static List<Axis> Axes = new List<Axis>();

        public string name;

        [NonSerialized]
        [JsonIgnore]
        public double value;

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
            if (axis == null) throw new AxisNotFoundException();

            double value = 0;

            if (axis is KeyboardAxis)
            {
                KeyboardAxis ka = (KeyboardAxis)axis;
                if (Keyboard.KeyPressed(ka.minusValue))
                    value--;
                if (Keyboard.KeyPressed(ka.plusValue))
                    value++;

            }
            return value;
        }
        public static string GetActualConfig()
        {
            List<KeyboardAxis> k = new List<KeyboardAxis>();
            List<MouseAxis> m = new List<MouseAxis>();
            foreach (Axis a in Axes)
                if (a is KeyboardAxis) k.Add((KeyboardAxis)a);
                else m.Add((MouseAxis)a);

            var serialized = new { keyboard = k, mouse = m };
            return JsonConvert.SerializeObject(serialized);
        }
        public static void SaveConfig(string path, ConfigType cfgType)
        {
            var serialized = new { keyboard = new List<KeyboardAxis>(), mouse = new List<MouseAxis>() };
            foreach (Axis a in Axes)
            {
                if (a is KeyboardAxis)
                    serialized.keyboard.Add((KeyboardAxis)a);
                else serialized.mouse.Add((MouseAxis)a);
            }
            WriteConfigFile(path, serialized, cfgType);
        }
        public static void LoadConfig(string path, ConfigType cfgType)
        {
            var deserialized = new { keyboard = new List<KeyboardAxis>(), mouse = new List<MouseAxis>() };

            deserialized = ReadConfigFile(path, deserialized, cfgType);

            foreach (KeyboardAxis a in deserialized.keyboard)
            {
                Axes.Add(a);
            }
            foreach (MouseAxis a in deserialized.mouse)
            {
                Axes.Add(a);
            }
        }
    }
}
