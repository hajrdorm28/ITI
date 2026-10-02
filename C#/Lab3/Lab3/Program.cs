
#region Q1
class ArrayRotation
{
    static int[] RotateArray(int[] arr, int k)
    {
        int n = arr.Length;
        if (n == 0) return arr;

        k = k % n;
        if (k < 0) k += n;

        int[] result = new int[n];

        for (int i = 0; i < n; i++)
        {
            int newIndex = (i + k) % n;
            result[newIndex] = arr[i];
        }

        return result;
    }

    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        int k = 2;

        int[] rotated = RotateArray(arr, k);

        Console.WriteLine("Rotated Array: " + string.Join(", ", rotated));
    }
}
#endregion

#region Q2
class Find2ndLargest
{
    static int FindSecondLargest(int[] arr)
    {
        if (arr.Length < 2)
            throw new ArgumentException("Array must have at least 2 elements.");

        int largest = int.MinValue;
        int secondLargest = int.MinValue;

        foreach (int num in arr)
        {
            if (num > largest)
            {
                secondLargest = largest;
                largest = num;
            }
            else if (num > secondLargest && num < largest)
            {
                secondLargest = num;
            }
        }

        return secondLargest;
    }

    static void Main()
    {
        int[] arr = { 12, 35, 1, 10, 34, 1 };
        Console.WriteLine("Second Largest: " + FindSecondLargest(arr));
    }
}
#endregion

#region Q3
class PrintfreqNum
{
    static void PrintFrequency(int[] arr)
    {
        Dictionary<int, int> freq = new Dictionary<int, int>();

        foreach (int num in arr)
        {
            if (freq.ContainsKey(num))
                freq[num]++;
            else
                freq[num] = 1;
        }

        foreach (var pair in freq)
        {
            string timesWord = pair.Value == 1 ? "time" : "times";
            Console.WriteLine($"Number {pair.Key} appears {pair.Value} {timesWord}");
        }
    }

    static void Main()
    {
        int[] arr = { 1, 2, 2, 3, 1, 4, 2 };
        PrintFrequency(arr);
    }
}
#endregion

#region Q4
class ArrayProcesses
{
    static void ReadArray(out int[] arr)
    {
        Console.Write("Enter number of elements: ");
        int size = int.Parse(Console.ReadLine());

        arr = new int[size];

        for (int i = 0; i < size; i++)
        {
            Console.Write($"Enter element {i + 1}: ");
            arr[i] = int.Parse(Console.ReadLine());
        }
    }

    static void ProcessArray(ref int[] arr)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left < right)
        {
            int temp = arr[left];
            arr[left] = arr[right];
            arr[right] = temp;

            left++;
            right--;
        }
    }

    static void PrintArray(params int[] arr)
    {
        Console.WriteLine(string.Join(" ", arr));
    }

    static void Main()
    {
        int[] numbers;

        ReadArray(out numbers);

        Console.Write("Original array: ");
        PrintArray(numbers);

        ProcessArray(ref numbers);

        Console.Write("Reversed array: ");
        PrintArray(numbers);
    }
}
#endregion

#region Q5
class Matrix
{
    static void Main()
    {
        Console.Write("Enter number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Enter number of columns: ");
        int cols = int.Parse(Console.ReadLine());

        int[,] matrix = new int[rows, cols];

        int totalElements = rows * cols;
        for (int index = 0; index < totalElements; index++)
        {
            int row = index / cols;
            int col = index % cols;

            Console.Write($"Enter value for [{row}][{col}]: ");
            matrix[row, col] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine("\nMatrix:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nRow Sums:");
        for (int i = 0; i < rows; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < cols; j++)
            {
                rowSum += matrix[i, j];
            }
            Console.WriteLine($"Row {i + 1} Sum: {rowSum}");
        }

        Console.WriteLine("\nColumn Sums:");
        for (int j = 0; j < cols; j++)
        {
            int colSum = 0;
            for (int i = 0; i < rows; i++)
            {
                colSum += matrix[i, j];
            }
            Console.WriteLine($"Column {j + 1} Sum: {colSum}");
        }
    }
} 
#endregion