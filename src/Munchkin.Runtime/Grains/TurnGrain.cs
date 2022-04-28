using Munchkin.Core.Model;
using Munchkin.Runtime.Abstractions;
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Grains
{
    public class TurnGrain : Grain, ITurn
    {
        private readonly IPersistentState<Table> _tablePersistance;
        private readonly IPersistentState<Turn> _turnPersistence;

        public TurnGrain(
            [PersistentState("table", "tableStore")] IPersistentState<Table> tablePersistance,
            [PersistentState("turn", "turnStore")] IPersistentState<Turn> turnPersistence)
        {
            _tablePersistance = tablePersistance ?? throw new ArgumentNullException(nameof(tablePersistance));
            _turnPersistence = turnPersistence ?? throw new ArgumentNullException(nameof(turnPersistence));
        }

        public override async Task OnActivateAsync()
        {
            _turnPersistence.State = Turn.From(_tablePersistance.State);

            await _turnPersistence.WriteStateAsync();
            await base.OnActivateAsync();
        }

        public async Task<ITurn> NextTurn()
        {
            _turnPersistence.State = Turn.Next(_turnPersistence.State);

            await _turnPersistence.WriteStateAsync();
            return this.AsReference<ITurn>();
        }
    }
}
