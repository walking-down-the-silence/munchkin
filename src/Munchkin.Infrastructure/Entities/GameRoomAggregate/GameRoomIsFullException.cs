using System;
using System.Runtime.Serialization;

namespace Munchkin.Infrastructure.Models
{
    [Serializable]
    internal class GameRoomIsFullException : Exception
    {
        public GameRoomIsFullException()
        {
        }

        public GameRoomIsFullException(string message) : base(message)
        {
        }

        public GameRoomIsFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameRoomIsFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}