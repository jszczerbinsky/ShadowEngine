using System;
using System.Windows;

namespace ShadowEngine.Objects
{

    /// <summary>
    /// 2D objects class.
    /// </summary>
    [Serializable]
    public abstract class _2Dobject
    {

        /// <value>Gets position of an object</value>
        public Point Position { get; protected set; }

        /// <value>Gets base size of an object</value>
        public Size BaseSize { get; protected set; } = new Size(200, 200);

        /// <value>Gets size multipler of an object</value>
        public Size SizeMultipler { get; protected set; } = new Size(1, 1);

        /// <value>Gets rotation of an object</value>
        public float Rotation { get; protected set; }

        #region position

        /// <summary>
        /// Sets a position.
        /// </summary>
        public virtual void SetPosition(Point position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Sets a position.
        /// </summary>
        public virtual void SetPosition(double X, double Y)
        {
            this.Position = new Point(X, Y);
        }

        /// <summary>
        /// Moves object.
        /// </summary>
        public virtual void Move(double X, double Y)
        {
            this.Position = new Point(X + this.Position.X, Y + this.Position.Y);
        }

        /// <summary>
        /// Moves object taking rotation into account.
        /// </summary>
        public virtual void MoveLocal(double X, double Y)
        {
            double rot = (Math.PI / 180) * Rotation;

            double moveX = Math.Sin(rot) * Y + Math.Cos(rot) * X;
            double moveY = Math.Sin(rot) * X + Math.Cos(rot) * Y;

            Move(moveX, -moveY);
        }

        /// <summary>
        /// Moves to point
        /// </summary>
        public virtual void MoveTo(Point p, double speed)
        {
            double xDistance = p.X - this.Position.X;
            double yDistance = p.Y - this.Position.Y;

            double distance = DistanceFrom(p);

            double xNormal = xDistance / distance;
            double yNormal = yDistance / distance;

            Move(xNormal * speed, yNormal * speed);
        }

        /// <summary>
        /// Gets distance from another object.
        /// </summary>
        public double DistanceFrom(_2Dobject o)
        {
            double x = Math.Abs(o.Position.X - this.Position.X);
            double y = Math.Abs(o.Position.Y - this.Position.Y);

            return Math.Sqrt(y * y + x * x);
        }

        /// <summary>
        /// Gets distance from a point.
        /// </summary>
        public double DistanceFrom(Point p)
        {
            double x = Math.Abs(p.X - this.Position.X);
            double y = Math.Abs(p.Y - this.Position.Y);

            return Math.Sqrt(y * y + x * x);
        }


        #endregion

        #region size

        /// <summary>
        /// Gets real size of an object.
        /// </summary>
        /// <returns>
        /// BaseSize * SizeMultipler
        /// </returns>
        public virtual Size GetRealSize()
        {
            return new Size(this.BaseSize.Width * this.SizeMultipler.Width, this.BaseSize.Height * this.SizeMultipler.Height);
        }

        /// <summary>
        /// Sets base size of an object.
        /// </summary>
        public void ChangeBaseSize(Size size)
        {
            this.BaseSize = size;
        }

        /// <summary>
        /// Sets base size of an object.
        /// </summary>
        public void ChangeBaseSize(double X, double Y)
        {
            this.BaseSize = new Size(X, Y);
        }

        /// <summary>
        /// Sets size multipler of an object.
        /// </summary>
        public void SetSize(Size size)
        {
            this.SizeMultipler = size;
        }

        /// <summary>
        /// Sets size multipler of an object.
        /// </summary>
        public void SetSize(double X, double Y)
        {
            this.SizeMultipler = new Size(X, Y);
        }

        #endregion

        #region rotation

        /// <summary>
        /// Sets rotation of an object.
        /// </summary>
        public void SetRotation(float rotation)
        {
            Rotation = rotation % 360;
        }

        /// <summary>
        /// Sets rotation to face a point
        /// </summary>
        public virtual void RotateTo(Point p)
        {
            float rot = (float)((180 / Math.PI) * Math.Atan2(p.Y - this.Position.Y, p.X - this.Position.X));
            if (rot < 0)
            {
                rot = 360 - (-rot);
            }
            rot += 90;
            Rotation = rot;
        }

        /// <summary>
        /// Rotates object.
        /// </summary>
        /// <param name="rotation">rotation angle</param>
        public virtual void Rotate(float rotation)
        {
            SetRotation(this.Rotation + rotation);
        }

        #endregion
    }
}
