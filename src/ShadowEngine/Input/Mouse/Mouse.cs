﻿using ShadowEngine.Objects.Parameters;
using ShadowEngine.Exceptions;
using ShadowEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace ShadowEngine.Input.Mouse
{
    /// <summary>
    /// Mouse events class.
    /// </summary>
    public static class Mouse
    {
        private static Collection<MouseButtons> PressedButtons = new Collection<MouseButtons>();
        private static Collection<MouseButtons> ClickedButtons = new Collection<MouseButtons>();

        private static bool lockCursor = false;

        /// <summary>
        /// If true - cursor is locked and hidden.
        /// if false - cursor is in normal mode.
        /// </summary>
        public static bool LockCursor
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

        /// <summary>
        /// Gets current mouse position relative to game window.
        /// </summary>
        public static Vector2D Position
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

                return new Vector2D(p.X, p.Y);
            }
        }

        /// <summary>
        /// Gets current mouse position on screen.
        /// </summary>
        public static Vector2D GlobalPosition
        {
            get
            {
                return new Vector2D(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        /// <summary>
        /// Gets point in game, that mouse is pointing on
        /// </summary>
        public static Vector2D GamePosition
        {
            get
            {
                Vector2D p = Position;
                p.X += Camera.Default.Position.X - Camera.Default.GetRealSize().Width / 2;
                p.Y += Camera.Default.Position.Y - Camera.Default.GetRealSize().Height / 2;
                return p;
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

        /// <summary>
        /// returns mouse axis value.
        /// </summary>
        public static float Get(MouseAxis a)
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
                    Exception ex = new AxisException("MouseAxis is null", new NullReferenceException());
                    Log.Exception(ex);
                    throw ex;
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

        private static bool CheckExists(MouseButtons k, Collection<MouseButtons> where)
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
                if (CheckExists(a.Button, ClickedButtons))
                    ClickedButtons.Remove(a.Button);
            }
        }

        /// <summary>
        /// Returns true if mouse button is pressed.
        /// </summary>
        public static bool ButtonPressed(MouseButtons k)
        {
            return CheckExists(k, PressedButtons);
        }

        /// <summary>
        /// Returns true when mouse button is pushed down.
        /// </summary>
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
