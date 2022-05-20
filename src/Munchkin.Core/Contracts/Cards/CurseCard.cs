using Munchkin.Core.Model;
using System.Threading.Tasks;

namespace Munchkin.Core.Contracts.Cards
{
    public abstract class CurseCard : DoorsCard
    {
        protected CurseCard(string code, string title) : 
            base(code, title)
        {
        }

        public bool OneShot { get; protected set; }

        public override Task Play(Table state) => Task.CompletedTask;

        public abstract Task BadStuff(Table state);
    }
}