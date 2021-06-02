﻿using Munchkin.Api.ViewModels;
using Munchkin.Core.Model;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class PlayerExtensions
    {
        public static PlayerVM ToVM(this Player player)
        {
            return new PlayerVM
            {
                PlayerId = 0,
                Name = player.Name
            };
        }
    }
}