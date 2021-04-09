using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IStep<TContext>
    {
        Task<TContext> Resolve(TContext context);
    }
}
