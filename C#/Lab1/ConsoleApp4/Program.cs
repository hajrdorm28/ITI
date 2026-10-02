// 1. Temperature Converter
class TemperatureConverter
{
    static void Main()
    {
        Console.Write("Enter temperature in Celsius: ");
        double celsius = Convert.ToDouble(Console.ReadLine());

        double fahrenheit = (celsius * 9 / 5) + 32;

        Console.WriteLine($"{celsius}°C is equal to {fahrenheit}°F");
    }
}

//2. Even or Odd

class EvenOrOdd
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        string result = (number % 2 == 0) ? "Even" : "Odd";
        Console.WriteLine($"{number} is {result}");

        Console.WriteLine($"Negative of {number} is {-number}");

        int temp = number;
        Console.WriteLine($"Pre-increment: {++temp}"); 
        temp = number;
        Console.WriteLine($"Post-increment: {temp++}"); 
        Console.WriteLine($"Value after post-increment: {temp}");
    }
}

//3.Grade Evaluation
class GradeEvaluation
{
    static void Main()
    {
        Console.Write("Enter your score (0-100): ");
        int score = Convert.ToInt32(Console.ReadLine());

        char grade;

        if (score >= 90 && score <= 100)
            grade = 'A';
        else if (score >= 80)
            grade = 'B';
        else if (score >= 70)
            grade = 'C';
        else if (score >= 60)
            grade = 'D';
        else if (score >= 0)
            grade = 'F';
        else
        {
            Console.WriteLine("Invalid score entered.");
            return;
        }

        Console.WriteLine($"Your grade is: {grade}");
    }
}
//4.Simple Calculator

class SimpleCalculator
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter second number: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Choose an operation (+, -, *, /): ");
        string op = Console.ReadLine();

        double result;
        bool valid = true;

        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 == 0)
                {
                    Console.WriteLine("Error: Division by zero is not allowed.");
                    valid = false;
                    result = 0;
                }
                else
                {
                    result = num1 / num2;
                }
                break;
            default:
                Console.WriteLine("Invalid operation.");
                valid = false;
                result = 0;
                break;
        }

        if (valid)
            Console.WriteLine($"Result: {num1} {op} {num2} = {result}");
    }
}