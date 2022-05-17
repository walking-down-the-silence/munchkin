using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards.Doors;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class PlayerExtensions
    {
        public static bool IsAlive(this Player player) => player.Life == EPlayerLifeState.Alive;

        public static bool IsDead(this Player player) => player.Life == EPlayerLifeState.Dead;

        public static bool IsRevived(this Player player) => player.Life == EPlayerLifeState.Revived;

        public static bool IsWinner(this Player hero, int winningLevel) => hero.Level >= winningLevel;

        public static bool WillBecomeWinner(this Player hero, int winningLevel) => hero.Level + 1 >= winningLevel;

        public static bool HasEquipped<T>(this Player player) => player.Equipped.OfType<T>().Any();

        public static bool HasInBackpack<T>(this Player player) => player.Backpack.OfType<T>().Any();

        public static bool HasInHand<T>(this Player player) => player.YourHand.OfType<T>().Any();

        public static IReadOnlyCollection<Card> AllCards(this Player player)
        {
            return Enumerable.Empty<Card>()
                .Concat(player.YourHand)
                .Concat(player.Backpack)
                .Concat(player.Equipped)
                .ToList()
                .AsReadOnly();
        }

        public static IReadOnlyCollection<Card> AllLootableCards(this Player player)
        {
            var playerCards = Enumerable.Empty<Card>()
                .Concat(player.YourHand)
                .Concat(player.Equipped
                    .NotOfType<ClassCard>()
                    .NotOfType<RaceCard>()
                    .NotOfType<CurseCard>()
                    .NotOfType<Halfbreed>()
                    .NotOfType<SuperMunchkin>())
                .Concat(player.Backpack)
                .ToList();

            return playerCards;
        }

        public static TCard FirstOrDefault<TCard>(this Player player)
        {
            return player.AllCards().OfType<TCard>().FirstOrDefault();
        }
    }
}