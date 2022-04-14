using Munchkin.Api.ViewModels;
using Munchkin.Core.Model.Expansions;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class ExpansionSelectionExtensions
    {
        public static TableExpansionSelectionVM ToVM(this ExpansionSelection expansionSelection)
        {
            return new TableExpansionSelectionVM
            {
                Code = expansionSelection.Code,
                Title = expansionSelection.Title,
                Selected = expansionSelection.Selected
            };
        }
    }
}
