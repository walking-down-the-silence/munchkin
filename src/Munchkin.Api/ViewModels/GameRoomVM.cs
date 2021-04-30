using System.Collections.Generic;

namespace Munchkin.Api.ViewModels
{
    public class GameRoomVM
    {
        public ICollection<UserVM> Users { get; set; }

        public ICollection<GameRoomExpansionSelectionVM> ExpansionSelections { get; set; }
    }
}
