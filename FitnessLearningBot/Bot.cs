using FitnessLearningBot.Controllers;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FitnessLearningBot;

public class Bot : BackgroundService
{
    private ITelegramBotClient _telegramBotClient;

    private TextMessageController _textMessageController;
    private InlineKeyboardController _inlineKeyboardController;
    private DefaultMessageController _defaultMessageController;

    public Bot(ITelegramBotClient telegramClient,
        TextMessageController textMessageController,
        InlineKeyboardController inlineKeyboardController,
        DefaultMessageController defaultMessageController)
    {
        _telegramBotClient = telegramClient;
        _inlineKeyboardController = inlineKeyboardController;
        _textMessageController = textMessageController;
        _defaultMessageController = defaultMessageController;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
    {
        if (update.Type == UpdateType.CallbackQuery)
        {
            await _inlineKeyboardController.Handle(update.Message, ct);
        }

        if (update.Type == UpdateType.Message)
        {
            switch (update.Message?.Type)
            {
                case MessageType.Text:
                    await _textMessageController.Handle(update.Message, ct);
                    break;
                default:
                    await _defaultMessageController.Handle(update.Message!, ct);
                    break;
            }
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);

        Console.WriteLine("Ожидаем 10 секунд перед повторным подключением");
        Thread.Sleep(10000);

        return Task.CompletedTask;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions()
            {
                AllowedUpdates = { }
            }, // Здесь выбираем, какие обновления хотим получать. В данном случае разрешены все
            cancellationToken: stoppingToken);

        return Task.CompletedTask;
    }
}