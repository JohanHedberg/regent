using Regent.AI.RPG.OpenAI.Models;
using Regent.AI.RPG.OpenAI.Services;

namespace Regent.AI.RPG.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly IOpenAIClient _openAiClient;

        public ChatGptService(IOpenAIClient openAIClient)
        {
            _openAiClient = openAIClient;
        }

        public async Task<string> GetIntroduction(string context)
        {
            var prompt = $"You are the game master for a role-playing game. Provide an engaging introduction to the scenario described below:\n\n{context}";

            var completionRequest = new CompletionRequest(prompt)
            {
                MaxTokens = 150,  // Adjust token count based on how long you want the introduction to be
                Temperature = 0.7F,  // Controls the creativity of the response,
                Model = "text-davinci-003"
            };

            var completionResult = await _openAiClient.CreateCompletionAsync(completionRequest);

            return completionResult.Choices[0].Text.Trim();
        }

        public async Task<string> ProcessPlayerAction(string playerName, string action)
        {
            var prompt = $"You are the game master for a role-playing game. The player '{playerName}' has decided to perform the following action: \"{action}\". Continue the story, describing how this action affects the narrative.";

            var completionRequest = new CompletionRequest(prompt)
            {
                MaxTokens = 150,  // Adjust token count based on how detailed you want the continuation to be
                Temperature = 0.7F,  // Controls the creativity of the response
            };

            var completionResult = await _openAiClient.CreateCompletionAsync(completionRequest);

            return completionResult.Choices[0].Text.Trim();
        }
    }
}
