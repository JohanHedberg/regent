using System.Text.Json.Serialization;

namespace Regent.AI.RPG.OpenAI.Models
{
    public class CompletionRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 150;

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = 0.7f;

        [JsonPropertyName("top_p")]
        public float TopP { get; set; } = 1.0f;

        [JsonPropertyName("frequency_penalty")]
        public float FrequencyPenalty { get; set; } = 0.0f;

        [JsonPropertyName("presence_penalty")]
        public float PresencePenalty { get; set; } = 0.0f;

        [JsonPropertyName("stop")]
        public string[] Stop { get; set; }

        public CompletionRequest(string prompt)
        {
            Prompt = prompt;
        }
    }
}
