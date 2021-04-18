using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Runtime;
using Munchkin.Runtime.Entities.GameRoomAggregate;
using Munchkin.Runtime.Entities.UserAggregate;
using System;
using System.Linq;

namespace Munchkin.Infrastructure.Services
{
    public class GameEngineService
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _expansionProvider;

        public GameEngineService(
            IMediator mediator,
            IServiceProvider expansionRegister)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _expansionProvider = expansionRegister ?? throw new ArgumentNullException(nameof(expansionRegister));
        }

        public GameEngine CreateEngine(GameRoom gameRoom)
        {
            var players = gameRoom.Players.Select(ToPlayer).ToArray();
            var selectedExpansions = _expansionProvider
                .GetServices<IExpansion>()
                .Where(x => gameRoom.SelectedExpansions.Any(y => string.Equals(y.Code, x.Code)))
                .ToArray();
            return new GameEngine(_mediator, selectedExpansions, players);
        }

        private static Player ToPlayer(User p)
        {
            return new Player(p.UserName, p.IsMale ? EGender.Male : EGender.Female);
        }
    }
}
