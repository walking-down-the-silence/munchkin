using Munchkin.Core.Contracts.Cards;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Curses
{
    public sealed class LoseYourClass : CurseCard
    {
        public LoseYourClass() : 
            base(MunchkinDeluxeCards.Doors.LoseYourClass, "Lose Your Class")
        {
        }

        public override Task BadStuff(Table context)
        {
            var classes = context.Players.Current.Equipped
                .OfType<ClassCard>()
                .ToList();

            if (classes.Count > 1)
            {
                // select which one to discard
            }
            else
            {
                classes.FirstOrDefault()?.Discard(context);
            }

            return Task.CompletedTask;
        }
    }
}