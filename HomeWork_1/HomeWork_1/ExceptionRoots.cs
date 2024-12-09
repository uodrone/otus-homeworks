namespace OTUS_Homework_3
{
    internal class ExeptionRoots:Exception
    {
        public ExeptionRoots()
        {
        }
        public ExeptionRoots(string message)
        : base(message) { }

        public void InputIntOverflow(string paramName, string value) {
            var ovex = new OverflowException($"Введенное значение параметра {paramName}, равное {value} превышает размер int");
            ovex.Data.Add("ArgOverflow", value);
            throw ovex;
        }
    }
}