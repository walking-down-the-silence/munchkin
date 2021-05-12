namespace Munchkin.Core.Model.Stages
{
    public class StepNames
    {
        public static string[] All = new[]
        {
            Charity,
            Combat,
            Curse,
            Death,
            EmptyRoom,
            PlayerTurnEnd,
            KickOpenTheDoor,
            LookForTrouble,
            LootTheRoom,
            RevivePlayerAvatar,
            RunAway,
            ReviveAndSetupAvatar
        };

        public const string Charity = "Charity";

        public const string Combat = "Combat";

        public const string Curse = "Curse";

        public const string Death = "Death";

        public const string EmptyRoom = "Empty Room";

        public const string PlayerTurnEnd = "Player Turn End";

        public const string KickOpenTheDoor = "Kick Open The Door";

        public const string LookForTrouble = "Look For Trouble";

        public const string LootTheRoom = "Loot The Room";

        public const string RevivePlayerAvatar = "Revive Player Avatar";

        public const string RunAway = "Run Away";

        public const string ReviveAndSetupAvatar = "Revive & Setup Avatar";
    }
}
