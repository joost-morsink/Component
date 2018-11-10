using System.Linq;
using Xunit;

namespace Biz.Morsink.Component.Test
{
    public class SimpleTest
    {
        private void Test(IContainerBuilder builder)
        {
            var container = builder.Add(new Person("Joost", "Morsink")).Build();
            var firstNames = container.On<Person>().Execute(p => p.FirstName);
            Assert.Equal("Joost", firstNames.First());
            var lastNames = container.On<Person>().Execute(p => p.LastName);
            Assert.Equal("Morsink", lastNames.First());
        }
        [Fact]
        public void Standard()
        {
            Test(Container.Build());
        }
        [Fact]
        public void ThreadSafe()
        {
            Test(Container.BuildThreadSafe());
        }
        [Fact]
        public void Flexible()
        {
            Test(Container.BuildFlexible());
        }
    }
}