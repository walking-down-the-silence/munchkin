using System;

namespace Munchkin.Runtime.Entities.UserAggregate
{
    public class User
    {
        public User(int userId, string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            UserId = userId;
            UserName = userName;
        }

        public int UserId { get; }

        public string UserName { get; }

        public bool IsMale { get; }
    }
}
