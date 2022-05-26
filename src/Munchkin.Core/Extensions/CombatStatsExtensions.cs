using Munchkin.Core.Model.Phases;
using System;

namespace Munchkin.Core.Extensions
{
    public static class CombatStatsExtensions
    {
        public static bool IsWinning(this CombatStats combat)
        {
            ArgumentNullException.ThrowIfNull(combat);

            return combat.PlayersStrength > combat.MonsterStrength;
        }

        public static bool WillBeWinning(this CombatStats combat, int strength)
        {
            ArgumentNullException.ThrowIfNull(combat);

            return combat.PlayersStrength + strength > combat.MonsterStrength;
        }

        public static bool IsLoosing(this CombatStats combat)
        {
            ArgumentNullException.ThrowIfNull(combat);

            return combat.PlayersStrength < combat.MonsterStrength;
        }

        public static bool WillBeLoosing(this CombatStats combat, int strength)
        {
            ArgumentNullException.ThrowIfNull(combat);

            return combat.PlayersStrength - strength < combat.MonsterStrength;
        }
    }
}
