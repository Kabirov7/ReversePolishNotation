using System;

namespace ReversePolishNotation
{
    class Programm
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите выражение: \n> ");
                Console.WriteLine(RPN.Calculate("(8 + 2*5) - 100"));
            }
        }
    }
}