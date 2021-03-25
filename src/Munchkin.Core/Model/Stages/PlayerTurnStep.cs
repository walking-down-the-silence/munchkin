using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class PlayerTurnStep : IHierarchialStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            var resetActions = new ResetActionsStep();
            table = await resetActions.Resolve(table);

            var setupAvatar = new SetupAvatarStep();
            table = await setupAvatar.Resolve(table);

            var room = new KickOpenTheDoorStep();
            table = await room.Resolve(table);

            // NOTE: wait for each player to actually end the turn by executing action
            await table.Dungeon.WaitForAllPlayers();

            return table;
        }
    }
}
