using Munchkin.Api.ViewModels;
using Munchkin.Core.Model.Expansions;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class ExpansionSelectionExtensions
    {
        public static TableExpansionSelectionVM ToVM(this ExpansionOption expansionOption)
        {
            return new TableExpansionSelectionVM
            {
                Code = expansionOption.Code,
                Title = expansionOption.Title,
                Selected = false
            };
        }
    }
}
