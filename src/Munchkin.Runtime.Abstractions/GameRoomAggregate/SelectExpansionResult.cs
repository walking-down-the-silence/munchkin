using Munchkin.Core.Contracts;

namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public class SelectExpansionResult : Enumeration
    {
        private SelectExpansionResult(int id, string name) : base(id, name)
        {
        }

        public static SelectExpansionResult OptionSelected => new(1, "Option Selected");

        public static SelectExpansionResult OptionUnselected => new(2, "Option Unselected");

        public static SelectExpansionResult InvalidOptionCode => new(3, "Invalid Option Code");
    }
}
