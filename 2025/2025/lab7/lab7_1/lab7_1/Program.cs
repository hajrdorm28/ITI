using System;

namespace lab7_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arr = new DynamicArray<string>();

                arr.Push("Apple");
                arr.Push("Banana");
                arr.Push("Cherry");
                Console.WriteLine("Pushed: Apple, Banana, and Cherry\n");

                Console.WriteLine($"Array[1] = {arr[1]}\n" +
                    $"Length: {arr.Length}\n" +
                    $"Capacity: {arr.Capacity}\n" +
                    $"Popped: {arr.Pop()}"); // Cherry popped

                arr.Pop(); // Banana popped
                arr.Pop(); // Apple popped
                Console.WriteLine($"Popped: {arr.Pop()}"); // error
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}