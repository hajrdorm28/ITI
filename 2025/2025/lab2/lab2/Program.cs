using System;

namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Number Guess with Limited Attempts
            Random random = new Random();
            int secret = random.Next(1, 101);
            int tries = 5;

            Console.WriteLine("Guess the num (1, 100), you have 5 tries");
            for (int i = 1; i <= tries; i++)
            {
                Console.Write("Try 1: ");
                //int guess = Convert.ToInt32(Console.ReadLine());
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Invalid input! Enter a number.");
                    i--;
                    continue;
                }
                if (guess == secret)
                {
                    Console.WriteLine("Correct, you win!");
                    return;
                }
                else if (guess > secret)
                    Console.WriteLine("too high\n");
                else
                    Console.WriteLine("too low\n");
            }

            Console.WriteLine($"\nOut of tries, the num was {secret}\n\n");
        #endregion

            #region shapes

            #region left-Angled Triangle Numbers
            start:
                Console.Write("Enter num of rows: ");
                // int rows = Convert.ToInt32(Console.ReadLine());
                string rowsStr = Console.ReadLine();
                if(!int.TryParse(rowsStr, out int rows))
                {
                    Console.WriteLine("Invalid input! Enter a number.");
                    goto start;
                }

                for (int i = 1; i <= rows; i++)
                {
                    for (int k = 1; k <= rows - i; k++)
                        Console.Write(" ");

                    for (int j = i; j >= 1; j--)
                        Console.Write(j);

                    Console.WriteLine();
                }
                Console.WriteLine("\n\n");

            #endregion

            #region Pyramid Pattern
        start1:
            Console.Write("Enter num of rows: ");
            // int rows_ = Convert.ToInt32(Console.ReadLine());

            string rowsStr_ = Console.ReadLine();
            if (!int.TryParse(rowsStr, out int rows_))
            {
                Console.WriteLine("Invalid input! Enter a number.");
                goto start1;
            }

            for (int i = 1; i <= rows_; i++)
            {
                for (int k = 1; k <= rows_ - i; k++)
                    Console.Write(" ");

                for (int star = 1; star <= (2 * i - 1); star++)
                    Console.Write("*");

                Console.WriteLine();
            }
            Console.WriteLine("\n\n");
            #endregion

            #endregion

            #region Age Calculator
            Console.Write("Enter your age: ");
            string ageStr = Console.ReadLine();

            // add 5 years
            if (int.TryParse(ageStr, out int ageInt))
                Console.WriteLine($"In 5 years, you will be {ageInt + 5} years old");
            else
                Console.WriteLine("Invalid integer for age");

            // as double
            if(double.TryParse(ageStr,out double ageDouble))
                Console.WriteLine($"as double / 3 = {ageDouble / 3}");
            else
                Console.WriteLine("Invalid double for age");

            Console.WriteLine("\n\n");
            #endregion

            #region Tax Calculator with Range Patterns
            Console.Write("Enter annual salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());

            decimal rate = salary switch
            {
                <= 10000m => 0.0m,
                > 10000m and <= 30000m => 0.10m,
                > 30000m and <= 70000m => 0.20m,
                _ => 0.30m
            };

            decimal tax = salary * rate;
            decimal net = salary - tax;

            Console.WriteLine($"Tax rate: {rate * 100}%");
            Console.WriteLine($"Tax amount: {tax}");
            Console.WriteLine($"Net after tax: {net} ");

            Console.WriteLine("\n\n");
            #endregion

            #region Multiplication Table
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= 10; i++)
                Console.WriteLine($"{n} * {i} = {n *  i}");
            #endregion
       
        }
    }
}