using System;
using System.Collections.Generic;
using System.Linq;

namespace Biz.Morsink.Component.Test
{
    public class WeaponSlot : IContainer
    {
        public object Weapon { get; set; }
        public IEnumerable<object> GetAll(Type type)
            => new object[] { Weapon }.Where(w => w != null && type.IsAssignableFrom(w.GetType()));

        public IEnumerable<T> GetAll<T>()
            => new[] { Weapon }.OfType<T>();
    }
}