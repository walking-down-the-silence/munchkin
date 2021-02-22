using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public sealed class FaceDownCard : Card
    {
        private readonly Card _originalCard;
        private bool _isHidden;

        public FaceDownCard(Card originalCard): base(originalCard is DoorsCard ? "Door" : "Treasure")
        {
            _originalCard = originalCard ?? throw new System.ArgumentNullException(nameof(originalCard));
        }

        public FaceUpCard TurnFaceUp()
        {
            _isHidden = false;
            return new FaceUpCard(_originalCard);
        }

        public override Task Play(Table context)
        {
            if (!_isHidden)
            {
                _originalCard.Play(context);
            }

            return Task.CompletedTask;
        }

        public override void Discard(Table context)
        {
            if (!_isHidden)
            {
                _originalCard.Discard(context);
            }
        }
    }
}
