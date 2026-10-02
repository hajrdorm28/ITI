using System;
using System.Reflection.Metadata.Ecma335;

namespace lab3
{
    internal class Program
    {
        #region rotate 
        static int Rotation(int[] arr, int len, int k)
        {
            k %= len;

            if (k == 0)
                return -1;
            
            while (k-- > 0)
            {
                int last = arr[len - 1];
                for (int i = len - 1; i > 0; i--)
                    arr[i] = arr[i - 1];
                arr[0] = last;
            }
            return 1;
        }
        static void PrintArr(int[] arr)
        {
            foreach (int num in arr)
                Console.Write(num + " ");
            Console.WriteLine();
        }

        #endregion

        #region the second largest number
        static int SecondLargest(int[] arr, int len)
        {
            int firstMax = arr[0]; 
            for(int i = 1; i < len; i++)
            {
                if(arr[i] > firstMax)
                    firstMax = arr[i];
            }

            int secondMax = int.MinValue; // if all of them are negative
            for(int i = 0; i < len; i++)
            {
                if (arr[i] != firstMax && arr[i] > secondMax)
                    secondMax = arr[i];
            }
            return secondMax;
        }
        #endregion

        #region Frequency Counter
        static void FreCtr(int[] arr, int len)
        {
            //int maxVal = arr[0];
            //for(int i = 1; i<len; i++)
            //    if(arr[i] > maxVal)
            //        maxVal = arr[i];

            //int[] count = new int[maxVal + 1]; 

            //for (int i = 0; i < len; i++)
            //    count[arr[i]]++;

            int[] visited = new int[len];

            for (int i = 0; i < len; i++)
            {
                if (visited[i] == 1) 
                    continue;

                int count = 1;
                for (int j = i + 1; j < len; j++)
                {
                    if (arr[i] == arr[j])
                    {
                        count++;
                        visited[j] = 1;
                    }
                }
                string times = count == 1 ? "time" : "times";
                Console.WriteLine($"Number {arr[i]} appears {count} {times}");
     
            }

        }
        #endregion

        #region Mixed Example (out, ref, params)
        static void ReadArray(out int[] arr)
        {
        startLen:
            Console.Write("Enter array length: ");
            if (!int.TryParse(Console.ReadLine(), out int len) || len <= 0)
            {
                Console.WriteLine("Invalid array length, try again.\n");
                goto startLen;
            }
            Console.WriteLine();

            arr = new int[len];

            for (int i = 0; i < len; i++)
            {
            startImplementation:
                Console.Write($"Enter element {i + 1}: ");
                if (!int.TryParse(Console.ReadLine(), out arr[i]))
                {
                    Console.WriteLine("Invalid input, try again.\n");
                    goto startImplementation;
                }
            }
        }
        
        static void ProcessArray(ref int[] arr)
        {
            // Array.Reverse(arr);
            int len = arr.Length;
            for (int i = 0; i < len / 2; i++)
            {
                int temp = arr[i];
                arr[i] = arr[len - 1 - i];
                arr[len - 1 - i] = temp;
            }
        }

        static void PrintArray(params int[] arr)
        {
            foreach(int num in arr)
                Console.Write(num + " ");
            Console.WriteLine();
        }
        #endregion

        #region Matrix Sum of Rows and Columns
        static int[] SumRows(int[,] matrix, int rows, int cols)
        {
            int[] rowSum = new int[rows];
            
            for (int i = 0; i < rows; i++)
            {
                int sumR = 0;
                for (int j = 0; j < cols; j++)
                    sumR += matrix[i, j];

                rowSum[i] = sumR;
            }
            return rowSum;
        }

        static int[] SumCols(int[,] matrix, int rows, int cols)
        {
            int[] ColSum = new int[cols];

            for (int i = 0; i < cols; i++)
            {
                int sumC = 0;
                for (int j = 0; j < rows; j++)
                    sumC += matrix[j, i];
                
                ColSum[i] = sumC;
            }
            return ColSum;
        }
        #endregion

        static void Main(string[] args)
        {
            #region rotate 
            Console.WriteLine("Rotation");

            int len;
            do
            {
                Console.Write("Enter array length: ");
                if (!int.TryParse(Console.ReadLine(), out len) || len <= 0)
                {
                    Console.WriteLine("Invalid array length, try again.\n");
                    len = 0;
                }
            } while (len <= 0);

            Console.WriteLine();

            int[] arr = new int[len];

            for (int i = 0; i < len; i++)
            {
                while (true)
                {
                    Console.Write($"Enter element {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out arr[i]))
                        break;
                    
                    Console.WriteLine("Invalid input, try again.\n");
                }
            }

            Console.Write("Array before rotation => ");
            PrintArr(arr);

            int k;
            while (true)
            {
                Console.Write("\nEnter k (steps to rotate right): ");
                if (int.TryParse(Console.ReadLine(), out k) && k >= 0)
                    break;
                
                Console.WriteLine("Invalid input, try again.\n");
            }

            int res = Rotation(arr, len, k);
            
            if(res == -1)
                Console.WriteLine("(No Rotation) k is a multiple of the array length");
            else
            {
                Console.Write("Array after rotation => ");
                PrintArr(arr);
            }
            Console.WriteLine("\n\n");
            #endregion

            #region the second largest number
            Console.WriteLine("The second largest number");

            int len1;
            do {
                Console.Write("Enter array length: ");
                if (!int.TryParse(Console.ReadLine(), out len1) || len1 <= 0)
                {
                    Console.WriteLine("Invalid array length, try again.\n");
                    len1 = 0;
                }
            }while (len1 <= 0); 

            Console.WriteLine();

            int[] arr1 = new int[len1];

            for (int i = 0; i < len1; i++)
            {
                while (true)
                {
                    Console.Write($"Enter element {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out arr1[i]))
                        break;
                    
                    Console.WriteLine("Invalid input, try again.\n");
                }
            }

            int secondLar = SecondLargest(arr1, len1);
            Console.WriteLine($"\nThe second largest num in this array is: {secondLar}\n\n");
            #endregion

            #region Frequency Counter
            Console.WriteLine("Frequency Counter");

            int len2;
            do {
                Console.Write("Enter array length: ");
                if (!int.TryParse(Console.ReadLine(), out len2) || len2 <= 0)
                {
                    Console.WriteLine("Invalid array length, try again.\n");
                    len2 = 0;
                }
            }while(len2 <= 0);
            Console.WriteLine();

            int[] arr2 = new int[len2];

            for (int i = 0; i < len2; i++)
            {
                while (true)
                {
                    Console.Write($"Enter element {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out arr2[i]))
                        break;

                    Console.WriteLine("Invalid input, try again.\n");
                }
            }
            Console.WriteLine();

            Console.WriteLine("\nFrequencies:");
            FreCtr(arr2, len2);

            Console.WriteLine("\n\n");
            #endregion

            #region Mixed Example (out, ref, params)
            // Read the array
            Console.WriteLine("Mixed Example (out, ref, params)");
            int[] arr3;
            ReadArray(out arr3);
            Console.WriteLine();

            // print the original
            Console.Write("The original array => ");
            PrintArray(arr3);

            // reverse
            ProcessArray(ref arr3);
            Console.Write("The reversed array => ");
            PrintArray(arr3);

            Console.WriteLine("\n\n");
            #endregion

            #region Matrix Sum of Rows and Columns
            Console.WriteLine("Matrix Sum of Rows and Columns");

            int rows;
            while (true)
            {
                Console.Write("Enter rows: ");
                if (int.TryParse(Console.ReadLine(), out rows) && rows > 0)
                    break;

                Console.WriteLine("Invalid input, please enter num of roes again.\n");
            }

            int cols;
            while (true)
            {
                Console.Write("Enter cols: ");
                if (int.TryParse(Console.ReadLine(), out cols) && cols > 0)
                    break;

                Console.WriteLine("Invalid input, enter the num of colums again.\n");
            }

            int[,] matrix = new int[rows, cols];

            Console.WriteLine("Enter matrix values:");
            //for (int i = 0; i < rows; i++) 
            //    for (int j = 0; j < cols; j++)
            //        matrix[i, j] = Convert.ToInt32(Console.ReadLine());

            // read the matrix in one array
            for(int i = 0; i < rows * cols; i++)
            {
                int RowNum = i / cols;
                int ColNum = i % cols;

                matrix[RowNum, ColNum] = Convert.ToInt32(Console.ReadLine());
            }

            // print sum of each row
            int[] rowSum = SumRows(matrix, rows, cols);
            Console.WriteLine($"\nnum of rows => {rows}");
            for (int i = 0; i < rowSum.Length; i++)
                Console.WriteLine($"Sum of row {i + 1} is: {rowSum[i]}");

            // print sum of each column
            Console.WriteLine($"\nnum of cols => {cols}");
            int[] colSum = SumCols(matrix, rows, cols);
            for (int i = 0; i < colSum.Length; i++)
                Console.WriteLine($"Sum of col {i + 1} is: {colSum[i]}");
            #endregion

        }
    }
}