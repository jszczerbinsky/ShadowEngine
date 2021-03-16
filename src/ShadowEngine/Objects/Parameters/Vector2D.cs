using ShadowEngine;
using System;

namespace ShadowEngine.Objects.Parameters
{
    [Serializable]
    public struct Vector2D
    {
        public float X;
        public float Y;

        public Vector2D(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2D operator -(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X - v2.X, v1.Y - v2.Y);

        public static Vector2D operator *(Vector2D v1, Vector2D v2) =>
            new Vector2D(v1.X * v2.X, v1.Y * v2.Y);

        public static Vector2D operator /(Vector2D v1, Vector2D v2)
        {
            if (v2.X == 0 || v2.Y == 0)
            {
                Exception ex = new DivideByZeroException();
                Log.Exception(ex);
                throw ex;
            }
            return new Vector2D(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, Size v2) =>
            new Vector2D(v1.X + v2.Width, v1.Y + v2.Height);

        public static Vector2D operator -(Vector2D v1, Size v2) =>
            new Vector2D(v1.X - v2.Width, v1.Y - v2.Height);

        public static Vector2D operator *(Vector2D v1, Size v2) =>
            new Vector2D(v1.X * v2.Width, v1.Y * v2.Height);

        public static Vector2D operator /(Vector2D v1, Size v2)
        {
            if (v2.Width == 0 || v2.Height == 0)
            {
                Exception ex = new DivideByZeroException();
                Log.Exception(ex);
                throw ex;
            }
            return new Vector2D(v1.X / v2.Width, v1.Y / v2.Height);
        }
    }
}
