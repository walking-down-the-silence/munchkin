using System;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;

namespace Munchkin.Core.Model.Cards.Doors.Monsters
{
    public sealed class Lawyers : MonsterCard
    {
        public Lawyers() : base("Lawyers", 6, 1, 2, 0, false)
        {
        }

        public override Task BadStuff(Table gameContext)
        {
            throw new NotImplementedException();
        }
    }
}