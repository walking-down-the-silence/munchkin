using System.Collections.Generic;

namespace Munchkin.Api.ViewModels
{
    public class TableVM
    {
        public ICollection<PlayerVM> Players { get; set; }

        public ICollection<TableExpansionSelectionVM> ExpansionSelections { get; set; }
    }
}
