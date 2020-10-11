using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class PlutoniumDragon : MonsterCard
    {
        public PlutoniumDragon() : base("Plutonium Dragon", 20, 2, 5, 0, false)
        {
        }

        public override Task BadStuff(Table context)
        {
            if (context.Players.Current.Level > 5)
            {
                context.Players.Current.Kill();
            }

            return Task.CompletedTask;
        }
    }
}