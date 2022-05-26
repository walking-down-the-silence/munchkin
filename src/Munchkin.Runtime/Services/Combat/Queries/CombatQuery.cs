using MediatR;
using Munchkin.Core.Model.Phases;

namespace Munchkin.Runtime.Queries
{
    public record CombatQuery(string TableId) :
        IRequest<Combat>;
}
