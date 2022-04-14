using System.Collections.Generic;

namespace Munchkin.Api.ViewModels.Trading
{
    public record TradeSideVM(
        string PlayerId,
        ICollection<string> AvaialbleCardIds,
        ICollection<string> OfferedCardIds);
}
