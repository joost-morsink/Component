using System;
using System.Collections.Generic;

namespace Biz.Morsink.Component
{
    public interface IContainer
    {
        IEnumerable<object> GetAll(Type type);
        IEnumerable<T> GetAll<T>();
    }

}
