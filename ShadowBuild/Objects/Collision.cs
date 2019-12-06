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
        public static bool check(GameObject obj1, GameObject obj2)
        {
            _2Dsize start1 = obj1.startPosition;
            _2Dsize start2 = obj2.startPosition;

            _2Dsize end1 = _2Dsize.add(obj1.startPosition, new _2Dsize( obj1.actualTexture.getSize().X * obj1.size.X, obj1.actualTexture.getSize().Y*obj1.size.Y));
            _2Dsize end2 = _2Dsize.add(obj2.startPosition, new _2Dsize( obj2.actualTexture.getSize().X * obj2.size.X, obj2.actualTexture.getSize().Y*obj2.size.Y));

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
