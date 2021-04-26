using Munchkin.Runtime.Abstractions.UserAggregate;
using System;
using System.Threading.Tasks;

namespace Munchkin.Services.Lobby.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
