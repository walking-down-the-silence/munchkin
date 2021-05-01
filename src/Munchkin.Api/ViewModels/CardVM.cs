namespace Munchkin.Api.ViewModels
{
    public class CardVM
    {
        public int CardId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public CardOwnerVM Owner { get; set; }
    }
}
