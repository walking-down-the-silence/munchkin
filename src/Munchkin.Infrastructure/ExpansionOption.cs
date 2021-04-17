namespace Munchkin.Infrastructure
{
    public class ExpansionOption
    {
        public ExpansionOption(string code, string title)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new System.ArgumentException($"'{nameof(code)}' cannot be null or whitespace.", nameof(code));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new System.ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
            }

            Code = code;
            Title = title;
        }

        public string Code { get; }

        public string Title { get; }
    }
}