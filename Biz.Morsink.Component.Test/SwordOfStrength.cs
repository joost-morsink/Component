namespace Biz.Morsink.Component.Test
{
    public class SwordOfStrength : IStatModifier
    {
        public SwordOfStrength(int strengthModifier)
        {
            StrengthModifier = strengthModifier;
        }
        public int MaxHealthModifier => 0;
        public int StrengthModifier { get; }
        public int StaminaModifier => 0;
        public int IntelligenceModifier => 0;
        public int DexterityModifier => 0;
    }

}