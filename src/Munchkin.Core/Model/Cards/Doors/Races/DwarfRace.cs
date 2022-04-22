﻿using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Cards.Doors.Races
{
    public sealed class DwarfRace : RaceCard
    {
        public DwarfRace() : base("Dwarf")
        {
            AddProperty(new CarryAnyAmountOfBigItemsAttribute());
        }

        public override Task Play(Table context)
        {
            throw new NotImplementedException();
        }
    }
}
