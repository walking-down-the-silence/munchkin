using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LoseYourClass : CurseCard
    {
        public LoseYourClass() : base("Lose Your Class")
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