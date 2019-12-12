using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects
{
    public static class Collision
    {
        public static bool Check(GameObject obj1, GameObject obj2)
        {
            _2Dsize start1 = obj1.GetStartPosition();
            _2Dsize start2 = obj2.GetStartPosition();

            _2Dsize end1 = _2Dsize.Add(obj1.GetStartPosition(), new _2Dsize( obj1.ActualTexture.GetSize().X * obj1.Size.X, obj1.ActualTexture.GetSize().Y*obj1.Size.Y));
            _2Dsize end2 = _2Dsize.Add(obj2.GetStartPosition(), new _2Dsize( obj2.ActualTexture.GetSize().X * obj2.Size.X, obj2.ActualTexture.GetSize().Y*obj2.Size.Y));

            if (
             (end1.X > start2.X
             && end1.X < end2.X
             && end1.Y > start2.Y
             && end1.Y < end2.Y)
             ||
             (end1.X > start2.X
             && end1.X < end2.X
             && start1.Y > start2.Y
             && start1.Y < end2.Y)
             ||
             (start1.X > start2.X
             && start1.X < end2.X
             && end1.Y > start2.Y
             && end1.Y < end2.Y)
             ||
             (start1.X > start2.X
             && start1.X < end2.X
             && start1.Y > start2.Y
             && start1.Y < end2.Y)
             )
             return true;
            return false;
        }
    }
}
