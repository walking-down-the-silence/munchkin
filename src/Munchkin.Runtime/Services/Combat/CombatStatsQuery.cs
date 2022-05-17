using MediatR;
using Munchkin.Core.Model.Phases;

namespace Munchkin.Runtime.Services
{
    public record CombatStatsQuery(string TableId) :
        IRequest<CombatStats>;
}
