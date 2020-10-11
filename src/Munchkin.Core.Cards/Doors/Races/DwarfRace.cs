﻿using Munchkin.Core.Cards.Traits;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Cards;
using System;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
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
