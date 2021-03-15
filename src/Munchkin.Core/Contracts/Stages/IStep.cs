using System.Threading.Tasks;

namespace Munchkin.Core.Contracts
{
    public interface IStep<TContext>
    {
        Task<TContext> Resolve(TContext context);
    }
}
