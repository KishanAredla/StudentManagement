namespace XUnit.Practice.Tests.Practice
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new ArgumentException("Cannot divide by zero");

            return a / b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public string GetGrade(int marks)
        {
            if (marks < 0 || marks > 100)
                throw new ArgumentException("Invalid marks");

            if (marks >= 90)
                return "A";
            if (marks >= 75)
                return "B";
            if (marks >= 60)
                return "C";

            return "D";
        }
    }
}
