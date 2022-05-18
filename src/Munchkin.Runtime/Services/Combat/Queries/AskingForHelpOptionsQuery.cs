using MediatR;
using Munchkin.Core.Model.Phases;

namespace Munchkin.Runtime.Queries
{
    public record AskingForHelpOptionsQuery(string TableId) :
        IRequest<AskingForHelp>;
}
