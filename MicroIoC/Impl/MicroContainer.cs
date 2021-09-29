using MicroIoC.Extensions;
using MicroIoC.Impl;
using MicroIoC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroIoC
{
    public class MicroContainer : IMicroContainer
    {
        protected internal IDictionary<BindingInfo, ILifestyle> typeBindingDict;

        public MicroContainer()
        {
            this.typeBindingDict = new Dictionary<BindingInfo, ILifestyle>(new BindingInfoEqualityComparer())
            { 
                [typeof(IMicroContainer)] = new SingletonLifestyle(this, typeof(MicroContainer)) 
            };
        }

        #region Get
        public object Get(Type ltype, string name = null)
        {
            var bindingInfo = new BindingInfo { Type = ltype, Name = name };

            if (typeBindingDict.ContainsKey(bindingInfo))
            {
                var lifestyle = typeBindingDict[bindingInfo];
                return lifestyle.GetInstance();
            }

            // I<T>
            if (ltype.IsGenericType)
            {
                // I<>
                var definition = ltype.GetGenericTypeDefinition(); 
                var definitionBinding = new BindingInfo {  Type = definition, Name = name };

                if (typeBindingDict.ContainsKey(definitionBinding))
                {
                    var lifestyle = typeBindingDict[definitionBinding];
                    return lifestyle.GetInstance(ltype.GetGenericArguments());
                }

            }

            return ltype.GetDefaultValue();
        }

        public TLeft Get<TLeft>(string name = null)
        {
            return (TLeft)this.Get(typeof(TLeft), name);
        }
        #endregion

        #region RegisterTransient
        public void RegisterTransient(Type ltype, Type rtype, string name = null)
        {
            this.AssertRtypeIsLtype(ltype, rtype);
            this.Register(ltype, new TransientLifestyle(this, rtype), name);
        }

        public void RegisterTransient<TLeft, TRight>(string name = null)
        {
            this.RegisterTransient(typeof(TLeft), typeof(TRight), name);
        }
        #endregion

        #region RegisterSingleton
        public void RegisterSingleton(Type ltype, Type rtype, string name = null)
        {
            this.AssertRtypeIsLtype(ltype, rtype);
            this.Register(ltype, new SingletonLifestyle(this, rtype), name);
        }

        public void RegisterSingleton<TLeft, TRight>(string name = null)
        {
            this.RegisterSingleton(typeof(TLeft), typeof(TRight), name);
        }
        #endregion


        private void Register(Type ltype, ILifestyle lifestyle, string name = null)
        {
            typeBindingDict.Add(new BindingInfo { Type = ltype, Name = name }, lifestyle);
        }

        private void AssertRtypeIsLtype(Type ltype, Type rtype)
        {
            //if (!ltype.IsAssignableFrom(rtype)) 
            //{
            //    throw new InvalidOperationException($"Type {ltype.FullName} is not assignable from type {rtype.FullName}");
            //}
        }

        public bool IsRegistered(Type ltype, string name = null)
        {
            return typeBindingDict.ContainsKey(new BindingInfo { Type = ltype, Name = name });
        }
    }
}
