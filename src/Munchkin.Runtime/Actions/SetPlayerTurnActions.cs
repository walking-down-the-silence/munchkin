using MediatR;
using Munchkin.Core.Contracts.Actions;
using System.Collections.Generic;

namespace Munchkin.Runtime.Actions
{
    internal record SetPlayerTurnActions(IReadOnlyCollection<IAction> Actions) : 
        ActionBase("munchkin.action.turn.set-player-actions", "Set Player Actions", string.Empty),
        INotification;
}
