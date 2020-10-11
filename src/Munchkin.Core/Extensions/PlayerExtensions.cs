using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class PlayerExtensions
    {
        public static bool IsWinner(this Player hero, int winningLevel)
        {
            return hero.Level >= winningLevel;
        }

        public static bool WillBecomeWinner(this Player hero, int winningLevel)
        {
            return hero.Level + 1 >= winningLevel;
        }

        public static bool Is<T>(this Player player)
        {
            return player.Equipped.OfType<T>().Any();
        }

        public static IReadOnlyCollection<Card> AllCards(this Player player)
        {
            return Enumerable.Empty<Card>()
                .Concat(player.YourHand)
                .Concat(player.Backpack)
                .Concat(player.Equipped)
                .ToList()
                .AsReadOnly();
        }
    }
}