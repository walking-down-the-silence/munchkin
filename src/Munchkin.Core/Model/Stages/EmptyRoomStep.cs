using Munchkin.Core.Contracts.Stages;
using Munchkin.Core.Model.Requests;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class EmptyRoomStep : StepBase<Table>
    {
        public EmptyRoomStep() : base(StepNames.EmptyRoom)
        {
        }
        protected override async Task<Table> OnResolve(Table table)
        {
            var request = new PlayerLookForTroubleOrLootTheRoomRequest(table.Players.Current, table);
            var response = await table.RequestSink.Send(request);
            var action = await response.Task;

            IStep<Table> lookForTrouble = new LookForTroubleStep();
            IStep<Table> lootTheRoom = new LootTheRoomStep();
            var stage = action == EmptyRoomActions.LookForTrouble
                ? lookForTrouble
                : lootTheRoom;
            return await stage.Resolve(table);
        }
    }
}
