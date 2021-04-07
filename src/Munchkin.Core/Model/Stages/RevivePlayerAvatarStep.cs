using Munchkin.Core.Contracts;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RevivePlayerAvatarStep : TerminalStep<Table>
    {
        public override Task<Table> Resolve(Table table)
        {
            if (table.Players.Current.IsDead)
            {
                table.Players.Current.Revive(table);
            }

            return Task.FromResult(table);
        }
    }
}
