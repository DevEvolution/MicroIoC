using MicroIoC.Extensions;
using MicroIoC.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC
{
    public class MicroContainer : IMicroContainer
    {
        protected internal IDictionary<Type, ILifestyle> typeBindingDict;

        public MicroContainer()
        {
            this.typeBindingDict = new Dictionary<Type, ILifestyle>() 
            { 
                [typeof(IMicroContainer)] = new SingletonLifestyle(this, typeof(MicroContainer)) 
            };
        }

        #region Get
        public object Get(Type ltype)
        {
            if (typeBindingDict.ContainsKey(ltype))
            {
                var lifestyle = typeBindingDict[ltype];
                return lifestyle.GetInstance();
            }

            // I<T>
            if (ltype.IsGenericType)
            {
                // I<>
                var definition = ltype.GetGenericTypeDefinition(); 
                
                if (typeBindingDict.ContainsKey(definition))
                {
                    var lifestyle = typeBindingDict[definition];
                    return lifestyle.GetInstance(ltype.GetGenericArguments());
                }

            }

            return ltype.GetDefaultValue();
        }

        public TLeft Get<TLeft>()
        {
            return (TLeft)this.Get(typeof(TLeft));
        }
        #endregion

        #region RegisterTransient
        public void RegisterTransient(Type ltype, Type rtype)
        {
            this.AssertRtypeIsLtype(ltype, rtype);
            this.Register(ltype, new TransientLifestyle(this, rtype));
        }

        public void RegisterTransient<TLeft, TRight>()
        {
            this.RegisterTransient(typeof(TLeft), typeof(TRight));
        }
        #endregion

        #region RegisterSingleton
        public void RegisterSingleton(Type ltype, Type rtype)
        {
            this.AssertRtypeIsLtype(ltype, rtype);
            this.Register(ltype, new SingletonLifestyle(this, rtype));
        }

        public void RegisterSingleton<TLeft, TRight>()
        {
            this.RegisterSingleton(typeof(TLeft), typeof(TRight));
        }
        #endregion


        private void Register(Type ltype, ILifestyle lifestyle)
        {
            typeBindingDict.Add(ltype, lifestyle);
        }

        private void AssertRtypeIsLtype(Type ltype, Type rtype)
        {
            //if (!ltype.IsAssignableFrom(rtype)) 
            //{
            //    throw new InvalidOperationException($"Type {ltype.FullName} is not assignable from type {rtype.FullName}");
            //}
        }

        public bool IsRegistered(Type ltype)
        {
            return typeBindingDict.ContainsKey(ltype);
        }
    }
}
