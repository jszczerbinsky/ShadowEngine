using ShadowBuild.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace ShadowBuild.Input.Mouse
{
    public static class Mouse
    {
        private static List<MouseButtons> PressedButtons = new List<MouseButtons>();
        private static List<MouseButtons> ClickedButtons = new List<MouseButtons>();

        private static bool lockCursor = false;
        public static bool LockCurosr
        {
            get
            {
                return lockCursor;
            }
            set
            {
                CenterCursor();
                GameWindow.actualGameWindow.Invoke(new Action(() =>
                {
                    if (value == true) Cursor.Hide();
                    else Cursor.Show();
                }));

                lockCursor = value;
            }
        }
        public static Point Position
        {
            get
            {
                System.Drawing.Point p = new System.Drawing.Point(0, 0);
                GameWindow.actualGameWindow.Invoke(
                    new Action(() =>
                    {
                        p = GameWindow.actualGameWindow.PointToClient(System.Windows.Forms.Cursor.Position);
                    })
                );

                return new Point(p.X, p.Y);
            }
        }
        public static Point GlobalPosition
        {
            get
            {
                return new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            }
        }
        private static System.Drawing.Point screenCenter
        {
            get
            {
                return new System.Drawing.Point(
                    Screen.PrimaryScreen.Bounds.Width / 2,
                    Screen.PrimaryScreen.Bounds.Height / 2
                );
            }
        }

        public static double Get(MouseAxis a)
        {
            switch (a)
            {
                case MouseAxis.X:
                    return MouseAxesValues.X;
                case MouseAxis.Y:
                    return MouseAxesValues.Y;
                case MouseAxis.Wheel:
                    return MouseAxesValues.Wheel;
                default:
                    throw new AxisException("You cannot find value for null MouseAxis");
            }
        }
        private static void CountValues()
        {
            if (lockCursor)
            {
                MouseAxesValues.X = GlobalPosition.X - screenCenter.X;
                MouseAxesValues.Y = GlobalPosition.Y - screenCenter.Y;
                CenterCursor();
            }
            else
            {
                MouseAxesValues.X = 0;
                MouseAxesValues.Y = 0;
            }
        }
        private static void CenterCursor()
        {
            System.Windows.Forms.Cursor.Position = screenCenter;

        }

        private static bool CheckExists(MouseButtons k, List<MouseButtons> where)
        {
            foreach (MouseButtons k1 in where)
            {
                if (k1 == k) return true;
            }
            return false;
        }
        public static void SetMouseButtonState(System.Windows.Forms.MouseEventArgs a, bool pressed)
        {

            if (pressed)
            {
                if (!CheckExists(a.Button, PressedButtons))
                    PressedButtons.Add(a.Button);
                if (!CheckExists(a.Button, ClickedButtons))
                    ClickedButtons.Add(a.Button);
            }
            else
            {
                if (CheckExists(a.Button, PressedButtons))
                    PressedButtons.Remove(a.Button);
                if (!CheckExists(a.Button, ClickedButtons))
                    ClickedButtons.Remove(a.Button);
            }
        }
        public static bool ButtonPressed(MouseButtons k)
        {
            return CheckExists(k, PressedButtons);
        }
        public static bool ButtonClick(MouseButtons k)
        {
            bool check = CheckExists(k, ClickedButtons);
            if (check) ClickedButtons.Remove(k);
            return check;
        }

        public static void OnStart()
        {
            CenterCursor();
        }
        public static void OnTick()
        {
            CountValues();
        }
    }
}
