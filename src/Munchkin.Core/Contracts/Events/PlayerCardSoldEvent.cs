﻿using Munchkin.Core.Model.Attributes;

namespace Munchkin.Core.Contracts.Events
{
    public sealed class PlayerCardSoldEvent : EventSupportingAttributes
    {
        public PlayerCardSoldEvent(string playerNickname, string cardId, int GoldPieces)
        {
            PlayerNickname = playerNickname;
            CardId = cardId;

            AddAttribute(new GoldPiecesAttribute(GoldPieces));
        }

        public string PlayerNickname { get; }

        public string CardId { get; }
    }
}
