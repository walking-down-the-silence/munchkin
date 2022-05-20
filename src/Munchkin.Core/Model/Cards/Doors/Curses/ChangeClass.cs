using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeClass : CurseCard
    {
        public ChangeClass() : base(MunchkinDeluxeCards.Doors.ChangeClass , "Change Class")
        {
        }

        public override Task BadStuff(Table context)
        {
            foreach (var equippedCard in context.Players.Current.Equipped)
            {
                if (equippedCard is ClassCard || equippedCard is SuperMunchkin)
                {
                    equippedCard.Discard(context);
                }
            }

            context = context with { DiscardedDoorsCards = context.DiscardedDoorsCards.TakeFirst<ClassCard>(out var firstDiscardedClass) };
            if (firstDiscardedClass != null)
            {
                context.Players.Current.Equip(firstDiscardedClass);
            }

            // TODO: resolve all other cards that don't match the new class
            return Task.CompletedTask;
        }
    }
}