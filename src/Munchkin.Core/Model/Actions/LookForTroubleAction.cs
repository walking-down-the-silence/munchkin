using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Look for trouble for a certain player
    /// </summary>
    public class LookForTroubleAction : DynamicAction
    {
        public LookForTroubleAction() : base("Look For Trouble", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            // send a request to player if he is looking for trouble (wants to or can fight his monster)
            //var ownMonster = state.RequestSink.Request<MonsterCard>(Player, Player, Player.YourHand.OfType<MonsterCard>());
            //var monster = await ownMonster.GetResult();
            //if (monster != null)
            //{
            //    await state.Dungeon.PlayACard(monster);

            //    // allow to run away from monster
            //    var endBattleAction = new EndBattleAction(Player);
            //    Player.AddAction(endBattleAction);

            //    // allow to ask for help only if doors led to monster
            //    var askForHelpAction = new AskForHelpAction(Player);
            //    Player.AddAction(askForHelpAction);

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