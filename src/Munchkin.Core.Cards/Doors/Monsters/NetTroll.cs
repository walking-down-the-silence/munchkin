using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class NetTroll : MonsterCard
    {
        public NetTroll() : base("Net Troll", 10, 1, 3, 0, false)
        {
        }

        public async override Task BadStuff(Table context)
        {
            var maxLevel = context.Players.Max(x => x.Level);
            var players = context.Players.Where(x => x.Level == maxLevel).GetEnumerator();

            // until the current pllayer still has cards and more player of max level
            while (players.MoveNext() && context.Players.Current.Equipped.OfType<TreasureCard>().Any())
            {
                var card = await context.RequestSink.RequestAsync(players.Current, null);
                context.Players.Current.Discard(card);
                players.Current.PutInPlayAsCarried(card);
            }
        }
    }
}