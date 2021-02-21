using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.Requests;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class Pitbull : MonsterCard
    {
        public Pitbull() : base("Pitbull", 2, 1, 1, 0, false)
        {
        }

        public async override Task BadStuff(Table context)
        {
            // TODO: request the player to discard an item that looks like a stick
            var request = new SelectCardsRequest(context.Players.Current, context, context.Players.Current.Equipped);
            var response = await context.RequestSink.Send(request);
            var card = await response.Task;

            if (card != null)
            {
                context.Players.Current.Discard(card);
            }
            else
            {

                context.Players.Current.LevelDown();
                context.Players.Current.LevelDown();
            }
        }
    }
}