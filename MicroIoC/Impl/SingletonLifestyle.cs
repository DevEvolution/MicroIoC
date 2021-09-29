using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC.Impl
{
    internal class SingletonLifestyle : BaseLifestyle, ILifestyle
    {
        private object _instance;

        public SingletonLifestyle(IMicroContainer container, Type type)
            : base(container, type)
        {
        }

        public override object GetInstance(params Type[] genericParameterTypes)
        {
            if (this._instance == null)
            {
                this._instance = this.CreateInstance(this.type, genericParameterTypes); // new
            }
            return this._instance;
        }
    }
}