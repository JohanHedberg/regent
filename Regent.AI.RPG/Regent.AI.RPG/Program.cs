using Microsoft.AspNetCore.Components.Web;
using InteractiveRPG;
using InteractiveRPG.Services;
using InteractiveRPG.Models;

var builder = WebApplication.CreateBuilder(args);
//var builder = WebAssemblyHostBuilder.CreateDefault(args);
//builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { });
builder.Services.AddSingleton<GameState>();
builder.Services.AddSingleton<DialogueService>();
builder.Services.AddSingleton<ImageGenerationService>();

await builder.Build().RunAsync();

// Models/Character.cs
namespace InteractiveRPG.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public Dictionary<string, int> Attributes { get; set; }

        public Character()
        {
            Attributes = new Dictionary<string, int>
            {
                {"Strength", 10},
                {"Dexterity", 10},
                {"Constitution", 10},
                {"Intelligence", 10},
                {"Wisdom", 10},
                {"Charisma", 10}
            };
        }
    }
}

// Services/GameState.cs

namespace InteractiveRPG.Services
{
    public class GameState
    {
        public Character CurrentCharacter { get; private set; }
        public List<string> DialogueHistory { get; private set; } = new List<string>();

        public void CreateCharacter(string name, string characterClass)
        {
            CurrentCharacter = new Character
            {
                Name = name,
                Class = characterClass,
                Level = 1,
                HealthPoints = 10
            };
        }

        public void AddDialogue(string message, bool isAI)
        {
            DialogueHistory.Add($"{(isAI ? "AI" : "Player")}: {message}");
        }
    }
}

// Services/DialogueService.cs
namespace InteractiveRPG.Services
{
    public class DialogueService
    {
        // This is a placeholder for actual AI integration
        public async Task<string> GetAIResponse(string playerInput)
        {
            // In a real implementation, this would call an AI service
            await Task.Delay(1000); // Simulating API call
            return $"AI response to: {playerInput}";
        }
    }
}

// Services/ImageGenerationService.cs
namespace InteractiveRPG.Services
{
    public class ImageGenerationService
    {
        // This is a placeholder for actual image generation integration
        public async Task<string> GenerateImage(string prompt)
        {
            // In a real implementation, this would call an image generation service
            await Task.Delay(1000); // Simulating API call
            return $"https://via.placeholder.com/800x400?text={Uri.EscapeDataString(prompt)}";
        }
    }
}