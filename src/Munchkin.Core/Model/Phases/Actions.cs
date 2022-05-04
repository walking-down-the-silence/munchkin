namespace Munchkin.Core.Model
{
    public class TurnActions
    {
        public const string NextTurn = "munchkin.action.next-turn";

        public class Dungeon
        {
            public static string[] All = new[]
            {
                KickOpenTheDoor,
                LootTheRoom,
                LookForTrouble,
                PlayCard
            };

            public const string KickOpenTheDoor = "munchkin.action.dungeon.kick-open-the-door";
            public const string LootTheRoom = "munchkin.action.dungeon.loot-the-room";
            public const string LookForTrouble = "munchkin.action.dungeon.look-for-trouble";
            public const string PlayCard = "munchkin.action.dungeon.card.play";
        }

        public class Curse
        {
            public static string[] All = new[]
            {
                ResolveWhileInDungeon,
                ResolveWhileInCombat,
                TakeBadStuffFromCurse
            };

            public const string ResolveWhileInDungeon = "munchkin.action.curse.resolve-in-dungeon";
            public const string ResolveWhileInCombat = "munchkin.action.curse.resolve-in-combat";
            public const string TakeBadStuffFromCurse = "munchkin.action.curse.take-bad-stuff";
        }

        public class Combat
        {
            public static string[] All = new[]
            {
                BindCard,
                UnbindCard,
                AskForHelp,
                RejectHelp,
                AcceptHelp,
                RunAway,
                RollTheDice,
                TakeBadStuffFromMonster,
                Reward
            };

            public const string BindCard = "munchkin.action.combat.card.bind";
            public const string UnbindCard = "munchkin.action.combat.card.unbind";
            public const string AskForHelp = "munchkin.action.combat.help.ask";
            public const string RejectHelp = "munchkin.action.combat.help.reject";
            public const string AcceptHelp = "munchkin.action.combat.help.accept";
            public const string RunAway = "munchkin.action.combat.monster.run-away";
            public const string RollTheDice = "munchkin.action.combat.monster.run-away.roll-the-dice";
            public const string TakeBadStuffFromMonster = "munchkin.action.combat.monster.take-bad-stuff";
            public const string Reward = "munchkin.action.combat.reward";
        }

        public class Trade
        {
            public static string[] All = new[]
            {
                InitiateTrade,
                LeftSideOffer,
                LeftSideRemove,
                LeftSideAccept,
                LeftSideReject,
                RightSideOffer,
                RightSideRemove,
                RightSideAccept,
                RightSideReject
            };

            public const string InitiateTrade = "munchkin.action.trade.initiate";
            public const string LeftSideOffer = "munchkin.action.trade.left-side.add-to-offer";
            public const string LeftSideRemove = "munchkin.action.trade.left-side.remove-from-offer";
            public const string LeftSideAccept = "munchkin.action.trade.left-side.accept";
            public const string LeftSideReject = "munchkin.action.trade.left-side.reject";
            public const string RightSideOffer = "munchkin.action.trade.right-side.add-to-offer";
            public const string RightSideRemove = "munchkin.action.trade.right-side.remove-from-offer";
            public const string RightSideAccept = "munchkin.action.trade.right-side.accept";
            public const string RightSideReject = "munchkin.action.trade.right-side.reject";
        }

        public class Death
        {
            public static string[] All = new[]
            {
                LootTheBody
            };

            public const string LootTheBody = "munchkin.action.death.loot-the-body";
        }

        public class Player
        {
            public static string[] All = new[]
            {
                Equip,
                TakeInHand,
                PutInBackpack,
                GiveAway,
                DiscardDoor,
                DiscardTreasure,
                Sell
            };

            public const string Equip = "munchkin.action.equip";
            public const string TakeInHand = "munchkin.action.take-in-hand";
            public const string PutInBackpack = "munchkin.action.put-in-backpack";
            public const string GiveAway = "munchkin.action.give-away";
            public const string DiscardDoor = "munchkin.action.dicard-door";
            public const string DiscardTreasure = "munchkin.action.discard-treasure";
            public const string Sell = "munchkin.action.treasure.sell";
        }
    }
}
