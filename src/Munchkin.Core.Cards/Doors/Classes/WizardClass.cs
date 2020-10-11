using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using Munchkin.Engine.Original.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class WizardClass : ClassCard
    {
        public WizardClass() : base("Wizard")
        {
            AddAction(new ActionDefinition<Table>("", () => new WizardFleeMonsterAction()));
        }

        public override Task Play(Table context)
        {
            throw new System.NotImplementedException();
        }
    }
}