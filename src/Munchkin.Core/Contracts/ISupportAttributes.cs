using System.Collections.Generic;

namespace Munchkin.Core.Contracts
{
    public interface ISupportAttributes
    {
        IReadOnlyCollection<IAttribute> Attributes { get; }
    }
}
