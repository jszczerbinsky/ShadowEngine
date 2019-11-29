using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowBuild.Input
{
    public static class Keyboard
    {
        private static List<Keys> pressedKeys = new List<Keys>();

        private static bool checkExists(Keys k)
        {
            foreach (Keys k1 in pressedKeys)
            {
                if (k1 == k) return true;
            }
            return false;
        }
        public static void setKeyState(KeyEventArgs a, bool pressed)
        {
            if (pressed)
            {
                if (!checkExists(a.KeyCode))
                    pressedKeys.Add(a.KeyCode);
            }
            else
            {
                if (checkExists(a.KeyCode))
                    pressedKeys.Remove(a.KeyCode);
            }
        }
        public static bool keyPressed(Keys k)
        {
            return checkExists(k);
        }
    }
}
