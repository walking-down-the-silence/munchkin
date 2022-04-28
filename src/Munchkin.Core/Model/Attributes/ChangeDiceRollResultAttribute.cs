using Munchkin.Core.Contracts.Attributes;

namespace Munchkin.Core.Model.Attributes
{
    public sealed class ChangeDiceRollResultAttribute : Attribute
    {
        public ChangeDiceRollResultAttribute()
        {
            Title = "Change the dice roll result";
            Description = string.Empty;
        }
    }
}
