using Munchkin.Core.Model;
using Munchkin.Core.Model.Expansions;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Munchkin.Runtime.Abstractions
{
    /// <summary>
    /// Defines a virtual table and available features of the table where players can play the game.
    /// </summary>
    public interface ITable : IGrainWithStringKey
    {
        /// <summary>
        /// Prepares the table for the game by dealing the cards to the players and shuffling the decks.
        /// </summary>
        /// <returns>The table grain instance.</returns>
        Task<ITable> SetupAsync();

        /// <summary>
        /// Gets the collection of players sitting around the table.
        /// </summary>
        /// <returns>A collection of the players.</returns>
        Task<IReadOnlyCollection<Player>> GetPlayersAsync();

        /// <summary>
        /// Gets a single player sitting beind the table.
        /// </summary>
        /// <param name="nickname">The nickname of the player to get.</param>
        /// <returns>The player instance.</returns>
        Task<Player> GetPlayerByIdAsync(string nickname);

        /// <summary>
        /// Add a player to the table to player the game.
        /// </summary>
        /// <param name="player">The player that joins the game.</param>
        /// <returns>The result of player trying to joing the game.</returns>
        Task<JoinTableResult> JoinAsync(Player player);

        /// <summary>
        /// Removes a player from the table.
        /// </summary>
        /// <param name="player">The player that leaves the game.</param>
        /// <returns>The result of player trying to leave the game.</returns>
        Task<JoinTableResult> LeaveAsync(Player player);

        /// <summary>
        /// Gets a list of available expansions in the game.
        /// </summary>
        /// <returns>A collection of expansions.</returns>
        Task<IReadOnlyCollection<ExpansionSelection>> GetAvailableExpansionsAsync();

        /// <summary>
        /// Gets a list of expansions included into the game by players.
        /// </summary>
        /// <returns>A collection of expansions.</returns>
        Task<IReadOnlyCollection<ExpansionSelection>> GetIncludedExpansionsAsync();

        /// <summary>
        /// Includes the expansion into the game.
        /// </summary>
        /// <param name="code">The code of the expansion.</param>
        /// <returns>The result of the player trying to include an expansion.</returns>
        Task<SelectExpansionResult> IncludeExpansionAsync(string code);

        /// <summary>
        /// Excludes the expansions from the game.
        /// </summary>
        /// <param name="code">The code of the expansion.</param>
        /// <returns>The result of the player trying to include an expansion.</returns>
        Task<SelectExpansionResult> ExcludeExpansionAsync(string code);
    }
}
