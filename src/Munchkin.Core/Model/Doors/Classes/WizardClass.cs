using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class WizardClass : ClassCard
    {
        public WizardClass() : base("Wizard")
        {
        }

        public IAction<Table> FleeMonster { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            FleeMonster = new WizardFleeMonsterAction();

            return Task.CompletedTask;
        }
    }
}