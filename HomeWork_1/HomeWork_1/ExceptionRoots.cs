namespace OTUS_Homework_3
{
    internal class ExeptionRoots
    {
        public void SendExeptionRoots()
        {
            throw new Exception("¬ещественных значений не найдено");
        }

        public void InputIntOverflow(string paramName, string value) {
            var ovex = new OverflowException($"¬веденное значение параметра {paramName}, равное {value} превышает размер int");
            ovex.Data.Add("ArgOverflow", value);
            throw ovex;
        }
    }
}