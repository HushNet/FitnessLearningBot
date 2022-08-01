using FitnessLearningBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FitnessLearningBot.Controllers;

public class TextMessageController
{
    private ITelegramBotClient _telegramBotClient;
    private IUserStorage _userStorage;

    public TextMessageController(ITelegramBotClient telegramBotClient, IUserStorage userStorage)
    {
        _telegramBotClient = telegramBotClient;
        _userStorage = userStorage;
    }
    
    public async Task Handle(Message message, CancellationToken ct)
    {
        var user = _userStorage.GetUser(message);
        

        var buttons = new List<InlineKeyboardButton[]>();
        buttons.Add(new[]
        {
            InlineKeyboardButton.WithCallbackData("Обновить данные.", "updateUser")
        });
        
        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id,
            $"Привет, пользователь {user.userId} {user.UserName}. Админ права: {user.IsAdmin}",
            cancellationToken: ct, replyMarkup: new InlineKeyboardMarkup(buttons));

        Console.WriteLine($"Привет, пользователь {user.userId} {user.UserName}. Админ права: {user.IsAdmin}");
    }
}