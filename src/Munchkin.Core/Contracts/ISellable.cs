using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts
{
    public interface ISellable
    {
        Table Sell(Table table);
    }
}
