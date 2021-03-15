using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class SetupAvatarStep : IHierarchialStep<Table>
    {
        public async Task<Table> Resolve(Table table)
        {
            var revive = new RevivePlayerAvatarStep();
            // TODO: wait for player to play cards and setup the avatar
            return await revive.Resolve(table);
        }
    }
}
