using System.Data.SqlTypes;
using System.Diagnostics;

namespace HomeWork_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartMessage(null);
        }

        public static void StartMessage(string? UserName)
        {
            Console.WriteLine(); //создаем отступ для удобочитаемости
            string AdditionText = "дай";
            string? UserDisplayName = UserName;
            string? Command;
            var TaskList = new List<string>();

            if (string.IsNullOrEmpty(UserName))
            {
                AdditionText = "/start набери";
                UserDisplayName = "гуманоид";
            }

            Console.WriteLine("Приветствую тебя, о " + UserDisplayName + "!\r\nЯ — Вояджер-17, твой чат-бот.");
            Console.WriteLine("Хочешь начать общение со мной?\r\nТогда команду " + AdditionText + " и вперёд!");
            Console.WriteLine("Если не знаешь, как пользоваться мной,\r\nНабери /help, я тебе помогу.");
            Console.WriteLine("А если хочешь узнать, кто я такой,\r\nТо спроси меня /info, всё тут я расскажу.");
            Console.WriteLine("Введи команду /addtask, помочь я буду только рад,\r\nИсполню всё, что скажешь ты, без колебаний и преград.");
            Console.WriteLine("Если нужен список задач тебе,\r\nВведи /showtasks — и всё откроется судьбе.");
            if (TaskList.Count > 0)
            {
                Console.WriteLine("Таску нужно удалить?\r\nСкажи: /removetask\r\nИ не забудь номер задачи предложить");
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine($"{UserName}!\r\nХочешь эхо услыхать?\r\nНабери команду /echo с аргументом впереди.\r\nЯ готов тебе внимать!");
            }
            Console.WriteLine("Надоело со мной говорить?\r\nПросто скажи мне /exit, и можешь уходить!");
            Console.WriteLine(); //отступ

            do
            {
                Command = Console.ReadLine();
                UserName = CommandHandler(Command, UserName, ref TaskList);
            } while (Command != "/exit");
        }

        /// <summary>
        /// Обработчик команд пользователя
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static string CommandHandler(string? Command, string? UserName, ref List<string> TaskList)
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
                    Console.WriteLine();
                    Console.WriteLine("/start - знакомство с чат-ботом, т.е. со мной. Я спрошу как тебя зовут.");
                    Console.WriteLine("/help - подмога в трудной ситуации, ты сейчас здесь!");
                    Console.WriteLine("/info - информация обо мне, т.е. о чат-боте");
                    Console.WriteLine("/addtask - добавить задачу в список твоих задач, чтобы ничего не пропустить");
                    Console.WriteLine("/showtasks - посмотреть список уже существующих задач");
                    Console.WriteLine("/removetask - удалить задачу из списка");
                    Console.WriteLine("/echo - верну тебе эхо строки, написанной сразу после команды");
                    Console.WriteLine("/exit - окончание общения со мной :( Не делай так, не надо!");
                    break;
                case "/info":
                    DateTime DateCreate = new DateTime(DateTime.Now.Year, 11, 16, 16, 0, 0);
                    Console.WriteLine($"{UserDisplayName}, меня зовут Вояджер-17, версия 17.01.28.1. Дата создания: {DateCreate}");
                    break;
                case "/addtask":
                    TaskAdd(ref TaskList);
                    break;
                case "/showtasks":
                    if (!TaskList.Any())
                    {
                        Console.WriteLine("Лист команд пуст");
                    }
                    else
                    {
                        Console.WriteLine("Список команд:");
                        TaskList.ForEach(task => Console.WriteLine(task));
                    }
                    break;
                case "/removetask":
                    TaskRemove(ref TaskList);
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
            Console.WriteLine(); //создаем отступ для удобочитаемости
            Console.WriteLine("А пока попробуй другие команды");
            Console.WriteLine(); //создаем отступ для удобочитаемости
            return UserName;
        }

        /// <summary>
        /// Получение имени пользователя
        /// </summary>
        /// <returns></returns>
        public static string GetName()
        {
            Console.WriteLine("Поведай имя мне своё");
            string? UserName = Console.ReadLine();

            while (string.IsNullOrEmpty(UserName))
            {
                Console.WriteLine("Смотрите какой таинственный! Попробуй еще раз.");
                UserName = Console.ReadLine();
            }

            Console.WriteLine("Приятно познакомиться!");
            return UserName;
        }

        public static void TaskAdd(ref List<string> TaskList) {
            Console.WriteLine();// отступ для удобочитаемости
            Console.WriteLine("Введи команду, которую нужно сохранить:");
            string Task = Console.ReadLine();
            if (string.IsNullOrEmpty(Task))
            {
                Console.WriteLine("Пустую команду не нужно вводить тебе,\r\nподумай еще и возвращайся ко мне");
            }
            else
            {
                TaskList.Add(Task);
                Console.WriteLine($"Задача «{Task}» добавлена в список");
            }
        }

        public static void TaskRemove(ref List<string> TaskList)
        {
            if (!TaskList.Any())
            {
                Console.WriteLine("Список пуст, нечего удалять");
            } else
            {
                Console.WriteLine();// отступ для удобочитаемости
                Console.WriteLine("Введи номер команды, которую нужно удалить:");
                for (int i = 1; i <= TaskList.Count; i++)
                {
                    Console.WriteLine($"{i}.{TaskList[i - 1]}");
                }

                string Command = Console.ReadLine();
                bool NumOrNot = int.TryParse(Command, out int CommandNum);


                if (string.IsNullOrEmpty(Command) || !NumOrNot || CommandNum > TaskList.Count || CommandNum < 1)
                {
                    Console.WriteLine("Неверный номер команды, попробуй ешо!");
                    TaskRemove(ref TaskList);
                }
                else
                {
                    TaskList.RemoveAt(CommandNum);
                    Console.WriteLine($"Задача «{TaskList[CommandNum - 1]}» удалена");
                }
            }
        }
    }
}
