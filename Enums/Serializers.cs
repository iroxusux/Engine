using Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Enums
{
    [Flags]
    public enum Serializers
    {
        None = 0,
        Binary = 1,
        Json = 2,
        Xml = 4,
    }
}
