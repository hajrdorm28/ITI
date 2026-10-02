
//class NumberGuessGame
//{
//    static void Main()
//    {
//        Random range = new Random();
//        int target = range.Next(1, 101);
//        int maxAttempts = 5;
//        int attempt = 0;
//        bool won = false;

//        Console.WriteLine("Welcome! I'm thinking of a number between 1 and 100. Can you guess it??!!!");
//        Console.WriteLine($"You have {maxAttempts} attempts to guess it.\n");

//        while (attempt < maxAttempts)
//        {
//            attempt++;
//            Console.Write($"Attempt {attempt}/{maxAttempts} - Enter your guess: ");
//            string input = Console.ReadLine();

//            if (!int.TryParse(input, out int guess))
//            {
//                Console.WriteLine("Please enter a valid whole number.");
//                attempt--;
//                continue;
//            }

//            if (guess == target)
//            {
//                Console.WriteLine($"Correct! The number was {target}. You won in {attempt} attempt(s)!");
//                won = true;
//                break;
//            }
//            else if (guess < target)
//            {
//                Console.WriteLine("Too low!\n");
//            }
//            else
//            {
//                Console.WriteLine("Too high!\n");
//            }
//        }

//        if (!won)
//        {
//            Console.WriteLine($"Game over! You've used all {maxAttempts} attempts. The number was {target}.");
//        }
//    }
//}
//---------------------------------------------------------------------------------------------------------------
class ShapePatterns
{
    static void Main()
    {
        Console.Write("Enter the height for the shapes: ");
        int height = int.Parse(Console.ReadLine());

        Console.WriteLine("\n--- Left-Angled Triangle Numbers ---");
        DrawTriangleNumbers(height);

        Console.WriteLine("\n--- Pyramid Pattern ---");
        DrawPyramid(height);
    }
    static void DrawTriangleNumbers(int height)
    {
        for (int row = 1; row <= height; row++)
        {
            for (int s = 0; s < height - row; s++)
            {
                Console.Write(' ');
            }
            for (int num = row; num >= 1; num--)
            {
                Console.Write(num);
            }

            Console.WriteLine();
        }
    }
    static void DrawPyramid(int height)
    {
        for (int row = 1; row <= height; row++)
        {
            for (int s = 0; s < height - row; s++)
            {
                Console.Write(' ');
            }
            for (int star = 0; star < 2 * row - 1; star++)
            {
                Console.Write('*');
            }

            Console.WriteLine();
        }
    }
}
//--------------------------------------------------------------------------------------
//class AgeCalculator
//{
//    static void Main()
//    {
//        Console.Write("Enter your age: ");
//        string ageInput = Console.ReadLine();

//        int age = int.Parse(ageInput);
//        int futureAge = age + 5;
//        Console.WriteLine($"In 5 years, you will be {futureAge} years old.");

//        double ageAsDouble = double.Parse(ageInput);
//        double dividedAge = ageAsDouble / 3;
//        Console.WriteLine($"Your age divided by 3 is {dividedAge:F2}.");
//    }
//}
//---------------------------------------------------------------------------------------

//class TaxCalculator
//{
//    static void Main()
//    {
//        Console.Write("Enter your annual salary: ");
//        double salary = double.Parse(Console.ReadLine());

//        double taxRate = salary switch
//        {
//            <= 10000 => 0.0,
//            > 10000 and <= 30000 => 0.10,
//            > 30000 and <= 70000 => 0.20,
//            > 70000 => 0.30,
//            _ => throw new ArgumentException("Invalid salary")
//        };

//        double taxOwed = salary * taxRate;

//        Console.WriteLine($"Salary: {salary:C}");
//        Console.WriteLine($"Tax rate: {taxRate:P0}");
//        Console.WriteLine($"Tax owed: {taxOwed:C}");
//    }
//}
//------------------------------------------------------------------------------------------
//class MultiplicationTable
//{
//    static void Main()
//    {
//        Console.Write("Enter a number: ");
//        int n = int.Parse(Console.ReadLine());

//        Console.WriteLine($"\n Multiplication table for {n}:");
//        for (int i = 1; i <= 10; i++)
//        {
//            Console.WriteLine($"{n} x {i} = {n * i}");
//        }
//    }
//}