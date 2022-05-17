using MediatR;

namespace Munchkin.Runtime.Services
{
    public record LootTheBodyOptionsQuery(string TableId, string PlayerNickname) :
        IRequest<LootTheBodyOptions>;
}
