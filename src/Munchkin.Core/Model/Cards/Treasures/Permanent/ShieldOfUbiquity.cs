using Munchkin.Core.Contracts;
using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Contracts.Rules;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Effects;
using Munchkin.Core.Model.Enums;
using Munchkin.Core.Model.Rules;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Engine.Original.Treasures
{
    public sealed class ShieldOfUbiquity : PermanentItemCard
    {
        public ShieldOfUbiquity() : base("Shield Of Ubiquity", 0, 0, EItemSize.Big, EWearingType.OneHanded, 600)
        {
            AddProperty(new WarriorOnlyRestriction());
            AddEffect(Effect
                .New(new PlayerStrengthBonusEffect(4))
                .With(() => Rule
                    .New(new CanCarryBigItemRule())
                    .And(new HasOneFreeHandRule())
                    .And(new HasWarriorClassRule())));
        }

        public override Task Play(Table context)
        {
            // apply dynamic effects if conditions are met
            Effects.Where(effect => effect.Satisfies(context)).ForEach(effect => effect.Apply(context));

            return Task.CompletedTask;
        }
    }
}