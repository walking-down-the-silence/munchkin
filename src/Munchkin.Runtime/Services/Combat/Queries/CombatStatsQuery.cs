using MediatR;
using Munchkin.Core.Model.Phases;

namespace Munchkin.Runtime.Queries
{
    public record CombatStatsQuery(string TableId) :
        IRequest<CombatStats>;
}
