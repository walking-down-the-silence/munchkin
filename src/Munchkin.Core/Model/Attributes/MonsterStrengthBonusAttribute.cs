namespace Munchkin.Core.Model.Attributes
{
    public record MonsterStrengthBonusAttribute(int Bonus) :
        BonusAttribute(Bonus);
}