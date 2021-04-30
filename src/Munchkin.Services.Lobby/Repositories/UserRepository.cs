using Munchkin.Runtime.Abstractions.UserAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<int, User> _users = new();

        public Task<User> GetUserByIdAsync(int userId)
        {
            return _users.ContainsKey(userId)
                ? Task.FromResult(_users[userId])
                : Task.FromResult<User>(null);
        }

        public Task SaveUserAsync(User user)
        {
            _ = user is not null
                ? (_users[user.UserId] = user)
                : null;
            return Task.CompletedTask;
        }
    }
}
