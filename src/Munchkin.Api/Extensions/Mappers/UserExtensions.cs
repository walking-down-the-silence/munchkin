using Munchkin.Api.ViewModels;
using Munchkin.Runtime.Abstractions.UserAggregate;

namespace Munchkin.Api.Extensions.Mappers
{
    public static class UserExtensions
    {
        public static UserVM ToVM(this User user)
        {
            return new UserVM
            {
                UserId = user.UserId,
                Username = user.UserName,
                IsMale = user.IsMale
            };
        }
    }
}
