using Munchkin.Core.Cards.Effects;
using Munchkin.Core.Cards.Rules;
using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Doors
{
    public sealed class MaulRat : MonsterCard
    {
        public MaulRat() : base("Maul Rat", 1, 1, 1, 0, false)
        {
            AddEffect(Effect
                .New(new MonsterStrengthBonusEffect(3))
                .With(() => Rule
                    .New(new HasClericClassRule())));
        }

        public override Task BadStuff(Table state)
        {
            state.Players.Current.LevelDown();
            return Task.CompletedTask;
        }
    }
}