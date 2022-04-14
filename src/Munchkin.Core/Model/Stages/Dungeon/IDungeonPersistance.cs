namespace Munchkin.Core.Model
{
    public interface IDungeonPersistance
    {
        Dungeon GetDungeonByIdAsync(string dungeonId);

        Dungeon SaveDungeonAsync(Dungeon dungeon);
    }
}