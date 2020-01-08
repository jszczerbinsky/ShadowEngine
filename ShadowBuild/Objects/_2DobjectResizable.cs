using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class _2DobjectResizable : _2Dobject
    {
        public Size Size { get; protected set; }

        public void SetSize(Size size)
        {
            this.Size = size;
        }
        public void SetSize(double X, double Y)
        {
            this.Size = new Size(X, Y);
        }
    }
}
