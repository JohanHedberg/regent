using Regent.AI.RPG.OpenAI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Regent.AI.RPG.OpenAI.Services
{
    public class OpenAIClient : IOpenAIClient
    {
        private readonly HttpClient _httpClient;

        public OpenAIClient(IHttpClientFactory httpClientFactory, string apiKey)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<CompletionResponse> CreateCompletionAsync(CompletionRequest completionRequest)
        {
            var jsonRequest = JsonSerializer.Serialize(completionRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("completions", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"OpenAI request failed: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CompletionResponse>(jsonResponse);
        }
    }
}
