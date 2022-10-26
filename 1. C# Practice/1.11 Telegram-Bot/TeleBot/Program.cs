using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VoiceTexterBot;
using T_Bot.Controllers;
using T_Bot.Services;
using T_Bot.Configuration;

//https://core.telegram.org/api/bots/buttons

public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        // Объект, отвечающий за постоянный жизненный цикл приложения
        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
            .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
            .Build(); // Собираем

        Console.WriteLine("Сервис запущен");
        // Запускаем сервис
        await host.RunAsync();

        Console.WriteLine("Сервис остановлен");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(BuildAppSettings());

        services.AddSingleton<IStorage, MemoryStorage>();

        services.AddTransient<DefaultControl>();
        services.AddTransient<AudioControl>();
        services.AddTransient<TextControl>();
        services.AddTransient<ButtonsControl>();

       
        // Регистрируем объект TelegramBotClient c токеном подключения
        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
        // Регистрируем постоянно активный сервис бота
        services.AddHostedService<Bot>();
    }

    static AppSettings BuildAppSettings()
    {
        return new AppSettings()
        {
            BotToken = "5734869531:AAE7LWopwOtGGEXE3-xwl5Mw9sODqWcvpPg"
        };
    }


}
