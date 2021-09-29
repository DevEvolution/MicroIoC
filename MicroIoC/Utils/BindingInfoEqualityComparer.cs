using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC.Utils
{
    internal class BindingInfoEqualityComparer : IEqualityComparer<BindingInfo>
    {
        public bool Equals(BindingInfo x, BindingInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return Type.Equals(x.Type, y.Type) && x.Name == y.Name;
        }

        public int GetHashCode(BindingInfo obj)
        {
            return HashCode.Combine(obj.Type, obj.Name);
        }
    }
}
