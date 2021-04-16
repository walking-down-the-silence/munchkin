using Munchkin.Core.Contracts.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class ReviveAndSetupAvatarStep : StepBase<Table>
    {
        private readonly Player _currentPlayer;

        public ReviveAndSetupAvatarStep(Player player) : base(StepNames.ReviveAndSetupAvatar)
        {
            _currentPlayer = player ?? throw new System.ArgumentNullException(nameof(player));
        }

        public Player CurrentPlayer => _currentPlayer;

        protected override async Task<Table> OnResolve(Table table)
        {
            // NOTE: revive the current player if dead
            if (_currentPlayer.IsDead)
            {
                _currentPlayer.Revive(table);
            }

            // NOTE: wait for players to play cards and setup the avatar
            table = await table.Dungeon.WaitForAllPlayers();

            return table;
        }
    }
}
