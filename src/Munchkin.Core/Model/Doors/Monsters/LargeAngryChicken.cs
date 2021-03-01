using System;
using System.Linq;
using System.Threading.Tasks;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class LargeAngryChicken : MonsterCard
    {
        public LargeAngryChicken() : base("Large Angry Chicken", 2, 1, 1, 0, false)
        {
        }

        public override Task Play(Table state)
        {
            // TODO: Add logic "gain an extra level if you defeat it with fire or flame"

            throw new NotImplementedException();
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();

            return Task.CompletedTask;
        }
    }
}