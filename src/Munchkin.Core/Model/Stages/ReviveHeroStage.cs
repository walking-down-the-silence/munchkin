using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Stages
{
    public class ReviveHeroStage : State, IStage
    {
        public bool IsTerminal => false;

        public IReadOnlyCollection<Card> PlayedCards => Enumerable.Empty<Card>().ToArray();

        public Task<IStage> Resolve(Table table)
        {
            if (table.Players.Current.IsDead)
            {
                table.Players.Current.Revive(table);
            }

            var stage = new KickOpenTheDoorStage();
            return Task.FromResult<IStage>(stage);
        }
    }
}
