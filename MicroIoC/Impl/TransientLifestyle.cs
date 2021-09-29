using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC.Impl
{
    internal class TransientLifestyle : BaseLifestyle, ILifestyle
    {
        public TransientLifestyle(IMicroContainer container, Type rtype)
            : base(container, rtype)
        {
        }

        public override object GetInstance(params Type[] genericParameterTypes)
        {
            return this.CreateInstance(this.type, genericParameterTypes); // new
        }
    }
}
