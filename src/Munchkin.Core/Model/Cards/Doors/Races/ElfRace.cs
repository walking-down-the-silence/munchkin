using Munchkin.Core.Contracts.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class ElfRace : RaceCard
    {
        public ElfRace() :
            base(MunchkinDeluxeCards.Doors.ElfRace1, "Elf")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}