using System;

namespace MicroIoC
{
    public interface IMicroContainer
    {
        void RegisterTransient(Type ltype, Type rtype, string name = null);

        void RegisterTransient<TLeft, TRight>(string name = null);

        void RegisterSingleton(Type ltype, Type rtype, string name = null);

        void RegisterSingleton<TLeft, TRight>(string name = null);

        object Get(Type ltype, string name = null);

        TLeft Get<TLeft>(string name = null);

        bool IsRegistered(Type ltype, string name = null);
    }
}
