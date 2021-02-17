using Munchkin.Core.Model.Cards;
using Munchkin.Core.Model.States;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public class CurseStage : State, IStage
    {
        private readonly Table _table;

        public CurseStage(Table table, CurseCard curse)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
            LastCardPlayed = curse;
        }

        public Dungeon Dungeon { get; }

        public CurseCard Curse { get; }

        public Card LastCardPlayed { get; }

        public bool IsTerminal => false;

        public Task<IStage> Resolve()
        {
            // TODO: prompt the player to handle the curse by either playing a Wishing Ring card or by Taking The Bad Stuff
            // TODO: prompt the player to either loot the room or look for trouble
            bool lookForTrouble = true;
            return lookForTrouble ? LookForTrouble() : LootTheRoom();
        }

        public async Task<IStage> LootTheRoom()
        {
            DoorsCard doorsCard = _table.DoorsCardDeck.Take();
            _table.Players.Current.TakeInHand(doorsCard);
            return new EndStage(_table);
        }

        public async Task<IStage> LookForTrouble()
        {
            // TODO: prompt a request to the player to select a monster from hand
            MonsterCard monsterCard = default;
            return new CombatStage(_table, monsterCard);
        }

        public bool AnyCardsPlayed()
        {
            System.Console.WriteLine(nameof(AnyCardsPlayed));
            bool played = true;
            return played;
        }

        /// <summary>
        /// TODO: prompt user to play beast from hand
        /// </summary>
        /// <returns></returns>
        public bool IsBeastPlayedFromHand()
        {
            System.Console.WriteLine(nameof(IsBeastPlayedFromHand));
            bool played = true;
            return played;
        }

        public CurseStage TakeBadStuff()
        {
            System.Console.WriteLine(nameof(TakeBadStuff));
            return this;
        }

        public CurseStage TakeCardInHand()
        {
            System.Console.WriteLine(nameof(TakeCardInHand));
            return this;
        }

        public CurseStage PromptUserToPlayBeast()
        {
            System.Console.WriteLine(nameof(PromptUserToPlayBeast));
            return this;
        }

        public CurseStage Recalculate()
        {
            System.Console.WriteLine(nameof(Recalculate));
            return this;
        }

        public CurseStage PromptUserToPlayCards()
        {
            System.Console.WriteLine(nameof(PromptUserToPlayCards));
            return this;
        }

        public bool IsCurseCancelled()
        {
            System.Console.WriteLine(nameof(IsCurseCancelled));
            bool cancelled = true;
            return cancelled;
        }

        public bool LastPlayedIsCurse()
        {
            System.Console.WriteLine(nameof(LastPlayedIsCurse));
            return LastCardPlayed is CurseCard;
        }
    }
}
