
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;


namespace Csharp_Telegram_Bot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient(""); // токен
        
        // обрабатываем события
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия

            // отправка данных события в чат 
            // переделать в запись в файл
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            // обработка входящего сообщений
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                // команда /start
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Велком, велком, " + update.Message.From.FirstName + ", ня!");
                    return;
                }
                else if (message.Text.ToLower() == "ня")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Ня!");
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");

                }
            }

            // обработка
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.InlineQuery)
            {
                
            }
        }

        // обработка ошибок
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия

            // отправка данных исключения в консоль
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            // обработка чего-то
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            // Запус бота
            bot.StartReceiving(
                HandleUpdateAsync, // обработчик обновлений (входящих сообщений)
                HandleErrorAsync,  // обработчик ошибок 
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
