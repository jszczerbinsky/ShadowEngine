using System.Collections.Generic;
using System.Windows.Forms;

namespace ShadowEngine.Input.Keyboard
{
    /// <summary>
    /// Keyboard events class.
    /// </summary>
    public static class Keyboard
    {
        private static List<Keys> PressedKeys = new List<Keys>();
        private static List<Keys> ClickedKeys = new List<Keys>();

        private static bool CheckExists(Keys k, List<Keys> where)
        {
            foreach (Keys k1 in where)
            {
                if (k1 == k) return true;
            }
            return false;
        }
        public static void SetKeyState(KeyEventArgs a, bool pressed)
        {
            if (pressed)
            {
                if (!CheckExists(a.KeyCode, PressedKeys))
                    PressedKeys.Add(a.KeyCode);
                if (!CheckExists(a.KeyCode, ClickedKeys))
                    ClickedKeys.Add(a.KeyCode);
            }
            else
            {
                if (CheckExists(a.KeyCode, PressedKeys))
                    PressedKeys.Remove(a.KeyCode);
                if (!CheckExists(a.KeyCode, ClickedKeys))
                    ClickedKeys.Remove(a.KeyCode);
            }
        }

        /// <summary>
        /// Returns true if key is pressed.
        /// </summary>
        public static bool KeyPressed(Keys k)
        {
            return CheckExists(k, PressedKeys);
        }

        /// <summary>
        /// Returns true when key is pushed down.
        /// </summary>
        public static bool KeyClick(Keys k)
        {
            bool check = CheckExists(k, ClickedKeys);
            if (check) ClickedKeys.Remove(k);
            return check;
        }
    }
}
