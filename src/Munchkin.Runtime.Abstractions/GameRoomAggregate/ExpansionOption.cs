namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public class ExpansionOption
    {
        public ExpansionOption(string code, string title)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new System.ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));
            }

            if (string.IsNullOrEmpty(title))
            {
                throw new System.ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            }

            Code = code;
            Title = title;
        }

        public string Code { get; }

        public string Title { get; }
    }
}