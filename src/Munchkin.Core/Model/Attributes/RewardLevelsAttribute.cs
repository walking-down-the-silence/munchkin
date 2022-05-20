namespace Munchkin.Core.Model.Attributes
{
    public record RewardLevelsAttribute(int Bonus) :
        BonusAttribute(Bonus);
}