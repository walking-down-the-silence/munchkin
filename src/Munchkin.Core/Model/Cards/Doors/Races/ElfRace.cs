using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model.Attributes;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public class ElfRace : RaceCard
    {
        public ElfRace() :
            base(MunchkinDeluxeCards.Doors.ElfRace1, "Elf")
        {
            AddAttribute(new ElfAttribute());
            AddAttribute(new RunAwayBonusAttribute(1));
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}