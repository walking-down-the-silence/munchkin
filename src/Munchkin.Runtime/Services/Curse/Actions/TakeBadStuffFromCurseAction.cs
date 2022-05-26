using Munchkin.Core.Contracts.Actions;
using Munchkin.Core.Contracts.Cards;
using System;

namespace Munchkin.Core.Model.Phases
{
    public sealed class TakeBadStuffFromCurseAction : ActionBase, ICurseAction
    {
        public TakeBadStuffFromCurseAction(CurseCard curse) :
            base(TurnActions.Curse.TakeBadStuff, "TakenBy Bad Stuff")
        {
            Curse = curse ?? throw new ArgumentNullException(nameof(curse));
        }

        public CurseCard Curse { get; }
    }
}