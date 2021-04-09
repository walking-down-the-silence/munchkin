using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class SetupAvatarStep : IStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            // NOTE: wait for players to play cards and setup the avatar
            table = await table.Dungeon.WaitForAllPlayers();

            var revive = new RevivePlayerAvatarStep();
            return await revive.Resolve(table);
        }
    }
}
