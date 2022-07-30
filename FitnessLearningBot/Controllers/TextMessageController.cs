using Telegram.Bot;
using Telegram.Bot.Types;

namespace FitnessLearningBot.Controllers;

public class TextMessageController
{
    private ITelegramBotClient _telegramBotClient;

    public TextMessageController(ITelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
    }
    
    public async Task Handle(Message message, CancellationToken ct)
    {
        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
            $"Привет, пользователь {message.From.Username} {message.From.FirstName} {message.From.Id}",
            cancellationToken: ct);
    }
}