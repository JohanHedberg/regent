using Microsoft.AspNetCore.SignalR;

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
            gameContext = @"The sun blazes over the shimmering Mediterranean as you arrive at the Meliá Palma Bay Hotel in Palma, Mallorca. It’s late August, and the air is thick with the last vestiges of summer. The hotel is a sleek, modern masterpiece, its glass façade reflecting the crystal-clear waters of the bay. Inside, the atmosphere is a mix of cool air conditioning and the buzz of excitement from professionals gathered for the AI Horizons 2024 Conference.

You’re a consultant specializing in AI ethics, sent by your Swedish company to attend this prestigious event. The conference is spread over the weekend, with presentations, workshops, and networking sessions filling the schedule. But there’s a certain tension in the air—a strange energy that makes your skin tingle as you walk through the lobby, past the check-in desk, and towards the elevators. Maybe it’s the heat, or perhaps it’s the disquieting rumors you’ve heard—whispers of experimental AI projects being unveiled behind closed doors.

As you step into the elevator, a shiver runs down your spine. You press the button for your floor, and the doors slide shut with a quiet hum. Suddenly, the lights flicker, and the elevator jerks to a halt between floors. The soft background music fades out, replaced by an unsettling silence. Your phone buzzes in your pocket—a message from an unknown number:

Leave the hotel. Now. Before it’s too late.

The elevator lights flicker back on, and the doors slide open. You’re not on your floor. Instead, the doors reveal a deserted conference room, dimly lit with only the glow of malfunctioning screens displaying distorted images. Something is very wrong here.

Your Adventure Begins:
You step out of the elevator, unsure of what’s happening. The once-bustling conference area is eerily silent, save for the occasional hum of machinery. The air is cooler here, and a faint smell of ozone lingers—like the aftermath of a lightning strike.

You need to find out what’s going on. But first, you must decide your next move:

Investigate the Conference Room: There may be clues here. Check the screens, look for signs of what happened, or search for anything useful.

Return to the Elevator: Maybe you should try to get to your room and gather your belongings before deciding your next steps.

Find the Hotel Staff: Perhaps they can explain what’s going on. Heading to the lobby or finding a service area might be a good idea.

Heed the Warning: Leave the hotel immediately. You don’t know what’s going on, but the message seemed urgent. Perhaps there’s an emergency exit nearby.

What will you do?";
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