using MediatR;

namespace Munchkin.Core.Model.Phases
{
    public record AskingForHelpOptionsQuery(string TableId) :
        IRequest<AskingForHelp>;
}
