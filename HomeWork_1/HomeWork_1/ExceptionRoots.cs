namespace OTUS_Homework_3
{
    internal class ExeptionRoots
    {
        public void SendExeptionRoots()
        {
            throw new Exception("������������ �������� �� �������");
        }

        public void InputIntOverflow(string paramName, string value) {
            var ovex = new OverflowException($"��������� �������� ��������� {paramName}, ������ {value} ��������� ������ int");
            ovex.Data.Add("ArgOverflow", value);
            throw ovex;
        }
    }
}