using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Regent.AI.RPG.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string OpenAIEndpoint = "https://api.openai.com/v1/completions";
        private string _conversationHistory;

        public ChatGptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = "your_openai_api_key";  // Replace with your actual OpenAI API key
            _conversationHistory = string.Empty;
        }

        public async Task<string> GetIntroduction(string context)
        {
            // Call OpenAI to generate the introduction
            _conversationHistory += $"DM: {context}\n";
            
            var request = new
            {
                model = "text-davinci-003",  // Specify the model you want to use
                prompt = _conversationHistory,
                max_tokens = 150,
                temperature = 0.7,
                top_p = 1.0,
                frequency_penalty = 0.0,
                presence_penalty = 0.0
            };

            var response = await _httpClient.PostAsJsonAsync(OpenAIEndpoint, request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var completionResponse = JsonConvert.DeserializeObject<OpenAICompletionResponse>(result);
                var aiResponse = completionResponse.Choices[0].Text.Trim();

                _conversationHistory += $"AI: {aiResponse}\n";
                return aiResponse;
            }
            else
            {
                return "An error occurred while generating the background information.";
            }
        }

        public async Task<string> ProcessPlayerAction(string context, string playerName, string action)
        {
            // This function will also interact with OpenAI to continue the conversation based on player's action
            _conversationHistory += $"Player: {context}\n";
            
            var request = new
            {
                model = "text-davinci-003",
                prompt = _conversationHistory,
                max_tokens = 150,
                temperature = 0.7,
                top_p = 1.0,
                frequency_penalty = 0.0,
                presence_penalty = 0.0
            };

            var response = await _httpClient.PostAsJsonAsync(OpenAIEndpoint, request);


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var completionResponse = JsonConvert.DeserializeObject<OpenAICompletionResponse>(result);
                var aiResponse = completionResponse.Choices[0].Text.Trim();

                _conversationHistory += $"AI: {aiResponse}\n";
                return aiResponse;
            }
            else
            {
                return "An error occurred while processing the player's action.";
            }
        }
    }

    public class OpenAICompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public string Model { get; set; }
        public Choice[] Choices { get; set; }
        public Usage Usage { get; set; }
    }

    public class Choice
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public string Logprobs { get; set; }
        public string FinishReason { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }
}
