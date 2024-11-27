using System.Data.SqlTypes;

namespace HomeWork_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartMessage(null);
        }

        static void StartMessage (string? UserName)
        {
            Console.WriteLine(); //создаем отступ для удобочитаемости
            string AdditionText = "дай";
            string UserDisplayName = UserName;
            string? Command;
            if (string.IsNullOrEmpty(UserName))
            {
                AdditionText = "/start набери";
                UserDisplayName = "гуманоид";
            }

            Console.WriteLine("Приветствую тебя, о " + UserDisplayName + "!\r\nЯ — Вояджер-17, твой чат-бот.");
            Console.WriteLine("Хочешь начать общение со мной?\r\nТогда команду " + AdditionText + " и вперёд!");
            Console.WriteLine("Если не знаешь, как пользоваться мной,\r\nНабери /help, я тебе помогу.");
            Console.WriteLine("А если хочешь узнать, кто я такой,\r\nТо спроси меня /info, всё тут я расскажу.");
            if (!string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine($"{UserName}!\r\nХочешь эхо услыхать?\r\nНабери команду /echo с аргументом впереди.\r\nЯ готов тебе внимать!");
            }
             Console.WriteLine("Надоело со мной говорить?\r\nПросто скажи мне /exit, и можешь уходить!");

            do
            {
                Command = Console.ReadLine();
                UserName = CommandHandler(Command, UserName);
            } while (Command != "/exit");
        }

        /// <summary>
        /// Обработчик команд пользователя
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        static string CommandHandler(string Command, string UserName)
        {            
            Console.WriteLine(); //создаем отступ для удобочитаемости
            string UserDisplayName = string.IsNullOrEmpty(UserName) ? "Гуманоид" : UserName;

            switch (Command)
            {
                case "/start":
                    if (string.IsNullOrEmpty(UserName))
                    {
                        UserName = GetName();
                    }
                    else
                    {
                        Console.WriteLine($"Я уже знаю что тебя зовут {UserName}, и готов ответить твои вопросы");
                    }                    
                    break;
                case "/help":
                    Console.WriteLine($"{UserDisplayName}, чтоб ботом пользоваться умело, выбирай команды смело!");
                    break;
                case "/info":
                    DateTime DateCreate = new DateTime(DateTime.Now.Year, 11, 16, 16, 0, 0);
                    Console.WriteLine($"{UserDisplayName}, меня зовут Вояджер-17, версия 17.01.28.1. Дата создания: {DateCreate}");
                    break;
                default:
                    if (string.IsNullOrEmpty(Command))
                    {
                        Console.WriteLine("Ничего ты не ответил, гуманоид дорогой,\r\nЧто тревожит твою душу, беспокоит твой покой.");
                        Console.WriteLine("Поразмысли о желаньях и теперь мне сообщи,\r\nЧто за выберешь команду, путь ты свой мне укажи.\r\n");                        
                    }
                    else if (Command.StartsWith("/echo ") && !string.IsNullOrEmpty(UserName))
                    {
                        Console.WriteLine(Command.Replace("/echo ", ""));
                    }
                    else
                    {
                        Console.WriteLine("Команды я такой не знаю,\r\nТы расскажи мне всё равно.");
                        Console.WriteLine("Что за команду ожидаешь\r\nЗдесь в выполнении моём?\r\n");                        
                    }                    
                    break;
            }
            Console.WriteLine("А пока попробуй другие команды");
            Console.WriteLine(); //создаем отступ для удобочитаемости
            return UserName;
        }

        /// <summary>
        /// Получение имени пользователя
        /// </summary>
        /// <returns></returns>
        static string GetName()
        {
            Console.WriteLine("Поведай имя мне своё");
            string UserName = Console.ReadLine();

            while (string.IsNullOrEmpty(UserName)) {
                Console.WriteLine("Смотрите какой таинственный! Попробуй еще раз.");
                UserName = Console.ReadLine();
            }

            Console.WriteLine("Приятно познакомиться!");
            return UserName;
        }
    }
}
