using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RevivePlayerAvatarStep : ITerminalStep<Table>
    {
        public Task<Table> Resolve(Table table)
        {
            if (table.Players.Current.IsDead)
            {
                table.Players.Current.Revive(table);
            }

            return Task.FromResult(table);
        }
    }
}
