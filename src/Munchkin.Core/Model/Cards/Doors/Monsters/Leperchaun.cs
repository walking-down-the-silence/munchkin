using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Rules;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Leperchaun : MonsterCard
    {
        public Leperchaun() : base("Leperchaun", 4, 1, 2, 0, false)
        {
            AddEffect(Effect
              .New(new MonsterStrengthBonusEffect(5))
              .With(() => Rule
                  .New(new HasElfRaceRule())));
        }

        public async override Task BadStuff(Table state)
        {
            var cardSelectedByLeftPlayer = await new PlayerSelectSingleCardRequest(state.Players.PeekNext(), state, state.Players.Current.Equipped)
                .SendAsync(state);
            state.Players.Current.Discard(cardSelectedByLeftPlayer);
            state.DiscardedDoorsCards.Put(cardSelectedByLeftPlayer as DoorsCard);

            var cardSelectedByRightPlayer = await new PlayerSelectSingleCardRequest(state.Players.PeekPrevious(), state, state.Players.Current.Equipped)
                .SendAsync(state);
            state.Players.Current.Discard(cardSelectedByRightPlayer);
            state.DiscardedDoorsCards.Put(cardSelectedByRightPlayer as DoorsCard);
        }
    }
}