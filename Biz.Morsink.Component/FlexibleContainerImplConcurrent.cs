using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Morsink.Component
{
    class FlexibleContainerImplConcurrent : IFlexibleContainer
    {
        private readonly ConcurrentDictionary<object, object> components;
        private readonly ConcurrentDictionary<Type, object[]> ofType;
        public FlexibleContainerImplConcurrent()
        {
            components = new ConcurrentDictionary<object, object>(ReferenceEqualityComparer.Instance);
            ofType = new ConcurrentDictionary<Type, object[]>();
        }

        public bool Add(object component)
        {
            if (components.TryAdd(component, component))
            {
                (component as IContainerAware)?.SetContainer(this);
                ofType.Clear();
                return true;
            }
            else
                return false;
        }
        public IEnumerable<object> GetAll(Type t)
        {
            var indirect = t == typeof(IContainer) ? Enumerable.Empty<object>() : GetAll<IContainer>().SelectMany(cont => cont.GetAll(t));
            return ofType.GetOrAdd(t, ty => components.Keys.Where(c => ty.IsAssignableFrom(c.GetType())).ToArray()).Concat(indirect);
        }
        public IEnumerable<T> GetAll<T>()
            => GetAll(typeof(T)).Cast<T>();

        public bool Remove(object component)
        {
            if (components.TryRemove(component, out var _))
            {
                ofType.Clear();
                return true;
            }
            else
                return false;
        }
    }

}
