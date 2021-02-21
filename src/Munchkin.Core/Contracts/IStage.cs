using Munchkin.Core.Contracts.States;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public interface IStage : IState
    {
        bool IsTerminal { get; }

        Task<IStage> Resolve();
    }
}
