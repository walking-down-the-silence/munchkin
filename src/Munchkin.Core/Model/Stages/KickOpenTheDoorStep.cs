using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class KickOpenTheDoorStep : IStep<Table>
    {
        private readonly List<Card> _playedCards = new();

        public KickOpenTheDoorStep()
        {
        }

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public async Task<Table> Resolve(Table table)
        {
            var door = table.DoorsCardDeck.Take();
            _playedCards.Add(door);

            var stage = door switch
            {
                CurseCard curseCard     => new CursedRoomStage(curseCard, _playedCards),
                MonsterCard monsterCard => new CombatRoomStep(monsterCard, _playedCards),
                _                       => TakeInHand(table, door)
            };

            return await stage.Resolve(table);
        }

        private IStep<Table> TakeInHand(Table table, DoorsCard doorsCard)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            _playedCards.Remove(doorsCard);
            table.Players.Current.TakeInHand(doorsCard);
            return new EmptyRoomStep(_playedCards);
        }
    }
}
