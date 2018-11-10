namespace Biz.Morsink.Component.Test
{
    public class RingOfTheMind : IStatModifier
    {
        public RingOfTheMind(int intelligenceModifier)
        {
            IntelligenceModifier = intelligenceModifier;
        }
        public int MaxHealthModifier => 0;
        public int StrengthModifier => 0;
        public int StaminaModifier => 0;
        public int IntelligenceModifier { get; }
        public int DexterityModifier => 0;
    }

}