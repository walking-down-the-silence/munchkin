using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model.Requests;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    /// <summary>
    /// Prompts a request to the player to select a monster from hand, if any.
    /// </summary>
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
