using System;
using System.Windows;

namespace ShadowBuild.Objects
{

    /// <summary>
    /// 2D objects class.
    /// </summary>
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
        public void SetPosition(Point position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Sets a position.
        /// </summary>
        public void SetPosition(double X, double Y)
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
        /// Gets distance from another object.
        /// </summary>
        public double DistanceFrom(_2Dobject o)
        {
            double x = Math.Abs(o.Position.X - this.Position.X);
            double y = Math.Abs(o.Position.Y - this.Position.Y);

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
