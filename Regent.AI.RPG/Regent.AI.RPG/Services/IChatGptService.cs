namespace Regent.AI.RPG.Services
{
    public interface IChatGptService
    {
        Task<string> GetIntroduction(string context);

        Task<string> ProcessPlayerAction(string context);
    }
}