using FitnessLearningBot;
using FitnessLearningBot.Configuration;
using FitnessLearningBot.Controllers;
using FitnessLearningBot.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;


var host = new HostBuilder()
    .ConfigureServices(((hostContext, services) => ConfigureServices(services)))
    .UseConsoleLifetime()
    .Build();

Console.WriteLine("Service was launched");

await host.RunAsync();

Console.WriteLine("Service was stopped");

static AppConfig BuildAppConfig()
{
    return new AppConfig()
    {
        BotToken = "5542652336:AAEK2_PAG3XzWyLodC1WYI5TrywezoOrdaY"
    };
}

static void ConfigureServices(IServiceCollection services)
{
    var appConfig = BuildAppConfig();

    services.AddSingleton(appConfig);
    services.AddSingleton<IUserStorage, UserStorage>();
        
    services.AddTransient<TextMessageController>();
    services.AddTransient<InlineKeyboardController>();
    services.AddTransient<DefaultMessageController>();
    
    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(appConfig.BotToken));
    services.AddHostedService<Bot>();
}