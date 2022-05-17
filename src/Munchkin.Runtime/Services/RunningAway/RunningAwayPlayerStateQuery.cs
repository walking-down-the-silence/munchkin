using MediatR;

namespace Munchkin.Runtime.Services
{
    public record RunningAwayPlayerStateQuery(string TableId, string PlayerNickname) :
        IRequest<RunningAwayPlayerState>;
}
