namespace Biz.Morsink.Component.Test
{
    public interface IStatModifier
    {
        int MaxHealthModifier { get; }
        int StrengthModifier { get; }
        int StaminaModifier { get; }
        int IntelligenceModifier { get; }
        int DexterityModifier { get; }
    }

}