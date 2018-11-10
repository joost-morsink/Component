namespace Biz.Morsink.Component.Test
{
    public class PlayerStats : IContainerAware
    {
        public PlayerStats(int maxHealth, int strength, int stamina, int intelligence, int dexterity)
        {
            BaseMaxHealth = maxHealth;
            BaseStrength = strength;
            BaseStamina = stamina;
            BaseIntelligence = intelligence;
            BaseDexterity = dexterity;
        }
        private IContainer container;
        public int BaseMaxHealth { get; }
        public int BaseStrength { get; }
        public int BaseStamina { get; }
        public int BaseIntelligence { get; }
        public int BaseDexterity { get; }
        public void SetContainer(IContainer container)
        {
            this.container = container;
        }
        public int MaxHealth => container.On<IStatModifier>().Execute(BaseMaxHealth, (v, m) => v + m.MaxHealthModifier);
        public int Strength => container.On<IStatModifier>().Execute(BaseStrength, (v, m) => v + m.StrengthModifier);
        public int Stamina => container.On<IStatModifier>().Execute(BaseStamina, (v, m) => v + m.StaminaModifier);
        public int Intelligence => container.On<IStatModifier>().Execute(BaseIntelligence, (v, m) => v + m.IntelligenceModifier);
        public int Dexterity => container.On<IStatModifier>().Execute(BaseDexterity, (v, m) => v + m.DexterityModifier);
    }

}