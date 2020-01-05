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
        internal Keys minusValue;
        internal Keys plusValue;
        public string minusValueString
        {
            get { return minusValue.ToString(); }
            set { minusValue = (Keys)Enum.Parse(typeof(Keys), value); }
        }
        public string plusValueString
        {
            get { return plusValue.ToString(); }
            set { plusValue = (Keys)Enum.Parse(typeof(Keys), value); }
        }

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
        public static void SaveConfig(string path)
        {
            var serialized = new { axes = new List<Axis>() };
            foreach (Axis a in Axes)
                serialized.axes.Add(a);

            WriteConfigFile(path, serialized);
        }
        public static void LoadConfig(string path)
        {
            dynamic val = ReadConfigFile(path);

            foreach (Dictionary<string, object> dict in val["axes"])
            {
                Axis a = new Axis(
                    (string)dict["name"],
                    (Keys)Enum.Parse(typeof(Keys), (string)dict["minusValueString"]),
                    (Keys)Enum.Parse(typeof(Keys), (string)dict["plusValueString"]));
                Axis.Setup(a);
            }

        }
    }
}
