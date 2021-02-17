using Munchkin.Core.Contracts.States;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public interface IStage : IState
    {
        bool IsTerminal { get; }

        Task<IStage> Resolve();
    }
}
