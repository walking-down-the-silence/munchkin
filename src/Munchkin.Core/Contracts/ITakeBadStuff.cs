using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts
{
    public interface ITakeBadStuff
    {
        Table TakeBadStuff(Table table, Player player);
    }
}
