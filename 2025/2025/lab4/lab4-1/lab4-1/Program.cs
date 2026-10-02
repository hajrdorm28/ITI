using System;

namespace lab4_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an array of 3 students.
            var students = new Student[]
            {
                new Student("Tasnim Alkhatib", 4, GradeLevel.Senior),
                new Student("Marwa Ahmed", 3.5, GradeLevel.Freshman),
                new Student("Ahmed Ali", 3.7, GradeLevel.Junior)
            };

            // Promote one student.
            var manage = new StudentManager();
            manage.Promote(ref students[0]);

            // Get and print the top student.
            manage.GetTopStudent(students, out Student topStudent);
            
            Console.WriteLine("Top Student");
            manage.PrintStudent(in topStudent);

            // Print all students.
            Console.WriteLine("------------------------------------------");
            foreach (var std in students)
                std.PrintInfo();
        }
    }
}