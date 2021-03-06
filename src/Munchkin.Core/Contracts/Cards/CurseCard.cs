using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class CurseCard : DoorsCard
    {
        protected CurseCard(string title) : base(title)
        {
        }

        public bool OneShot { get; protected set; }

        public override Task Play(Table state) => Task.CompletedTask;

        public abstract Task BadStuff(Table state);
    }
}