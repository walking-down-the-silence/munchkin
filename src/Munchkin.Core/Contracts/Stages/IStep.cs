using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Stages
{
    public interface IStep<TContext>
    {
        string Name { get; }

        Task<TContext> Resolve(TContext context);
    }
}
