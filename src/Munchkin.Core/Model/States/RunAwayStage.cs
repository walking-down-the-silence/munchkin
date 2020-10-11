namespace Munchkin.Core.Model
{
    public class RunAwayStage
    {
        public RunAwayStage(Dungeon dungeon, Table table)
        {
            Dungeon = dungeon ?? throw new System.ArgumentNullException(nameof(dungeon));
            Table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public Dungeon Dungeon { get; }

        public Table Table { get; }

        public RunAwayStage EndTurn()
        {
            System.Console.WriteLine(nameof(EndTurn));
            return this;
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
