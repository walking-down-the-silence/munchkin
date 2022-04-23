using Munchkin.Core.Model;
using Munchkin.Core.Model.Phases;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Requests.Enums;
using Munchkin.Core.Primitives;
using System.Threading.Tasks;

namespace Munchkin.Core.Tests.Primitives
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