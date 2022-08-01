using FitnessLearningBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FitnessLearningBot.Controllers;

public class InlineKeyboardController
{
    private ITelegramBotClient _telegramBotClient;
    private IUserStorage _userStorage;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient, IUserStorage userStorage)
    {
        _telegramBotClient = telegramBotClient;
        _userStorage = userStorage;
    }

    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == "updateUser")
        {
            _userStorage.UpdateUser(callbackQuery.From.Id);
            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"Данные обновлены.",
                cancellationToken: ct);
        }
    }
}