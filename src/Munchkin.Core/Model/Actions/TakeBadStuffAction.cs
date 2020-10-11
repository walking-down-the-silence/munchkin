using System;
using System.Threading.Tasks;

namespace Munchkin.Core.Model.Actions
{
    /// <summary>
    /// Takes badd stuff from monster right away
    /// </summary>
    public class TakeBadStuffAction : DynamicAction
    {
        public TakeBadStuffAction() : base("Take Bad Stuff", "")
        {
        }

        public override bool CanExecute(Table state)
        {
            throw new NotImplementedException();
        }

        public override async Task<Table> ExecuteAsync(Table state)
        {
            return state;
        }
    }
}