using Munchkin.Core.Model.Cards;

namespace Munchkin.Core.Model
{
    public class CurseStage
    {
        private CurseStage(Dungeon dungeon, CurseCard curse)
        {
            Dungeon = dungeon ?? throw new System.ArgumentNullException(nameof(dungeon));
            Curse = curse ?? throw new System.ArgumentNullException(nameof(curse));
            LastCardPlayed = curse;
        }

        public static CurseStage FromCurse(Dungeon dungeon, CurseCard curse)
        {
            return new CurseStage(dungeon, curse);
        }

        public Dungeon Dungeon { get; }

        public CurseCard Curse { get; }

        public Card LastCardPlayed { get; }

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

        public CurseStage LootTheRoom()
        {
            System.Console.WriteLine(nameof(LootTheRoom));
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
