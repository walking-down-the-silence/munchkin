using Munchkin.Core.Model.Phases;

namespace Munchkin.Runtime.Abstractions
{
    public interface IDungeonPersistance
    {
        Dungeon GetDungeonByIdAsync(string dungeonId);

        Dungeon SaveDungeonAsync(Dungeon dungeon);
    }
}