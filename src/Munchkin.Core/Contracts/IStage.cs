using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.States;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public interface IStage : IState
    {
        bool IsTerminal { get; }

        IReadOnlyCollection<Card> PlayedCards { get; }

        Task<IStage> Resolve();
    }
}
