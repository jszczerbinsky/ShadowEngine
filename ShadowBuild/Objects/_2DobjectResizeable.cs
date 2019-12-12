using ShadowBuild.Objects.Dimensions;

namespace ShadowBuild.Objects
{
    public abstract class _2DobjectResizeable : _2Dobject
    {
        public _2Dsize Size { get; protected set; }

        #region size

        public void SetSize(_2Dsize size)
        {
            this.Size = size;
        }
        public void SetSize(double X, double Y)
        {
            this.Size = new _2Dsize(X, Y);
        }
        #endregion
    }
}
