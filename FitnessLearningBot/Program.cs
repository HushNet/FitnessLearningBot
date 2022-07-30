using FitnessLearningBot;
using FitnessLearningBot.Controllers;
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

static void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<TextMessageController>();
    services.AddTransient<InlineKeyboardController>();
    services.AddTransient<DefaultMessageController>();
    
    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("5542652336:AAEK2_PAG3XzWyLodC1WYI5TrywezoOrdaY"));
    services.AddHostedService<Bot>();
}