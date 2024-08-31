using Moq;
using Moq.Protected;
using Regent.AI.RPG.OpenAI.Models;
using Regent.AI.RPG.OpenAI.Services;
using System.Net;
using System.Text;

namespace Regent.AI.RPG.Test.OpenAI.Services
{
    public class OpenAIClientTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly OpenAIClient _openAiClient;

        public OpenAIClientTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.openai.com/v1/")
            };

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _openAiClient = new OpenAIClient(_httpClientFactoryMock.Object, "fake-api-key");
        }

        [Fact]
        public async Task CreateCompletionAsync_ReturnsCompletionResponse_WhenSuccessful()
        {
            // Arrange
            var responseContent = "{\"id\":\"cmpl-123\",\"object\":\"text_completion\",\"created\":1589478378,\"model\":\"text-davinci-003\",\"choices\":[{\"text\":\"This is a test response.\",\"index\":0,\"logprobs\":null,\"finish_reason\":\"stop\"}],\"usage\":{\"prompt_tokens\":5,\"completion_tokens\":7,\"total_tokens\":12}}";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponseMessage);

            var completionRequest = new CompletionRequest("Test prompt");

            // Act
            var result = await _openAiClient.CreateCompletionAsync(completionRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("cmpl-123", result.Id);
            Assert.Equal("This is a test response.", result.Choices[0].Text.Trim());
        }

        [Fact]
        public async Task CreateCompletionAsync_ThrowsHttpRequestException_WhenRequestFails()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponseMessage);

            var completionRequest = new CompletionRequest("Test prompt");

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _openAiClient.CreateCompletionAsync(completionRequest));
        }
    }
}
