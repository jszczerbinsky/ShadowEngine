using ShadowBuild.Config;
using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShadowBuild.Input.Keyboard
{
    public class Axis : ConfigSavable
    {
        private static List<Axis> Axes = new List<Axis>();

        public string Name;
        internal Keys Negative;
        internal Keys Positive;
        public string NegativeName
        {
            get { return Negative.ToString(); }
        }
        public string PositiveName
        {
            get { return Positive.ToString(); }
        }

        public Axis(string name, Keys minusValue, Keys plusValue)
        {
            this.Negative = minusValue;
            this.Positive = plusValue;
            this.Name = name;
        }

        public static void Setup(Axis axis)
        {
            if (Axis.Find(axis.Name) != null) throw new AxisException("Axis name \"" + axis.Name + "\" is already used");
            Axes.Add(axis);
        }
        public static Axis Find(string name)
        {
            foreach (Axis axis in Axes)
                if (axis.Name == name) return axis;
            return null;
        }
        public static double GetValue(string name)
        {
            Axis axis = Find(name);
            if (axis == null) throw new AxisException("Cannot find axis \"" + name + "\"");

            double value = 0;

            if (Keyboard.KeyPressed(axis.Negative))
                value--;
            if (Keyboard.KeyPressed(axis.Positive))
                value++;

            return value;
        }
        public static void SaveConfig(string path)
        {
            var serialized = new { Axes = new List<Axis>() };
            foreach (Axis a in Axes)
                serialized.Axes.Add(a);

            WriteConfigFile(path, serialized);
        }
        public static void LoadConfig(string path)
        {
            Axes = new List<Axis>();
            dynamic val = ReadConfigFile(path);


            try
            {
                var i = val["Axes"];
            }catch(Exception e)
            {
                throw new ConfigException(path+" config file is incorrect",e);
            }
            foreach (Dictionary<string, object> dict in val["Axes"])
            {
                string name = "";
                Keys pos;
                Keys neg;
                try
                {
                    name = (string)dict["Name"];
                    pos = (Keys)Enum.Parse(typeof(Keys), (string)dict["PositiveName"]);
                    neg = (Keys)Enum.Parse(typeof(Keys), (string)dict["NegativeName"]);
                }
                catch (Exception e)
                {
                    throw new ConfigException(path+" config file is incorrect", e);
                }
                Axis a = new Axis(name, neg, pos);
                Axis.Setup(a);
            }

        }
    }
}
