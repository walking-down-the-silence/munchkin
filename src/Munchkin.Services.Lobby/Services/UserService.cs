using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository clusterClient)
        {
            _userRepository = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public async Task<User> CreateUserAsync(int userId, string nickname, bool isMale)
        {
            var user = new User(userId, nickname, isMale);
            await _userRepository.SaveUserAsync(user);
            return user;
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            return _userRepository.GetUserByIdAsync(userId);
        }
    }
}
