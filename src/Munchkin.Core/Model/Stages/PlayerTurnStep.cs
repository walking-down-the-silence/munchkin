using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Stages;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class PlayerTurnStep : SequenceStep<Table>
    {
        public PlayerTurnStep()
        {
            AddStep(new ResetActionsStep());
            AddStep(new SetupAvatarStep());
            AddStep(new KickOpenTheDoorStep(null));
        }

        public override async Task<Table> Resolve(Table table)
        {
            // NOTE: wait for each player to actually end the turn by executing action
            table = await base.Resolve(table);
            table = await table.Dungeon.WaitForAllPlayers();
            return table;
        }
    }
}
