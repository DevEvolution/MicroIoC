using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestContainer
{
    internal class BaseTestClass<T> where T: IFakeConnection
    {
        public T Connection {  get; set; }

        public virtual string Text { get; } = "base";

        public override string ToString()
        {
            return this.Text;
        }
    }
}
