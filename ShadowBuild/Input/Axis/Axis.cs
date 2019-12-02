using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Input.Axis
{
    public abstract class Axis
    {
        private static List<Axis> axes = new List<Axis>();

        public string name;
        public double value;

        public static void setup(Axis axis)
        {
            axes.Add(axis);
        }
        private static Axis getAxisByName(string name)
        {
            foreach (Axis axis in axes)
                if (axis.name == name) return axis;
            return null;
        }
        public static double getValue(string name)
        {
            Axis axis = getAxisByName(name);
            if (axis == null) throw new AxisNotFoundException();

            double value = 0;

            if(axis is KeyboardAxis)
            {
                KeyboardAxis ka = (KeyboardAxis)axis;
                if (Keyboard.keyPressed(ka.minusValue))
                    value--;
                if (Keyboard.keyPressed(ka.plusValue))
                    value++;

            }
            return value;
        }
    }
}
