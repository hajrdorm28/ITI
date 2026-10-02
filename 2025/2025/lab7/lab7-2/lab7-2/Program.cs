using lab7_3;
using System;
using System.Text;

namespace lab7_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new Student(1, "Ali", new StringBuilder("Good"));
            Student s2 = s1;
            Student s3 = s1.DeepCopy();
            s1.Notes.Append(" Student");
            Console.WriteLine($"s2 Notes: {s2.Notes}");
            Console.WriteLine($"s3 Notes: {s3.Notes}");
        }
    }
}