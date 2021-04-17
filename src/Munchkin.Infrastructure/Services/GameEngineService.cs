using MediatR;
using Munchkin.Core.Model;
using Munchkin.Core.Model.Enums;
using Munchkin.Infrastructure.Models;
using System.Linq;

namespace Munchkin.Infrastructure.Services
{
    public class GameEngineService
    {
        private readonly IMediator _mediator;
        private readonly IExpansionRegister _expansionRegister;

        public GameEngineService(
            IMediator mediator,
            IExpansionRegister expansionRegister)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _expansionRegister = expansionRegister ?? throw new System.ArgumentNullException(nameof(expansionRegister));
        }

        public GameEngine CreateEngine(GameRoom gameRoom)
        {
            var players = gameRoom.Players.Select(p => new Player(p.UserName, p.IsMale ? EGender.Male : EGender.Female)).ToArray();
            var expansions = gameRoom.SelectedExpansions;
            var selectedExpansions = _expansionRegister.SearchExpansions(x => expansions.Any(y => string.Equals(y.Title, x.Title)));
            return new GameEngine(_mediator, selectedExpansions, players);
        }
    }
}
