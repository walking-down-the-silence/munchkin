using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Requests all players to end the battle and gets the result
    /// </summary>
    public class EndBattleAction : DynamicAction
    {
        public EndBattleAction() : base("End Battle", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            // TODO: request if all players agree to end the battle before actually ending it
            //if (state.PlayersWon())
            //{
            //    // if player has won the battle then take rewards
            //    state.TakeReward();
            //}
            //else if (state.MonstersWon())
            //{
            //    // allow to run away from monster
            //    var runAwayAction = new RunAwayAction(Player);
            //    Player.AddAction(runAwayAction);

            //    // allow to look for trouble
            //    var takeBadStuffAction = new TakeBadStuffAction(Player);
            //    Player.AddAction(takeBadStuffAction);
            //}

            return state;
        }
    }
}