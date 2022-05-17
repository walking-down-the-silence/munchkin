using MediatR;

namespace Munchkin.Runtime.Services
{
    public record CharityOptionsQuery(string TableId, string PlayerNickname) :
        IRequest<CharityOptions>;
}
