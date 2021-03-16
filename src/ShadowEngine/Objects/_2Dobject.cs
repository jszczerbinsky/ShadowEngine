using ShadowEngine.Objects.Parameters;
using System;

namespace ShadowEngine.Objects
{

    /// <summary>
    /// 2D objects class.
    /// </summary>
    [Serializable]
    public abstract class _2Dobject
    {

        /// <value>Gets position of an object</value>
        public Vector2D Position { get; protected set; }

        /// <value>Gets base size of an object</value>
        public Size Size { get; protected set; } = new Size(200, 200);

        /// <value>Gets size multipler of an object</value>
        public Size Scale { get; protected set; } = new Size(1, 1);

        /// <value>Gets rotation of an object</value>
        public float Rotation { get; protected set; }

        #region position

        /// <summary>
        /// Sets a position.
        /// </summary>
        public virtual void SetPosition(Vector2D position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Sets a position.
        /// </summary>
        public virtual void SetPosition(float X, float Y)
        {
            this.Position = new Vector2D(X, Y);
        }

        /// <summary>
        /// Moves object.
        /// </summary>
        public virtual void Move(float X, float Y)
        {
            this.Position += new Vector2D(X, Y);
        }

        /// <summary>
        /// Moves object taking rotation into account.
        /// </summary>
        public virtual void MoveLocal(float X, float Y)
        {
            double rot = (Math.PI / 180) * Rotation;

            float moveX = (float)(Math.Sin(rot) * Y + Math.Cos(rot) * X);
            float moveY = (float)(Math.Sin(rot) * X + Math.Cos(rot) * Y);

            Move(moveX, -moveY);
        }

        /// <summary>
        /// Moves to point
        /// </summary>
        public virtual void MoveTo(Vector2D p, float speed)
        {
            float xDistance = p.X - this.Position.X;
            float yDistance = p.Y - this.Position.Y;

            float distance = DistanceFrom(p);

            float xNormal = xDistance / distance;
            float yNormal = yDistance / distance;

            Move(xNormal * speed, yNormal * speed);
        }

        /// <summary>
        /// Gets distance from another object.
        /// </summary>
        public float DistanceFrom(_2Dobject o)
        {
            float x = Math.Abs(o.Position.X - this.Position.X);
            float y = Math.Abs(o.Position.Y - this.Position.Y);

            return (float)Math.Sqrt(y * y + x * x);
        }

        /// <summary>
        /// Gets distance from a point.
        /// </summary>
        public float DistanceFrom(Vector2D p)
        {
            float x = Math.Abs(p.X - this.Position.X);
            float y = Math.Abs(p.Y - this.Position.Y);

            return (float)Math.Sqrt(y * y + x * x);
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
            return Size * Scale;
        }

        /// <summary>
        /// Sets base size of an object.
        /// </summary>
        public void SetSize(Size size)
        {
            this.Size = size;
        }

        /// <summary>
        /// Sets base size of an object.
        /// </summary>
        public void SetSize(float X, float Y)
        {
            this.Size = new Size(X, Y);
        }

        /// <summary>
        /// Sets size multipler of an object.
        /// </summary>
        public void SetScale(Size size)
        {
            this.Scale = size;
        }

        /// <summary>
        /// Sets size multipler of an object.
        /// </summary>
        public void SetScale(float X, float Y)
        {
            this.Scale = new Size(X, Y);
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
        public virtual void RotateTo(Vector2D p)
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
