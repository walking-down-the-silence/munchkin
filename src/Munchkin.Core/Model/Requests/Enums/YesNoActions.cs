using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Requests.Enums
{
    public sealed record YesNoActions : Enumeration
    {
        private YesNoActions(int code, string name) : base(code, name)
        {
        }

        public static YesNoActions Yes => new(1, "Yes");

        public static YesNoActions No => new(2, "No");
    }
}
