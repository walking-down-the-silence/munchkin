namespace Munchkin.Core.Contracts.Attributes
{
    public abstract record Attribute(string Title) : 
        IAttribute;
}