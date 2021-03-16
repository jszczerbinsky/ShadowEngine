using ShadowEngine;
using System;

namespace ShadowEngine.Objects.Parameters
{
    [Serializable]
    public struct Size
    {
        public float Width;
        public float Height;

        public Size(float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public static Size operator +(Size v1, Size v2) =>
            new Size(v1.Width + v2.Height, v1.Width + v2.Height);

        public static Size operator -(Size v1, Size v2) =>
            new Size(v1.Width - v2.Width, v1.Height - v2.Height);

        public static Size operator *(Size v1, Size v2) =>
            new Size(v1.Width * v2.Width, v1.Height * v2.Height);

        public static Size operator /(Size v1, Size v2)
        {
            if (v2.Width == 0 || v2.Height == 0)
            {
                Exception ex = new DivideByZeroException();
                Log.Exception(ex);
                throw ex;
            }
            return new Size(v1.Width / v2.Width, v1.Height / v2.Height);
        }
    }
}
