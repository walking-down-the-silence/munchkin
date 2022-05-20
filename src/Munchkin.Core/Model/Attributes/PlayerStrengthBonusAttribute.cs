namespace Munchkin.Core.Model.Attributes
{
    public record PlayerStrengthBonusAttribute(int Bonus) :
        BonusAttribute(Bonus);
}