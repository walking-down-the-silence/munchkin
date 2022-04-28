using Munchkin.Core.Model;
using Orleans;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    public interface IPlayer : IGrainWithStringKey
    {
        Task<Player> GetStateAsync();
    }
}
