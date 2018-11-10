using System.Linq;
using Xunit;

namespace Biz.Morsink.Component.Test
{
    public class AwarenessTest
    {
        void Test(IContainerBuilder builder)
        {
            builder = builder
                .Add(new Person("Joost", "Morsink"))
                .Add(new PlayerStats(maxHealth: 50, strength: 10, stamina: 10, intelligence: 20, dexterity: 20))
                .Add(new SwordOfStrength(3))
                .Add(new RingOfTheMind(2));
            var joost = builder.Build();

            Assert.Equal(50, joost.On<PlayerStats>().Execute(st => st.BaseMaxHealth).First());
            Assert.Equal(50, joost.On<PlayerStats>().Execute(st => st.MaxHealth).First());

            Assert.Equal(10, joost.On<PlayerStats>().Execute(st => st.BaseStrength).First());
            Assert.Equal(13, joost.On<PlayerStats>().Execute(st => st.Strength).First());

            Assert.Equal(10, joost.On<PlayerStats>().Execute(st => st.BaseStamina).First());
            Assert.Equal(10, joost.On<PlayerStats>().Execute(st => st.Stamina).First());

            Assert.Equal(20, joost.On<PlayerStats>().Execute(st => st.BaseIntelligence).First());
            Assert.Equal(22, joost.On<PlayerStats>().Execute(st => st.Intelligence).First());

            Assert.Equal(20, joost.On<PlayerStats>().Execute(st => st.BaseDexterity).First());
            Assert.Equal(20, joost.On<PlayerStats>().Execute(st => st.Dexterity).First());
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