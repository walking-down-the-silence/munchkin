using Munchkin.Core.Contracts.Cards;
using System.Threading.Tasks;

namespace Munchkin.Core.Model
{
    public sealed class FaceUpCard : Card
    {
        private readonly Card _originalCard;

        public FaceUpCard(Card originalCard) : base(originalCard.Title)
        {
            _originalCard = originalCard;
        }

        public FaceDownCard TurnFaceDown()
        {
            return new FaceDownCard(_originalCard);
        }

        public override Task Play(Table context)
        {
            _originalCard.Play(context);

            return Task.CompletedTask;
        }

        public override void Discard(Table context)
        {
            _originalCard.Discard(context);
        }
    }
}