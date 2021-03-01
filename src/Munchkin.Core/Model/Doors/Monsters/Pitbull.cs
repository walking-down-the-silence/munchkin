using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Requests;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Pitbull : MonsterCard
    {
        public Pitbull() : base("Pit Bull", 2, 1, 1, 0, false)
        {
            //TODO: handle logic here
        }

        public async override Task BadStuff(Table state)
        {
            // TODO: request the player to discard an item that looks like a stick
            var request = new PlayerSelectSingleCardRequest(state.Players.Current, state, state.Players.Current.Equipped);
            var response = await state.RequestSink.Send(request);
            var card = await response.Task;

            if (card != null)
            {
                state.Players.Current.Discard(state, card);
            }
            else
            {

                state.Players.Current.LevelDown();
                state.Players.Current.LevelDown();
            }
        }
    }
}