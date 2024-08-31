namespace Regent.AI.RPG.Services
{
    public interface IChatGptService
    {
        /// <summary>
        /// Gets an introduction text for a role playing game scenario, to be presented for the
        /// players.
        /// </summary>
        /// <param name="context">The prompt for the AI API to produce the introduction text.</param>
        /// <returns>The text with the game introduction.</returns>
        Task<string> GetIntroduction(string context);

        /// <summary>
        /// Processes the input from a player by taking his name and action to perform, by sending it
        /// to Open AI API.
        /// </summary>
        /// <param name="playerName">The name of the player character.</param>
        /// <param name="action">The action the character performs in the game.</param>
        /// <returns>Returns the continued narrative of the story including the character performing his actions
        /// and how that affected the story.</returns>
        Task<string> ProcessPlayerAction(string playerName, string action);
    }
}