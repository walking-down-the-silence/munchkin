﻿using MediatR;
using Munchkin.Core.Contracts;
using Munchkin.Core.Model.Requests;
using Munchkin.Runtime.Abstractions.Actions;
using System.Threading;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Handlers
{
    internal class GameWaitForPlayerHandler : IRequestHandler<GameWaitForPlayerRequest, Response<Unit>>
    {
        private readonly IPlayerActionRepository _playerActionRepository;

        public GameWaitForPlayerHandler(IPlayerActionRepository playerActionRepository)
        {
            _playerActionRepository = playerActionRepository ?? throw new System.ArgumentNullException(nameof(playerActionRepository));
        }

        public async Task<Response<Unit>> Handle(GameWaitForPlayerRequest request, CancellationToken cancellationToken)
        {
            var (source, response) = Response<Unit>.Create();

            return response;
        }
    }
}
