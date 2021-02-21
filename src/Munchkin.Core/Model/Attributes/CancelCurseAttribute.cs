using Munchkin.Core.Model.Properties;

namespace Munchkin.Core.Model.Attributes
{
    public sealed class CancelCurseAttribute : Attribute
    {
        public CancelCurseAttribute()
        {
            Title = "Cancel Curse";
            Description = string.Empty;
        }
    }
}
