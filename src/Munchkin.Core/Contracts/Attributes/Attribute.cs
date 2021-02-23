namespace Munchkin.Core.Contracts.Attributes
{
    public abstract class Attribute : IAttribute
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}