using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests
{
    public class YesNoActions : Enumeration
    {
        private YesNoActions(int id, string name) : base(id, name)
        {
        }

        public static YesNoActions Yes => new(1, "Yes");

        public static YesNoActions No => new(2, "No");
    }
}
