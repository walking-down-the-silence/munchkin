using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts
{
    public interface ITakeBadStuff
    {
        Table BadStuff(Table table, Player player);
    }
}
