namespace Munchkin.Core.Model.Attributes
{
    public record StrengthBonusAttribute(int Bonus) :
        BonusAttribute(Bonus);
}