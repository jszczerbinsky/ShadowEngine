using ShadowBuild.Exceptions;
using System.Collections.Generic;

namespace ShadowBuild.Input.Axis
{
    public abstract class Axis
    {
        private static List<Axis> Axes = new List<Axis>();

        public string name;
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
    }
}
