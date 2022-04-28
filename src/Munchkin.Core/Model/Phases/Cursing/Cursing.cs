using Munchkin.Core.Contracts.Cards;
using Munchkin.Core.Extensions;
using Munchkin.Core.Model.Attributes;
using Munchkin.Core.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Munchkin.Core.Model.Phases
{
    public record Cursing(
        Table Table,
        Player Player,
        ImmutableArray<Card> TemporaryPile)
    {
        public static Cursing From(Table table, Player player)
        {
            return new Cursing(table, player, ImmutableArray<Card>.Empty);
        }

        public static Cursing Reduce(Cursing state, ICurseAction action)
        {
            return action switch
            {
                ResolveWhileInCombatAction resolveInCombat      => ResolveWhileInCombat(state, resolveInCombat.Card),
                ResolveWhileInDungeonAction resolveInDungeon    => ResolveWhileInDungeon(state, resolveInDungeon.Card),
                TakeBadStuffFromCurseAction takeBadStuff        => TakeBadStuff(state, takeBadStuff.Card),
                _                                               => throw new ArgumentOutOfRangeException(nameof(action))
            };
        }

        #region Calculated Properties

        public IEnumerable<CurseCard> Curses => TemporaryPile.OfType<CurseCard>();

        #endregion

        public static Cursing ResolveWhileInCombat(Cursing state, Card card)
        {
            var availableActions = ImmutableList.CreateRange(TurnActions.Combat.All);
            //return ActionResult.Create(nextState, availableActions);
            return ResolveCurse(state, card);
        }

        public static Cursing ResolveWhileInDungeon(Cursing state, Card card)
        {
            var availableActions = ImmutableList.CreateRange(new string[]
            {
                TurnActions.Dungeon.LootTheRoom,
                TurnActions.Dungeon.LookForTrouble
            });
            //return ActionResult.Create(nextState, availableActions);
            return ResolveCurse(state, card);
        }

        public static Cursing TakeBadStuff(Cursing state, CurseCard curse)
        {
            // TODO: pass the current player implicitly
            curse.BadStuff(state.Table);
            var availableActions = ImmutableList.CreateRange(new string[]
            {
                TurnActions.Dungeon.LootTheRoom,
                TurnActions.Dungeon.LookForTrouble
            });
            //return ActionResult.Create(state, availableActions);
            return state;
        }

        private static Cursing ResolveCurse(Cursing state, Card card)
        {
            // NOTE: remove from player's cards and add it to the temporary pile,
            // before the step is resolved completely
            if (!card.HasAttribute<CancelCurseAttribute>())
                throw new InvalidCardUsedException("The card used does not have the attribute for cancelling curses.");

            state.Player.Discard(card);
            return state;
        }
    }
}