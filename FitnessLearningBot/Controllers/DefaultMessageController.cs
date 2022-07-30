using Telegram.Bot;
using Telegram.Bot.Types;

namespace FitnessLearningBot.Controllers;

public class DefaultMessageController
{
    private ITelegramBotClient _telegramBotClient;

    public DefaultMessageController(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }
    
    public async Task Handle(Message message, CancellationToken ct)
    {
        
    }
}