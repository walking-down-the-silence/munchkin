namespace Munchkin.Api.ViewModels
{
    public class CardOwnerReferenceVM
    {
        /// <summary>
        /// Get the owner type for the card.
        /// Owner types: player, teasures deck, doors deck, treasures discard pile, doors discard pile.
        /// </summary>
        public string OwnerType { get; set; }
    }
}
