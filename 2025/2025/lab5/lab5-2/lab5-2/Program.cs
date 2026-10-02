using System;

namespace lab5_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee e1 = new Employee(1, "Tasnim", Branch.Cairo, Permissions.Read | Permissions.Write);
            Employee e2 = new Employee(2, "Marwa", Branch.Alex, Permissions.Delete);
            Employee e3 = new Employee(1, "Tasnim Copy", Branch.Mansoura, Permissions.Read);
            Employee e4 = new Employee(); // empty

            Console.WriteLine(e1);
            Console.WriteLine(e2);
            Console.WriteLine(e3);
            Console.WriteLine(e4);

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Test Equality");
            Console.WriteLine($"e1 == e2 ? {e1 == e2}");  // false, different Ids
            Console.WriteLine($"e1 == e3 ? {e1 == e3}");  // true, same Id
            Console.WriteLine($"e1.Equals(e3)? {e1.Equals(e3)}"); // true, same Id
            Console.WriteLine($"e1 != e2 ? {e1 != e2}");  // true

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Merge Permissions");
            Employee merged = e1 + e2; 
            Console.WriteLine(merged);

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("Test Explicit Cast to string");
            string asString = (string)e1;
            Console.WriteLine(asString); // "Employee: 1 - Tasnim - Cairo"

           
        }
    }
}