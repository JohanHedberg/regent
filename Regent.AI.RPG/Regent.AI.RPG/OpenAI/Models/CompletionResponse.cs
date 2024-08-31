using System.Text.Json.Serialization;

namespace Regent.AI.RPG.OpenAI.Models
{
    public class CompletionResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("choices")]
        public CompletionChoice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }
}
