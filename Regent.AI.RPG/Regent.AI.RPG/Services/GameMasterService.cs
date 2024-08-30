using Microsoft.AspNetCore.SignalR;

namespace Regent.AI.RPG.Services
{
    public class GameMasterService : Hub
    {
        private static string narrative = "Welcome to the adventure!";

        public async Task SendAction(string playerName, string action)
        {
            // Update the narrative with the player's action
            narrative += $"\n{playerName}: {action}";

            // Broadcast the updated narrative to all clients
            await Clients.All.SendAsync("ReceiveNarrative", narrative);
        }
    }
}