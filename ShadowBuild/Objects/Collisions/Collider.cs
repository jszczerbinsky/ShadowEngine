using ShadowBuild.Objects.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowBuild.Objects.Collisions
{
    public sealed class Collider : _2Dobject
    {
        public ColliderShape shape { get; private set; }
        
    }
}
