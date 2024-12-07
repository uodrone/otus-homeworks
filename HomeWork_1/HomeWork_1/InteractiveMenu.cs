using System.Text;

namespace OTUS_Homework_3
{
    internal class InteractiveMenu
    {

        /// <summary>
        /// Инициализируем положение курсора
        /// </summary>
        private static int selectedValue = 0;
        private static int ConsoleStringPosition = 5;

        /// <summary>
        /// Аргументы в пунктах меню
        /// </summary>
        private static string[] options = new[]{
            "a: ",
            "b: ",
            "c: "
        };

        /// <summary>
        /// Выводим меню
        /// </summary>
        private static void MenuRender ()
        {
            for (var i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(2, i+1);
                Console.WriteLine($"{options[i]}");
            }
            //убираем мигание курсора
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Смещаем курсор вниз
        /// </summary>
        private static void CursorToDown()
        {
            if (selectedValue < options.Length)
            {
                selectedValue++;
            }
            else
            {
                selectedValue = 1;
            }
        }

        /// <summary>
        /// Смещаем курсор вверх
        /// </summary>
        private static void CursorToTop()
        {
            if (selectedValue > 1)
            {
                selectedValue--;
            }
            else
            {
                selectedValue = 4;
            }
        }

        private static void SetCursorPosition(int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.Write(">");
            Console.SetCursorPosition(0, pos);
        }

        private static void ClearCursor(int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.Write(" ");
            Console.SetCursorPosition(0, pos);
        }

        /// <summary>
        /// Запуск и управление меню
        /// </summary>
        public int MenuHandler(ref Dictionary<string, int> Arguments, ref Dictionary<string, string> ArgsCheckFormat, ref List<string> ListErrorArgs)
        {
            ConsoleKeyInfo ConsKey;
            int lineLength = ConsoleStringPosition;
            selectedValue = 1;
            var QE = new QuadraticEquation();
            var sb = new StringBuilder();

            MenuRender();
            SetCursorPosition(selectedValue);
            do
            {
                ConsKey = Console.ReadKey();                
                ClearCursor(selectedValue);

                switch (ConsKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        CursorToTop();
                        lineLength = ConsoleStringPosition;
                        sb = new StringBuilder();
                        break;
                    case ConsoleKey.DownArrow:
                        CursorToDown();
                        lineLength = ConsoleStringPosition;
                        sb = new StringBuilder();
                        break;
                    case ConsoleKey.Enter:  
                        if (ArgsCheckFormat.Count == 3)
                        {
                            GetArgument("a", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
                            GetArgument("b", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
                            GetArgument("c", ref Arguments, ref ArgsCheckFormat, ref ListErrorArgs);
                        }
                        break;
                    default:
                        //включаем обратно мигание курсора
                        Console.CursorVisible = true;
                        int cursorTop = Console.CursorTop;
                        sb.Append(ConsKey.KeyChar);
                        
                        //Наполняем словарь проверки значениями, исходя из номера строки, на которой находимся
                        if (selectedValue == 1)
                        {
                            if (ArgsCheckFormat.ContainsKey("a"))
                            {
                                ArgsCheckFormat["a"] = sb.ToString();
                            }
                            else
                            {
                                ArgsCheckFormat.Add("a", sb.ToString());
                            }                            
                        }

                        if (selectedValue == 2)
                        {
                            if (ArgsCheckFormat.ContainsKey("b"))
                            {
                                ArgsCheckFormat["b"] = sb.ToString();
                            }
                            else
                            {
                                ArgsCheckFormat.Add("b", sb.ToString());
                            }
                        }

                        if (selectedValue == 3)
                        {
                            if (ArgsCheckFormat.ContainsKey("c"))
                            {
                                ArgsCheckFormat["c"] = sb.ToString();
                            }
                            else
                            {
                                ArgsCheckFormat.Add("c", sb.ToString());
                            }
                        }

                        GetCurrentCursorPosition(ref lineLength, cursorTop, ConsKey.KeyChar);                        
                        break;
                }
                SetCursorPosition(selectedValue);
            }
            while (Arguments.Count < 3);

            return selectedValue + 2;
        }

        /// <summary>
        /// Выставляем позицию курсора при наборе значений в меню
        /// </summary>
        /// <param name="lineLength"></param>
        /// <param name="cursorTop"></param>
        /// <param name="symbol"></param>
        private void GetCurrentCursorPosition (ref int lineLength, int cursorTop, char symbol)
        {
            Console.SetCursorPosition(lineLength, cursorTop);
            Console.Write(symbol);
            lineLength++;
        }

        public void GetArgument(string ArgName, ref Dictionary<string, int> Arguments, ref Dictionary<string, string> ArgsCheckFormat, ref List<string> ListErrorArgs)
        {
            bool result = int.TryParse(ArgsCheckFormat[ArgName], out int ArgVal);
            Arguments.Add(ArgName, ArgVal);
            if (!result)
            {
                ListErrorArgs.Add(ArgName);
            }
        }

    }
}