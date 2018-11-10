using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Morsink.Component
{
    class ContainerImplConcurrent : IContainer
    {
        private readonly object[] components;
        private ConcurrentDictionary<Type, object[]> ofType;

        public ContainerImplConcurrent(IEnumerable<object> components)
        {
            this.components = components.ToArray();
            ofType = new ConcurrentDictionary<Type, object[]>();
        }

        public IEnumerable<object> GetAll(Type t)
        {
            var indirect = t == typeof(IContainer) ? Enumerable.Empty<object>() : GetAll<IContainer>().SelectMany(cont => cont.GetAll(t));
            return ofType.GetOrAdd(t, ty => components.Where(c => ty.IsAssignableFrom(c.GetType())).ToArray()).Concat(indirect);
        }

        public IEnumerable<T> GetAll<T>()
            => GetAll(typeof(T)).Cast<T>();
    }

}
