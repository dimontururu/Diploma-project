using Application.Bot;

namespace TaskSlayerfrontendTGBot
{
    internal class App
    {
        IBotService _bot;
        public App(IBotService bot) 
        {
            _bot = bot;
        }

        public async Task Run()
        {
            await _bot.Start();
        }
    }
}
