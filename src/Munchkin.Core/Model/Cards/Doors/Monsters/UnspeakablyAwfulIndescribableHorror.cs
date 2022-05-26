using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Cards.Doors.Classes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class UnspeakablyAwfulIndescribableHorror : MonsterCard
    {
        public UnspeakablyAwfulIndescribableHorror() :
            base(MunchkinDeluxeCards.Doors.UnspeakablyAwfulIndescribableHorror, "Unspeakably Awful Indescribable Horror", 14, 1, 4, 0, false)
        {
        }

        public override Task Play(Table gameContext)
        {
            var currentHero = gameContext.Players.Current;
            var currentHeroIsWarrior = currentHero.Equipped.OfType<WarriorClass>().Any();

            // TODO: check if current stage actually is a combat
            //var helpingHero = gameContext.Dungeon.Combat.HelpingPlayer;
            //var helpingHeroIsWarrior = helpingHero?.Equipped.OfType<WarriorClass>().Any();

            return base.Play(gameContext);
        }

        public override Table BadStuff(Table table, Player player)
        {
            ArgumentNullException.ThrowIfNull(table, nameof(table));
            ArgumentNullException.ThrowIfNull(player, nameof(player));

            throw new NotImplementedException();
        }
    }
}