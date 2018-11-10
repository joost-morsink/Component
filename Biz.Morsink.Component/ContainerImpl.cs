using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Morsink.Component
{
    class ContainerImpl : IContainer
    {
        private readonly object[] components;
        private Dictionary<Type, object[]> ofType;

        public ContainerImpl(IEnumerable<object> components)
        {
            this.components = components.ToArray();
            ofType = new Dictionary<Type, object[]>();
        }

        public IEnumerable<object> GetAll(Type t)
        {
            var indirect = t == typeof(IContainer) ? Enumerable.Empty<object>() : GetAll<IContainer>().SelectMany(cont => cont.GetAll(t));

            if (ofType.TryGetValue(t, out var result))
                return result.Concat(indirect);
            else
            {
                ofType[t] = result = components.Where(c => t.IsAssignableFrom(c.GetType())).ToArray();
                return result.Concat(indirect);
            }
        }

        public IEnumerable<T> GetAll<T>()
            => GetAll(typeof(T)).Cast<T>();
    }

}
