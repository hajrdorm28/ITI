using lab7_3;
using System;

namespace lab7_3
{
    internal class Program
    {
        public static void PrintEmployees(Employee[] employees, string message)
        {
            Console.WriteLine(message);
            foreach (var employee in employees)
                Console.WriteLine(employee);
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Employee[] employees = {
                new Employee(1, 5000, "Omar"),
                new Employee(2, 2000, "Ali"),
                new Employee(3, 7000, "Nada")
            };

            Array.Sort(employees);
            PrintEmployees(employees, "Sorted by Salary:");

            Console.WriteLine("------------------------------------------\n");
            Array.Sort(employees, new EmployeeNameComparer());
            PrintEmployees(employees, "Sorted by Name:");

        }
    }
}