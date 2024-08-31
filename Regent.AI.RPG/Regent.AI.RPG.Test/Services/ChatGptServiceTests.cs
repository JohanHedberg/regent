using Moq;
using Regent.AI.RPG.OpenAI.Models;
using Regent.AI.RPG.OpenAI.Services;
using Regent.AI.RPG.Services;

namespace Regent.AI.RPG.Test.Services
{
    public class ChatGptServiceTests
    {
        private readonly Mock<IOpenAIClient> _mockOpenAiClient;
        private readonly ChatGptService _chatGptService;

        public ChatGptServiceTests()
        {
            _mockOpenAiClient = new Mock<IOpenAIClient>();
            _chatGptService = new ChatGptService(_mockOpenAiClient.Object);
        }

        [Fact]
        public async Task GetIntroduction_ReturnsCorrectIntroductionText()
        {
            // Arrange
            var expectedText = "Welcome to your adventure in the lost dungeons!";
            var response = new CompletionResponse
            {
                Choices = new CompletionChoice[] { new() { Text = expectedText } }
            };

            _mockOpenAiClient.Setup(x => x.CreateCompletionAsync(It.IsAny<CompletionRequest>()))
                             .ReturnsAsync(response);
            var context = "Start of a new journey...";

            // Act
            var result = await _chatGptService.GetIntroduction(context);

            // Assert
            Assert.Equal(expectedText, result);
        }

        [Fact]
        public async Task ProcessPlayerAction_ReturnsNarrativeUpdate()
        {
            // Arrange
            var expectedText = "Aragorn cautiously moves forward into the shadow.";
            var response = new CompletionResponse
            {
                Choices = new CompletionChoice[] { new() { Text = expectedText } }
            };

            _mockOpenAiClient.Setup(x => x.CreateCompletionAsync(It.IsAny<CompletionRequest>()))
                             .ReturnsAsync(response);
            var playerName = "Aragorn";
            var action = "cautiously moves forward";

            // Act
            var result = await _chatGptService.ProcessPlayerAction(playerName, action);

            // Assert
            Assert.Equal(expectedText, result);
        }
    }
}