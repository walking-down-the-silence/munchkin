using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.UserAggregate
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
