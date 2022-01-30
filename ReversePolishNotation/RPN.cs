using System;
using System.Collections.Generic;

namespace ReversePolishNotation
{
    public class RPN
    {
        public static double Calculate(string input)
        {
            string expression = GetExpression(input);
            double result = Counting(expression);
            return result;
        }

        private static string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                    continue;

                if (char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char o = operStack.Pop();
                        while (o != '(')
                        {
                            output += o.ToString() + ' ';
                            o = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                        {
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                            operStack.Push(input[i]);
                        }
                    }
                }
            }

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";
            

            return output;
        }

        private static double Counting(string input)
        {
            int result = 0;
            Stack<int> temp = new Stack<int>();

            foreach (string token in input.Split(new char[] {' '}))
            {
                if (char.IsDigit(token[0]))
                {
                    temp.Push(int.Parse(token));

                }
                else
                {
                    int rhs = temp.Pop();
                    int lhs = temp.Pop();
                    
                    switch (token[0])
                    {
                        case '+': result = lhs + rhs; break;
                        case '-': result = lhs - rhs; break;
                        case '*': result = lhs * rhs; break;
                        case '/': result = lhs / rhs; break;
                        case '^': result = (int) Math.Pow(lhs, rhs); break;
                    }
                    
                    temp.Push(result);
                }
            }

            return temp.Peek();
        }

        private static bool IsDelimeter(char c)
        {
            if (" =".IndexOf(c) != -1)
            {
                return true;
            }

            return false;
        }

        private static bool IsOperator(char c)
        {
            if ("+-/*^()".IndexOf(c) != -1)
            {
                return true;
            }

            return false;
        }

        private static byte GetPriority(char c)
        {
            switch (c)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 6;
            }
        }
    }
}