using Munchkin.Core.Model.States;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class RunAwayStage : State, IStage
    {
        private readonly Table _table;

        public RunAwayStage(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public bool IsTerminal => false;

        public async Task<IStage> Resolve()
        {
            // TODO: prompt the player to throw the dice or play cards for runaway
            return new EmptyStage(_table);
        }

        public RunAwayStage TakeBadStuff()
        {
            System.Console.WriteLine(nameof(TakeBadStuff));
            return this;
        }

        /// <summary>
        /// TODO: check if any player played cards
        /// </summary>
        /// <returns></returns>
        public bool AnyCardsPlayed()
        {
            System.Console.WriteLine(nameof(AnyCardsPlayed));
            bool played = true;
            return played;
        }

        public bool IsDiceRollSuccessful()
        {
            System.Console.WriteLine(nameof(IsDiceRollSuccessful));
            bool successful = true;
            return successful;
        }

        public RunAwayStage RollTheDice()
        {
            System.Console.WriteLine(nameof(RollTheDice));
            return this;
        }

        /// <summary>
        /// // TODO: prompt player if try running
        /// </summary>
        /// <returns></returns>
        public bool IsRunningAway()
        {
            System.Console.WriteLine(nameof(IsRunningAway));
            bool running = true;
            return running;
        }

        /// <summary>
        /// TODO: update the state
        /// </summary>
        /// <returns></returns>
        public RunAwayStage Recalculate()
        {
            System.Console.WriteLine(nameof(Recalculate));
            return this;
        }

        public RunAwayStage PromptUserToPlayCards()
        {
            System.Console.WriteLine(nameof(PromptUserToPlayCards));
            return this;
        }
    }
}
