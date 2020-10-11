using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public class ElfRace : RaceCard
    {
        public ElfRace() : base("Elf")
        {
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}