using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Primitives
{
    public class LookForTroubleStep : StepBase<Table>
    {
        public LookForTroubleStep() : base(StepNames.LookForTrouble)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            var monsters = table.Players.Current.YourHand.OfType<MonsterCard>().ToList();
            var request = new PlayerSelectMonsterFromHandRequest(table.Players.Current, table, monsters);
            var response = await table.RequestSink.Send(request);
            var monsterCard = await response.Task;

            var stage = new CombatRoomStep(table.Players.Current, monsterCard);
            return await stage.Resolve(table);
        }
    }
}