using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestContainer.Impl
{
    internal class FakeCosmosConnection : IFakeConnection
    {
        public void Connect()
        {
            Console.WriteLine("Связь с космосом установлена");
        }
    }
}
