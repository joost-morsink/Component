using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Biz.Morsink.Component
{
    public static class Container
    {
        public static ContainerBuilder Build()
        {
            return new ContainerBuilder(components => new ContainerImpl(components));
        }
        public static ContainerBuilder BuildThreadSafe()
        {
            return new ContainerBuilder(components => new ContainerImplConcurrent(components));
        }
        public static ContainerBuilder BuildFlexible()
        {
            return new ContainerBuilder(components =>
            {
                var flex = CreateFlexible();
                foreach (var component in components)
                    flex.Add(component);
                return flex;
            });
        }
        public static ContainerBuilder BuildImmutable()
        {
            return new ContainerBuilder(components => new ImmutableContainer(components.ToImmutableList()));
        }
        public static IContainer Empty { get; } = new ImmutableContainer(ImmutableList<object>.Empty);

        public static IFlexibleContainer CreateFlexible()
            => new FlexibleContainerImplConcurrent();

        public struct ContainerExecution<T>
        {
            private readonly IContainer container;
            public ContainerExecution(IContainer container)
            {
                this.container = container;
            }
            public R[] Execute<R>(Func<T, R> action)
                => container.GetAll<T>().Select(action).ToArray();
            public R Execute<R>(R seed, Func<R, T, R> action)
                => container.GetAll<T>().Aggregate(seed, action);
            public R Execute<U, R>(Func<T, U> action, R seed, Func<R, U, R> aggregate)
                => container.GetAll<T>().Select(action).Aggregate(seed, aggregate);
            public Task ExecuteAsync(Func<T, Task> action, int parallellism)
            {
                var semaphore = new SemaphoreSlim(parallellism);
                return Task.WhenAll(container.GetAll<T>().Select(async t =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await action(t);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }
            public Task<R[]> ExecuteAsync<R>(Func<T, Task<R>> action, int parallellism)
            {
                var semaphore = new SemaphoreSlim(parallellism);
                return Task.WhenAll(container.GetAll<T>().Select(async t =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        return await action(t);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }
        }
        public static ContainerExecution<T> On<T>(this IContainer container)
            => new ContainerExecution<T>(container);
        public static bool TryGetSingle<T>(this IContainer container, out T res)
        {
            res = default;
            foreach (var entry in container.GetAll<T>().Take(1))
            {
                res = entry;
                return true;
            }
            return false;
        }
        public static T GetOrDefault<T>(this IContainer container)
            => container.TryGetSingle<T>(out var t) ? t : default;

        public static IContainer Append(this IContainer left, IContainer right)
        {
            if (left is ImmutableContainer icleft)
                return icleft.Append(right);
            else
                return new ImmutableContainer(left.GetAll<object>().Concat(right.GetAll<object>()).ToImmutableList());
        }
        public static IContainer AddRange(this IContainer container, IEnumerable<object> objects)
        {
            if (container is ImmutableContainer ic)
                return ic.AddRange(objects);
            else
                return new ImmutableContainer(container.GetAll<object>().Concat(objects).ToImmutableList());
        }
        public static IContainer Add(this IContainer container, object obj)
        {
            if (container is ImmutableContainer ic)
                return ic.Add(obj);
            else
                return new ImmutableContainer(container.GetAll<object>().ToImmutableList().Add(obj));
        }
    }
}
