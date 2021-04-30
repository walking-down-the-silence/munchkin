using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.GameRoomAggregate;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class ExpansionSelectionExtensions
    {
        public static GameRoomExpansionSelectionVM ToVM(this ExpansionSelection expansionSelection)
        {
            return new GameRoomExpansionSelectionVM
            {
                Code = expansionSelection.Code,
                Title = expansionSelection.Title,
                Selected = expansionSelection.Selected
            };
        }
    }
}
