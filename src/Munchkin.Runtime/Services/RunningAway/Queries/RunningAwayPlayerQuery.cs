using MediatR;
using Munchkin.Runtime.Services;

namespace Munchkin.Runtime.RunningAway.Queries
{
    public record RunningAwayPlayerQuery(string TableId, string PlayerNickname) :
        IRequest<RunningAwayPlayer>;
}
