using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class _2DobjectResizable : _2Dobject
    {
        public Point Size { get; protected set; }

        public void SetSize(Point size)
        {
            this.Size = size;
        }
        public void SetSize(double X, double Y)
        {
            this.Size = new Point(X, Y);
        }
    }
}
