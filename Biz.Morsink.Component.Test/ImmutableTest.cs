using System;
using System.Linq;
using Xunit;

namespace Biz.Morsink.Component.Test
{

    public class ImmutableTest
    {
        public ImmutableTest()
        {
        }
        [Fact]
        public void Append()
        {
            var c1 = Container.BuildImmutable().Add(1).Add(2).Add(3).Build();
            var c2 = Container.BuildImmutable().Add(4).Add(5).Add(6).Build();
            Assert.Equal(3, c1.GetAll<int>().Count());
            Assert.Equal(3, c2.GetAll<int>().Count());
            Assert.Equal(6, c1.Append(c2).GetAll<int>().Count());
        }
    }
}
