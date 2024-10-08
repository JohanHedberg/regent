﻿using Microsoft.AspNetCore.SignalR;

namespace Regent.AI.RPG.Services
{
    public class GameMasterService : Hub
    {
        private string _initialPrompt = @"Let's play a RPG scenario. 
        You are the DM and will set the scene and guide me through this short adventure. 
        The setting 
        It is set at Meliá Palma Bay Hotell in Palma, Mallorca during contemporary time. A hot late summer weekend a Swedish consultant company is holding a conference about AI.
        The story
        should be about an AI conference gone terribly wrong and I as the participant has to make it out of Palma alive.";

        private readonly IChatGptService _chatGpt;

        public GameMasterService(IChatGptService chatGpt)
        {
            _chatGpt = chatGpt;
        }

        public async Task StartGame()
        {
            var narrative = await _chatGpt.GetIntroduction(_initialPrompt);

            // Broadcast the updated narrative to all clients
            await Clients.All.SendAsync("ReceiveNarrative", narrative);
        }

        public async Task SendAction(string playerName, string action)
        {
            var narrative = await _chatGpt.ProcessPlayerAction(playerName, action);

            // Broadcast the updated narrative to all clients
            await Clients.All.SendAsync("ReceiveNarrative", narrative);
        }
    }
}