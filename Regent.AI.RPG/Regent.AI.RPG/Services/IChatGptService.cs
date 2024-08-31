namespace Regent.AI.RPG.Services
{
    public interface IChatGptService
    {
        string GetIntroduction(string context);

        string ProcessPlayerAction(string context);
    }
}