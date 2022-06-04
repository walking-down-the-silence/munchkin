using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Cards.Doors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Munchkin.Core.Extensions
{
    public static class PlayerExtensions
    {
        public static bool IsAlive(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Life == EPlayerLifeState.Alive;
        }

        public static bool IsDead(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Life == EPlayerLifeState.Dead;
        }

        public static bool IsRevived(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Life == EPlayerLifeState.Revived;
        }

        public static bool IsWinning(this Player player, int winningLevel)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Level >= winningLevel;
        }

        public static bool WillBeWinning(this Player player, int winningLevel)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Level + 1 >= winningLevel;
        }

        public static bool HasEquipped<T>(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Equipped.OfType<T>().Any();
        }

        public static bool HasInBackpack<T>(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Backpack.OfType<T>().Any();
        }

        public static bool HasInHand<T>(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.YourHand.OfType<T>().Any();
        }

        public static bool HasActiveAttribute<TAttribute>(this Player player)
            where TAttribute : IAttribute
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.Equipped.Any(x => x.HasAttribute<TAttribute>());
        }

        public static IReadOnlyCollection<Card> AllCards(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            return Enumerable.Empty<Card>()
                .Concat(player.YourHand)
                .Concat(player.Backpack)
                .Concat(player.Equipped)
                .ToList()
                .AsReadOnly();
        }

        public static IReadOnlyCollection<Card> AllLootableCards(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            var playerCards = Enumerable.Empty<Card>()
                .Concat(player.YourHand)
                .Concat(player.Equipped
                    .ExceptType<ClassCard>()
                    .ExceptType<RaceCard>()
                    .ExceptType<CurseCard>()
                    .ExceptType<Halfbreed>()
                    .ExceptType<SuperMunchkin>())
                .Concat(player.Backpack)
                .ToList();

            return playerCards;
        }

        public static TCard FirstOrDefault<TCard>(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));
            return player.AllCards().OfType<TCard>().FirstOrDefault();
        }

        public static int GetMaximumCardsInHand(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            const int DefaultMaximumCardsInHand = 5;
            return player.Equipped.Any(x => x.HasAttribute<MaximumCardsInHandAttribute>())
                ? player.Equipped.Max(x => x.GetAttribute<MaximumCardsInHandAttribute>().Limit)
                : DefaultMaximumCardsInHand;
        }

        public static int GetMaximumBigItemsCarried(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            const int DefaultMaximumBigItemsCarried = 1;
            return player.Equipped.Any(x => x.HasAttribute<MaximumBigItemsCarriedAttribute>())
                ? player.Equipped.Max(x => x.GetAttribute<MaximumBigItemsCarriedAttribute>().Limit)
                : DefaultMaximumBigItemsCarried;
        }

        public static int GetMaximumClassesEquipped(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            const int DefaultMaximumClassesEquipped = 1;
            return player.Equipped.Any(x => x.HasAttribute<MaximumEquippedClassesAttribute>())
                ? player.Equipped.Max(x => x.GetAttribute<MaximumEquippedClassesAttribute>().Limit)
                : DefaultMaximumClassesEquipped;
        }

        public static int GetMaximumRacesEquipped(this Player player)
        {
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            const int DefaultMaximumClassesEquipped = 1;
            return player.Equipped.Any(x => x.HasAttribute<MaximumEquippedRacesAttribute>())
                ? player.Equipped.Max(x => x.GetAttribute<MaximumEquippedRacesAttribute>().Limit)
                : DefaultMaximumClassesEquipped;
        }
    }
}