using Orleans;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface ITurn : IGrainWithStringKey
    {
        Task<ITurn> NextTurn();
    }
}
