using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class RevivePlayerAvatarStep : StepBase<Table>
    {
        public RevivePlayerAvatarStep() : base(StepNames.RevivePlayerAvatar)
        {
        }

        protected override async Task<Table> OnResolve(Table table)
        {
            if (table.Players.Current.IsDead)
            {
                table.Players.Current.Revive(table);
            }

            return await Task.FromResult(table);
        }
    }
}
