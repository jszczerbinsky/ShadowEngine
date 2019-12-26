using ShadowBuild.Exceptions;
using ShadowBuild.Rendering;
using System;
using System.Windows;
using System.Windows.Forms;

namespace ShadowBuild.Input.Mouse
{
    public static class Mouse
    {
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
                        p = GameWindow.actualGameWindow.PointToClient(Cursor.Position);
                    })
                );

                return new Point(p.X, p.Y);
            }
        }
        public static Point GlobalPosition
        {
            get
            {
                return new Point(Cursor.Position.X, Cursor.Position.Y);
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
            MouseAxesValues.X = GlobalPosition.X - screenCenter.X;
            MouseAxesValues.Y = GlobalPosition.Y - screenCenter.Y;
            CenterCursor();
        }
        private static void CenterCursor()
        {
            Cursor.Position = screenCenter;

        }
        public static void OnStart()
        {
            CenterCursor();
        }
        public static void OnTick()
        {
            if (lockCursor)
                CountValues();
        }
    }
}
