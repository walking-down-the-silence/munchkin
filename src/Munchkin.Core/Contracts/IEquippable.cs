using Munchkin.Core.Model;

namespace Munchkin.Core.Contracts
{
    public interface IEquippable
    {
        void Equip(Table table, Player player);
    }
}
