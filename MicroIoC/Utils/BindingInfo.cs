using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC.Utils
{
    public class BindingInfo
    {
        public Type Type {  get; set; }
        public string Name {  get; set; }

        public static implicit operator BindingInfo(Type type)
        {
            return new BindingInfo { Type = type, Name = null };
        }
    }
}
