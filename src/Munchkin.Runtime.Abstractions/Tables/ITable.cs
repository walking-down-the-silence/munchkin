using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions.Tables
{
    public interface ITable : IGrainWithStringKey
    {
        Task<IReadOnlyCollection<Player>> GetPlayers();

        Task<JoinTableResult> JoinRoom(Player player);

        Task<JoinTableResult> LeaveRoom(Player player);

        Task WithExpansions(IReadOnlyCollection<ExpansionOption> expansions);

        Task<IReadOnlyCollection<ExpansionSelection>> GetExpansionSelections();

        Task<SelectExpansionResult> SelectExpansion(string code);

        Task<SelectExpansionResult> UnselectExpansion(string code);
    }
}
