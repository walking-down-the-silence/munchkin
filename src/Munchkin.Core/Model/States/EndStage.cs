using System.Threading.Tasks;

namespace Munchkin.Core.Model.States
{
    public class EndStage : State, IStage
    {
        private readonly Table _table;

        public EndStage(Table table)
        {
            _table = table ?? throw new System.ArgumentNullException(nameof(table));
        }

        public bool IsTerminal => true;

        public Task<IStage> Resolve() => Task.FromResult((IStage)this);
    }
}
