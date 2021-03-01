using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class KickOpenTheDoorStage : State, IStage
    {
        private readonly List<Card> _playedCards = new();

        public KickOpenTheDoorStage()
        {
        }

        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => _playedCards.AsReadOnly();

        public Task<IStage> Resolve(Table table)
        {
            var door = table.Dungeon.KickOpenTheDoor();
            _playedCards.Add(door);

            var stage = door switch
            {
                CurseCard curseCard => new CurseStage(curseCard, _playedCards),
                MonsterCard monsterCard => new CombatStage(monsterCard, _playedCards),
                _ => TakeInHand(table, door)
            };

            return Task.FromResult(stage);
        }

        private IStage TakeInHand(Table table, DoorsCard doorsCard)
        {
            // NOTE: if card is taking in hand then remove from played, so it is not discarded later
            _playedCards.Remove(doorsCard);
            table.Players.Current.TakeInHand(doorsCard);
            return new EmptyStage(_playedCards);
        }
    }
}
