using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Actions;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class WarriorClass : ClassCard
    {
        public WarriorClass() : base("Warrior")
        {
        }

        public IAction<Table> AddStrengthAgainstMonster { get; private set; }

        public override Task Play(Table context)
        {
            // TODO: Owner here is null because it is not set yet
            AddStrengthAgainstMonster = new WarriorStrengthBonus1Action();

            return Task.CompletedTask;
        }
    }
}