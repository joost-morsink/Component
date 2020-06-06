using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Biz.Morsink.Component
{
    public struct ContainerBuilder : IContainerBuilder
    {
        private readonly Func<IEnumerable<object>, IContainer> constructor;
        private readonly ImmutableStack<object> components;

        internal ContainerBuilder(Func<IEnumerable<object>, IContainer> constructor) : this(constructor, ImmutableStack<object>.Empty) { }
        private ContainerBuilder(Func<IEnumerable<object>, IContainer> constructor, ImmutableStack<object> components)
        {
            this.constructor = constructor;
            this.components = components;
        }
        public ContainerBuilder Add(object component)
        {
            return new ContainerBuilder(constructor, components.Push(component));
        }
        public ContainerBuilder AddRange(IEnumerable<object> comps)
        {
            var cs = components;
            foreach (var c in comps)
                cs = cs.Push(c);
            return new ContainerBuilder(constructor, cs);
        }
        IContainerBuilder IContainerBuilder.Add(object component)
            => Add(component);

        public IContainer Build()
        {
            var res = constructor(components.Reverse());
            foreach (var aware in components.OfType<IContainerAware>())
                aware.SetContainer(res);
            return res;
        }
    }

}
