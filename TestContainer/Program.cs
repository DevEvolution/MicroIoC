using MicroIoC;
using System;
using TestContainer.Impl;

namespace TestContainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = new MicroContainer();

            container.RegisterSingleton(typeof(BaseTestClass<>), typeof(ConcreteTestClass<>));
            container.RegisterTransient<IFakeConnection, FakeCosmosConnection>();
            container.RegisterTransient<IFakeConnection, FakeAlienIntelligenceConnection>("alien");

            var ct1 = container.Get<BaseTestClass<IFakeConnection>>();
            var ct2 = container.Get<BaseTestClass<IFakeConnection>>();
            var conn = container.Get<IFakeConnection>();
            var conn2 = container.Get<IFakeConnection>("alien");

            Console.WriteLine(object.Equals(ct1, ct2));
            Console.WriteLine(object.ReferenceEquals(ct1, ct2));
        }
    }
}
