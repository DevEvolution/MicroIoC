using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestContainer.Impl
{
    class FakeAlienIntelligenceConnection : IFakeConnection
    {
        public void Connect()
        {
            Console.WriteLine("Связь с искусственным разумом установлена");
        }
    }
}
