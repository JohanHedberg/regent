using Regent.AI.RPG.OpenAI.Models;

namespace Regent.AI.RPG.OpenAI.Services
{
    public interface IOpenAIClient
    {
        Task<CompletionResponse> CreateCompletionAsync(CompletionRequest completionRequest);
    }
}