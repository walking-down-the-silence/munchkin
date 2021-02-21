using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ChangeClass : CurseCard
    {
        public ChangeClass() : base("Change Class")
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

            var firstDiscardedClass = context.DiscardedDoorsCards.TakeFirst<ClassCard>();
            if (firstDiscardedClass != null)
            {
                context.Players.Current.PutInPlayAsEquipped(firstDiscardedClass);
            }

            // TODO: resolve all other cards that don't match the new class
            return Task.CompletedTask;
        }
    }
}