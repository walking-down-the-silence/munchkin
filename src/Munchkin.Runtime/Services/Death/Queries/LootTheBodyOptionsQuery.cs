using MediatR;
using Munchkin.Runtime.Services;

namespace Munchkin.Runtime.Queries
{
    public record LootTheBodyOptionsQuery(string TableId, string PlayerNickname) :
        IRequest<LootTheBodyOptions>;
}
