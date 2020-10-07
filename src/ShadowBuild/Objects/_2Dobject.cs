using System;
using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class _2Dobject
    {
        public Point Position { get; protected set; }

        public Size BaseSize = new Size(200, 200);
        public Size SizeMultipler = new Size(1, 1);

        public float Rotation { get; private set; }

        #region position

        public void SetPosition(Point position)
        {
            this.Position = position;
        }
        public void SetPosition(double X, double Y)
        {
            this.Position = new Point(X, Y);
        }
        public virtual void Move(double X, double Y)
        {
            this.Position = new Point(X + this.Position.X, Y + this.Position.Y);
        }
        public double DistanceFrom(_2Dobject o)
        {
            double x = Math.Abs(o.Position.X - this.Position.X);
            double y = Math.Abs(o.Position.Y - this.Position.Y);

            return Math.Sqrt(y * y + x * x);
        }


        #endregion

        #region size

        public virtual Size GetRealSize()
        {
            return new Size(this.BaseSize.Width * this.SizeMultipler.Width, this.BaseSize.Height * this.SizeMultipler.Height);
        }

        public void ChangeBaseSize(Size size)
        {
            this.BaseSize = size;
        }
        public void ChangeBaseSize(double X, double Y)
        {
            this.BaseSize = new Size(X, Y);
        }

        public void SetSize(Size size)
        {
            this.SizeMultipler = size;
        }
        public void SetSize(double X, double Y)
        {
            this.SizeMultipler = new Size(X, Y);
        }

        #endregion

        #region rotation

        public void SetRotation(float rotation)
        {
            Rotation = rotation % 360;
        }

        public void Rotate(float rotation)
        {
            SetRotation(this.Rotation + rotation);
        }

        #endregion
    }
}
