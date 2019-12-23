using ShadowBuild.Objects.Dimensions;

namespace ShadowBuild.Objects
{
    public abstract class _2DobjectResizable : _2Dobject
    {
        public _2Dsize Size { get; protected set; }

        public void SetSize(_2Dsize size)
        {
            this.Size = size;
        }
        public void SetSize(double X, double Y)
        {
            this.Size = new _2Dsize(X, Y);
        }
    }
}
