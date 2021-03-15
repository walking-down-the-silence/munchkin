using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests;
using Munchkin.Core.Model.Stages;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class PlayerTurnStep : IHierarchialStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            // NOTE: propagates a request with end turn action to each player
            var playerEndTurnActionRequests = table.Players.Select(player => new PlayerEndTurnRequest(table, player));
            var requestTasks = playerEndTurnActionRequests.Select(request => table.RequestSink.Send(request));
            await Task.WhenAll(requestTasks);

            // NOTE: wait for each player to actually end the turn by executing action
            var responseTasks = requestTasks.Select(task => task.Result.Task);
            await Task.WhenAll(responseTasks);

            var setupAvatar = new SetupAvatarStep();
            table = await setupAvatar.Resolve(table);

            var room = new KickOpenTheDoorStep();
            table = await room.Resolve(table);

            return table;
        }
    }
}
