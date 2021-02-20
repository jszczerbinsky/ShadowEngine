using ShadowEngine.Config;
using ShadowEngine.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ShadowEngine.Input.Keyboard
{
    /// <summary>
    /// Keyboard axes class.
    /// </summary>
    public class Axis : ConfigSavable
    {
        private static List<Axis> Axes = new List<Axis>();

        /// <value>Axis name</value>
        public string Name;
        internal Keys Negative;
        internal Keys Positive;

        /// <value>Gets name of a negative key</value>
        public string NegativeName
        {
            get { return Negative.ToString(); }
        }

        /// <value>Gets name of a positive key</value>
        public string PositiveName
        {
            get { return Positive.ToString(); }
        }

        /// <summary>
        /// Keyboard axis constructor.
        /// </summary>
        /// <param name="name">axis name</param>
        /// <param name="negativeKey">negative keyboard key</param>
        /// <param name="positiveKey">positive keyboard key</param>
        public Axis(string name, Keys negativeKey, Keys positiveKey)
        {
            this.Negative = negativeKey;
            this.Positive = positiveKey;
            this.Name = name;
        }

        /// <summary>
        /// Sets up your axis to find it by name later.
        /// </summary>
        public static void Setup(Axis axis)
        {
            if (Axis.Find(axis.Name) != null) throw new AxisException("Axis name \"" + axis.Name + "\" is already used");
            Axes.Add(axis);
        }

        /// <summary>
        /// Finds axis by name.
        /// </summary>
        public static Axis Find(string name)
        {
            foreach (Axis axis in Axes)
                if (axis.Name == name) return axis;
            return null;
        }

        /// <summary>
        /// Gets axis value.
        /// </summary>
        /// <param name="name">axis name</param>
        public static double GetValue(string name)
        {
            Axis axis = Find(name);
            if (axis == null) throw new AxisException("Cannot find axis \"" + name + "\"");
            return GetValue(axis);
        }

        /// <summary>
        /// Gets axis value.
        /// </summary>
        public static double GetValue(Axis axis)
        {
            double value = 0;

            if (Keyboard.KeyPressed(axis.Negative))
                value--;
            if (Keyboard.KeyPressed(axis.Positive))
                value++;

            return value;
        }

        /// <summary>
        /// Saves current axes to file. Axes have to be set up before.
        /// </summary>
        public static void SaveConfig(string path)
        {
            var serialized = new { Axes = new List<Axis>() };
            foreach (Axis a in Axes)
                serialized.Axes.Add(a);

            WriteConfigFile(path, serialized);
        }

        /// <summary>
        /// Loads axes configuration from file.
        /// </summary>
        public static void LoadConfig(string path)
        {
            Axes = new List<Axis>();
            dynamic val = ReadConfigFile(path);


            try
            {
                var i = val["Axes"];
            }
            catch (Exception e)
            {
                throw new ConfigException(path + " config file is incorrect", e);
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
                    throw new ConfigException(path + " config file is incorrect", e);
                }
                Axis a = new Axis(name, neg, pos);
                Axis.Setup(a);
            }

        }
    }
}
