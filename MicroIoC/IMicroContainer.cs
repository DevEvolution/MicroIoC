using System;

namespace MicroIoC
{
    public interface IMicroContainer
    {
        void RegisterTransient(Type ltype, Type rtype);

        void RegisterTransient<TLeft, TRight>();

        void RegisterSingleton(Type ltype, Type rtype);

        void RegisterSingleton<TLeft, TRight>();

        object Get(Type ltype);

        TLeft Get<TLeft>();

        bool IsRegistered(Type ltype);
    }
}
