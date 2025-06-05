using TaskSlayerfrontendTGBot.Interface.IServices;

namespace TaskSlayerfrontendTGBot
{
    internal class App
    {
        IBotServices _bot;
        public App(IBotServices bot) 
        {
            _bot = bot;
        }

        public async Task Run()
        {
            await _bot.Start();
        }
    }
}
