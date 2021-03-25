using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ThiefClass : ClassCard
    {
        public ThiefClass() : base("Thief")
        {
        }

        public IAction<Table> StealTheCard { get; private set; }

        public IAction<Table> Backstab { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            StealTheCard = new ThiefStealCardAction();
            Backstab = new ThiefStabFor2Action();

            return Task.CompletedTask;
        }
    }
}