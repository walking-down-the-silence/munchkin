using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class NetTroll : MonsterCard
    {
        public NetTroll() : 
            base(MunchkinDeluxeCards.Doors.NetTroll, "Net Troll", 10, 1, 3, 0, false)
        {
        }

        public async override Task BadStuff(Table state)
        {
            var maxLevel = state.Players.Max(x => x.Level);
            var players = state.Players.Where(x => x.Level == maxLevel).GetEnumerator();

            // until the current pllayer still has cards and more player of max level
            while (players.MoveNext() && state.Players.Current.Equipped.OfType<TreasureCard>().Any())
            {
                var treasures = state.Players.Current.Equipped.OfType<TreasureCard>().ToList();
                //var request = new PlayerSelectSingleCardRequest(players.Current, state, treasures);
                //var response = await state.RequestSink.Send(request);
                //var card = await response.Task;

                //state.Players.Current.Discard(card);
                //players.Current.PutInBackpack(card);
            }
        }
    }
}