using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class ChangeClass : CurseCard
    {
        public ChangeClass() : base(MunchkinDeluxeCards.Doors.ChangeClass, "Change Class")
        {
        }

        public override Task BadStuff(Table table)
        {
            // TODO: Clarify in Errata if class card should be discarded or cards that act as a class as well
            table.Players.Current.Equipped
                .Where(x => x is ClassCard || x is SuperMunchkin)
                .ForEach(x => x.Discard(table));

            table = table with
            {
                DiscardedDoorsCards = table.DiscardedDoorsCards.TakeFirst<ClassCard>(out var classCard)
            };

            if (classCard != null)
                table.Players.Current.Equip(classCard);

            return Task.CompletedTask;
        }
    }
}