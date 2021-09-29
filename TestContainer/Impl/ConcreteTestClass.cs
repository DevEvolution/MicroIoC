using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestContainer.Impl
{
    internal class ConcreteTestClass<T> : BaseTestClass<T> where T : IFakeConnection
    {
        //public IFakeConnection Connection { get; set; }

        //public ConcreteTestClass(IFakeConnection connection, int a)
        //{
        //    this.Connection = connection;
        //}

        public override string Text => "concrete";
    }
}
