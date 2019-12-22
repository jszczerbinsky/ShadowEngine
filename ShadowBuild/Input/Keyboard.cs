using System.Collections.Generic;
using System.Windows.Forms;

namespace ShadowBuild.Input
{
    public static class Keyboard
    {
        private static List<Keys> PressedKeys = new List<Keys>();

        private static bool CheckExists(Keys k)
        {
            foreach (Keys k1 in PressedKeys)
            {
                if (k1 == k) return true;
            }
            return false;
        }
        public static void SetKeyState(KeyEventArgs a, bool pressed)
        {
            if (pressed)
            {
                if (!CheckExists(a.KeyCode))
                    PressedKeys.Add(a.KeyCode);
            }
            else
            {
                if (CheckExists(a.KeyCode))
                    PressedKeys.Remove(a.KeyCode);
            }
        }
        public static bool KeyPressed(Keys k)
        {
            return CheckExists(k);
        }
    }
}
