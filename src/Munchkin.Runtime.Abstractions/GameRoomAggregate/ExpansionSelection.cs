namespace Munchkin.Runtime.Abstractions.GameRoomAggregate
{
    public record ExpansionSelection
    {
        public ExpansionSelection(string code, string title, bool selected)
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
            Selected = selected;
        }

        public string Code { get; }

        public string Title { get; }

        public bool Selected { get; }
    }
}