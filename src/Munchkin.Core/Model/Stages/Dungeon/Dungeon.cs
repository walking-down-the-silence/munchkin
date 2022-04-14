using Munchkin.Core.Contracts.Cards;
using System.Collections.Immutable;

namespace Munchkin.Core.Model
{
    /// <summary>
    /// Defines the state of the dungeon that the player has entered.
    /// </summary>
    /// <param name="Door">The door card that the player played to enter the dungeon.</param>
    /// <param name="PlayedCards">All the played cards in the dungeon.</param>
    /// <param name="Attributes">All the attributes that the state has.</param>
    public record Dungeon(
        DoorsCard Door,
        ImmutableList<Card> PlayedCards,
        ImmutableList<Contracts.Attributes.Attribute> Attributes)
    {
        public static Dungeon KickOpenTheDoor(DoorsCard doors) => new(
            doors,
            ImmutableList<Card>.Empty.Add(doors),
            ImmutableList<Contracts.Attributes.Attribute>.Empty);

        //public static Dungeon KickOpenTheDoor(Table table)
        //{
        //    var door = table.DoorsCardDeck.Take();
        //    var dungeon = new Dungeon(door, ImmutableList<Card>.Empty);

        //    dungeon = door is not MonsterCard && door is not CurseCard
        //        ? dungeon.TakeInHand(table)
        //        : dungeon.PutInPlay(table);

        //    return dungeon;
        //}
    }
}