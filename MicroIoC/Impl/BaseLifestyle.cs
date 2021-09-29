using System;
using System.Linq;

namespace MicroIoC.Impl
{
    internal abstract class BaseLifestyle : ILifestyle
    {
        protected IMicroContainer container;
        protected Type type;

        public BaseLifestyle(IMicroContainer container, Type rtype)
        {
            this.container = container;
            this.type = rtype;
        }

        public abstract object GetInstance(params Type[] genericParameterTypes);

        protected object CreateInstance(Type type, params Type[] genericParameterTypes)
        {
            var registeredType = type;

            if (type.IsGenericTypeDefinition && genericParameterTypes.Length > 0)
            {
                registeredType = type.MakeGenericType(genericParameterTypes);
            }
            
            foreach (var constructorInfo in registeredType.GetConstructors())
            {
                var paramTypeList = constructorInfo
                    .GetParameters()
                    .Select(p => p.ParameterType);

                var propertyList = registeredType.GetProperties()
                    .Where(x => container.IsRegistered(x.PropertyType))
                    .Where(x => x.CanWrite);

                // constructor injection
                var instance = constructorInfo.Invoke(
                    paramTypeList.Select(paramType =>
                    {
                        var injectionValue = container.Get(paramType);
                        return injectionValue;
                    })
                    .ToArray());

                // property injection
                foreach (var propertyInfo in propertyList)
                {
                    var injectionValue = container.Get(propertyInfo.PropertyType);
                    propertyInfo.SetValue(instance, injectionValue);
                }

                return instance;
            }

            return null;
        }
    }
}
