using System;

namespace Munchkin.Runtime.Abstractions.UserAggregate
{
    public class User
    {
        public User(int userId, string userName, bool isMale)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            UserId = userId;
            UserName = userName;
            IsMale = isMale;
        }

        public int UserId { get; }

        public string UserName { get; }

        public bool IsMale { get; }
    }
}
