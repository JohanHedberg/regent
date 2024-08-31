using System.Text.Json.Serialization;

namespace Regent.AI.RPG.OpenAI.Models
{
    public class CompletionChoice
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("logProbs")]
        public string LogProbs { get; set; }

        [JsonPropertyName("finishReason")]
        public string FinishReason { get; set; }
    }
}