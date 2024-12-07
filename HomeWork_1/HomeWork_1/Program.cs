using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OTUS_Homework_3
{
    internal class QuadraticEquation
    {
        enum Severity
        {
            Warning,
            Error
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Попробуем решить квадратное уравнение a*x^2 + b*x + c = 0");
            HandlerExeption();
        }

        /// <summary>
        /// Запрашиваем данные и обрабатываем исключения
        /// </summary>
        static void HandlerExeption()
        {
            var Arguments = new Dictionary<string, int>();
            var ArgsCheckFormat = new Dictionary<string, string>();
            var ListErrorArgs = new List<string>();

            GetArgument("a", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
            GetArgument("b", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
            GetArgument("c", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);

            try
            {
                if (ListErrorArgs.Count > 0)
                {
                    string ErrorArgs = string.Join(", ", ListErrorArgs);
                    string EndWord = ListErrorArgs.Count > 1 ? "ов" : "а";

                    throw new FormatException($"Неверный формат параметр{EndWord}: {ErrorArgs}");
                }
                else
                {
                    GetRoots(Arguments);
                }
            }
            catch (FormatException fex)
            {
                FormatData(fex.Message, Severity.Error, ArgsCheckFormat);
                Console.WriteLine();
                Console.WriteLine("Начинаем все сначала:");
                HandlerExeption();
            }
            catch (Exception ex) {
                FormatData(ex.Message, Severity.Warning, ArgsCheckFormat);
            }
        }

        /// <summary>
        /// Метод получения аргумента уравнения
        /// </summary>
        /// <param name="ArgName"></param>
        /// <param name="Arguments"></param>
        /// <param name="ArgsCheckFormat"></param>
        static void GetArgument (string ArgName, ref Dictionary<string, int> Arguments, ref Dictionary<string, string> ArgsCheckFormat, ref List<string> ListErrorArgs)
        {
            Console.WriteLine($"Введите значение {ArgName}:");
            string ArgValString = Console.ReadLine();
            ArgsCheckFormat.Add(ArgName, ArgValString);
            bool result = int.TryParse(ArgValString, out int ArgVal);
            Arguments.Add(ArgName, ArgVal);
            if (!result)
            {
                ListErrorArgs.Add(ArgName);
            }
        }

        /// <summary>
        /// Получаем дискриминант
        /// </summary>
        /// <param name="Arguments"></param>
        /// <returns></returns>
        static int GetDiscriminant (Dictionary<string, int> Arguments)
        {
            int D = Arguments["b"]*Arguments["b"] - 4*Arguments["a"]*Arguments["c"];
            return D;
        }

        /// <summary>
        /// Ищем корни, а если их нет - пробрасываем исключение
        /// </summary>
        /// <param name="Arguments"></param>
        /// <exception cref="Exception"></exception>
        static void GetRoots (Dictionary<string, int> Arguments)
        {
            int D = GetDiscriminant(Arguments);
            int a = Arguments["a"];
            int b = Arguments["b"];            

            if (D < 0)
            {
                var ExceptionRoots = new ExeptionRoots();
                ExceptionRoots.SendExeptionRoots();
            }
            else if (D == 0) {
                double x = (Math.Sqrt(D) - b) / 2 * a;
                Console.WriteLine("x = " + x);
            }
            else
            {
                double x1 = (Math.Sqrt(D) - b) / 2 * a;
                double x2 = (-Math.Sqrt(D) - b) / 2 * a;

                Console.WriteLine("x1 = " + x1);
                Console.WriteLine("x2 = " + x2);
            }

            Console.WriteLine("Поздравляю, Вы гениальны!");
        }

        /// <summary>
        /// Определяем формат вывода исключения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        /// <param name="data"></param>
        static void FormatData(string message, Severity severity, IDictionary<string, string> data)
        {
            //устанавливаем цвета консоли в зависимости от типа ошибки
            if (severity == Severity.Error) {
                Console.BackgroundColor = ConsoleColor.Red;
            } else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }

            for (int i = 0; i < 50; i++) {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine(message);
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            for (int i = 0; i < data.Count; i++) {
                Console.WriteLine($"{data.Keys.ElementAt(i)} = {data[data.Keys.ElementAt(i)]}");
            }

            //Сбрасываем цвета консоли, чтоб не дай бох дальше таким писать
            Console.ResetColor();
        }
    }
}
