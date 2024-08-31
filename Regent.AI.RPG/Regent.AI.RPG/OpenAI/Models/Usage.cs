using System.Text.Json.Serialization;

namespace Regent.AI.RPG.OpenAI.Models
{
    public class Usage
    {
        [JsonPropertyName("promptTokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("completionTokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("totalTokens")]
        public int TotalTokens { get; set; }
    }
}