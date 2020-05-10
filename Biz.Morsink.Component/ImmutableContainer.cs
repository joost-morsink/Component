using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Biz.Morsink.Component
{
    internal class ImmutableContainer : IContainer
    {
        private readonly ImmutableList<object> _objects;
        private readonly ConcurrentDictionary<Type, object[]> _byType;
        public ImmutableContainer(ImmutableList<object> objects)
        {
            _objects = objects;
            _byType = new ConcurrentDictionary<Type, object[]>();
        }

        public IEnumerable<object> GetAll(Type type)
            => _byType.GetOrAdd(type, t => _objects.Where(t.IsInstanceOfType).ToArray());

        public IEnumerable<T> GetAll<T>()
            => GetAll(typeof(T)).Cast<T>();

        public IContainer Append(IContainer other)
        {
            if (other is ImmutableContainer icother)
                return new ImmutableContainer(_objects.AddRange(icother._objects));
            else
                return new ImmutableContainer(_objects.AddRange(other.GetAll<object>()));
        }
    }
}
