using Munchkin.Core.Contracts;

namespace Munchkin.Core.Model.Properties
{
    public abstract class Attribute : IAttribute
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}