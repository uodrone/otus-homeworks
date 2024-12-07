using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OTUS_Homework_3
{
    internal class QuadraticEquation
    {
        enum Severity
        {
            Warning,
            Overflow,
            Error
        }

        static void Main()
        {

            Console.WriteLine("Попробуем решить квадратное уравнение a*x^2 + b*x + c = 0");
            var Arguments = new Dictionary<string, int>();
            var ArgsCheckFormat = new Dictionary<string, string>();
            var ListErrorArgs = new List<string>();

            var Menu = new InteractiveMenu();

            try
            {
                Menu.MenuHandler(ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
                Console.SetCursorPosition(0, 5);

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
                Console.SetCursorPosition(0, 5);
                FormatData(fex.Message, Severity.Error, ArgsCheckFormat);
                Console.WriteLine();
                Console.WriteLine("Начинаем все сначала? (Enter - ДА)");
                ConsoleKeyInfo ConsKey = Console.ReadKey();
                if (ConsKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Main();
                }
            }
            catch(OverflowException ovex)
            {
                Console.SetCursorPosition(0, 5);
                if (ovex.Data.Contains("ArgOverflow"))
                {
                    FormatData(ovex.Message, Severity.Overflow, ArgsCheckFormat);
                }
                else
                {
                    FormatData($"Слишком много. Результат не вписывается в диапазон: {ovex.Message}", Severity.Error, ArgsCheckFormat);
                }
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, 5);
                FormatData(ex.Message, Severity.Warning, ArgsCheckFormat);
            }
        }

        /// <summary>
        /// Получаем дискриминант
        /// </summary>
        /// <param name="Arguments"></param>
        /// <returns></returns>
        static int GetDiscriminant (Dictionary<string, int> Arguments)
        {
            try
            {
                int D = checked(Arguments["b"] * Arguments["b"] - 4 * Arguments["a"] * Arguments["c"]);
                return D;
            }
            catch (OverflowException)
            {
                throw;
            }
        }

        /// <summary>
        /// Ищем корни, а если их нет - пробрасываем исключение
        /// </summary>
        /// <param name="Arguments"></param>
        /// <exception cref="Exception"></exception>
        static void GetRoots (Dictionary<string, int> Arguments)
        {
            try {
                int D = GetDiscriminant(Arguments);
                int a = Arguments["a"];
                int b = Arguments["b"];

                if (D < 0)
                {
                    var ExceptionRoots = new ExeptionRoots();
                    ExceptionRoots.SendExeptionRoots();
                }
                else if (D == 0)
                {
                    double x = checked((Math.Sqrt(D) - b) / 2 * a);
                    Console.WriteLine("x = " + x);
                }
                else
                {
                    double x1 = checked((Math.Sqrt(D) - b) / 2 * a);
                    double x2 = checked((-Math.Sqrt(D) - b) / 2 * a);

                    Console.WriteLine("x1 = " + x1);
                    Console.WriteLine("x2 = " + x2);
                }
            }
            catch (OverflowException)
            {
                throw;
            }
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
            } 
            else if (severity == Severity.Overflow)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
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
