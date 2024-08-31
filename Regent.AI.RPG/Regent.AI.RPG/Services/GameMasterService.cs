﻿using Microsoft.AspNetCore.SignalR;

namespace Regent.AI.RPG.Services
{
    public class GameMasterService : Hub
    {
        public string gameContext = "Welcome to the game.";
        private static string ChatGPTGameSetupString = @"Let's play a RPG scenario. 
        You are the DM and will set the scene and guide me through this short adventure. 
        The setting 
        It is set at Meliá Palma Bay Hotell in Palma, Mallorca during contemporary time. A hot late summer weekend a Swedish consultant company is holding a conference about AI.
        The story
        should be about an AI conference gone terribly wrong and I as the participant has to make it out of Palma alive.";
        
        private readonly IChatGptService _chatGpt;

        public GameMasterService(IChatGptService chatGpt)
        {
            _chatGpt = chatGpt;
            //gameContext = _chatGpt.GetIntroduction(ChatGPTGameSetupString).Result;
            gameContext = "You are on a conference somewhere in Mallorca";
        }

        public async Task SendAction(string playerName, string action)
        {
            //var narrative = await _chatGpt.ProcessPlayerAction(gameContext, playerName, action);

            var narrative = $"{playerName} {action}s.";

            // Update the narrative with the player's action
            //narrative += $"\n{playerName}: {action}";

            // Broadcast the updated narrative to all clients
            await Clients.All.SendAsync("ReceiveNarrative", narrative);
        }
    }
}