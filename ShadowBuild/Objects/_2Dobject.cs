using System;
using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class _2Dobject
    {
        public Point Position { get; protected set; }

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

    }
}
