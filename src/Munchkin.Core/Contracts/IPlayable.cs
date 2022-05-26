using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public interface IPlayable
    {
        Task Play(Table table);
    }
}
