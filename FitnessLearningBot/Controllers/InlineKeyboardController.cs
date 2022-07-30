using Telegram.Bot;
using Telegram.Bot.Types;

namespace FitnessLearningBot.Controllers;

public class InlineKeyboardController
{
    private ITelegramBotClient _telegramBotClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        
    }
}