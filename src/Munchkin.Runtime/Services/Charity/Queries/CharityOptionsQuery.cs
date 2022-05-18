using MediatR;

namespace Munchkin.Runtime.Queries
{
    public record CharityOptionsQuery(string TableId, string PlayerNickname) :
        IRequest<CharityOptions>;
}
