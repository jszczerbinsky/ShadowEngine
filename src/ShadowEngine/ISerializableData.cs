using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowEngine
{
    public interface ISerializableData
    {
        void BeforeSerialization();
        void AfterDeserialization();
    }
}
