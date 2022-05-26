using Munchkin.Core.Model.Phases;
using System;

namespace Munchkin.Core.Extensions
{
    public static class CombatExtensions
    {
        public static bool IsWinning(this Combat combat)
        {
            ArgumentNullException.ThrowIfNull(combat);
            return combat.PlayersStrength > combat.MonsterStrength;
        }

        public static bool WillBeWinning(this Combat combat, int strength)
        {
            ArgumentNullException.ThrowIfNull(combat);
            return combat.PlayersStrength + strength > combat.MonsterStrength;
        }

        public static bool IsLoosing(this Combat combat)
        {
            ArgumentNullException.ThrowIfNull(combat);
            return combat.PlayersStrength < combat.MonsterStrength;
        }

        public static bool WillBeLoosing(this Combat combat, int strength)
        {
            ArgumentNullException.ThrowIfNull(combat);
            return combat.PlayersStrength - strength < combat.MonsterStrength;
        }
    }
}
