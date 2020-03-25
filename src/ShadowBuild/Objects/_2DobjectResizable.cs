using System.Windows;

namespace ShadowBuild.Objects
{
    public abstract class _2DobjectResizable : _2Dobject
    {
        public Size BaseSize = new Size(200, 200);
        public Size SizeMultipler = new Size(1, 1);

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
    }
}
