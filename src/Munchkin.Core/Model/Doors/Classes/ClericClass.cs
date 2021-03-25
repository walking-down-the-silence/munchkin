using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class ClericClass : ClassCard
    {
        public ClericClass() : base("Cleric")
        {
        }

        public IAction<Table> ReviveTheCard { get; private set; }

        public IAction<Table> AddStrengtAgainstUndead { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            ReviveTheCard = new ClericReviveCardAction(Owner);
            AddStrengtAgainstUndead = new ClericStrengthBonus3Action();

            return Task.CompletedTask;
        }
    }
}