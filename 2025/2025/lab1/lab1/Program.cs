using System;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Temperature Converter
            Console.Write("Enter temperature in celsius: ");
            double celsius = Convert.ToDouble(Console.ReadLine());

            double fahrenheit = (celsius * 9 / 5) + 32;

            Console.WriteLine($"Temperature in fahrenheit = {fahrenheit}\n");
            #endregion

            #region Even or Odd
            Console.Write("Enter the num: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num % 2 == 0)
                Console.WriteLine($"{num} is even");
            else
                Console.WriteLine($"{num} is odd");

            num++;
            Console.WriteLine($"After increment number: {num}");

            num--;
            Console.WriteLine($"After increment number: {num}");
            Console.WriteLine();

            //string res = (num % 2 == 0) ? $"{num} is even" : $"{num} is odd";
            //Console.WriteLine($"{res} \n");
            #endregion

            #region Grade Evaluation
            Console.Write("Enter the score: ");
            int score = Convert.ToInt32(Console.ReadLine());

            if(score >= 90 && score <= 100)
                Console.WriteLine("A");
            else if(score >= 80)
                Console.WriteLine("B");
            else if(score >= 70)
                Console.WriteLine("C");
            else if(score >= 60)
                Console.WriteLine("D");
            else
                Console.WriteLine("F");
            Console.WriteLine();
        #endregion

            #region Simple Calculator
        start:
            Console.Write("Enter first num: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("choose operation (+, -, /, *): ");
            char op = Convert.ToChar(Console.ReadLine());

            Console.Write("Enter second num: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case '+':
                    Console.WriteLine($"num1 + num2 = {num1 + num2}");
                    break;
                case '-':
                    Console.WriteLine($"num1 - num2 = {num1 - num2}");
                    break;
                case '*':
                    Console.WriteLine($"num1 * num2 = {num1 * num2}");
                    break;
                case '/':
                    if (num2 != 0)
                        Console.WriteLine($"num1 / num2 = {num1 / num2}");
                    else
                    {
                        Console.WriteLine("\nInvalid division!, try again");
                        goto start;
                    }
                    break;
                default:{
                        Console.WriteLine("\nInvalid operation!, try again");
                        goto start;
                        }
                    break;
            }
            #endregion
        
        }
    }
}