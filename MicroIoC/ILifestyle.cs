using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC
{
    public interface ILifestyle
    {
        object GetInstance(params Type[] genericParameterTypes);
    }
}
