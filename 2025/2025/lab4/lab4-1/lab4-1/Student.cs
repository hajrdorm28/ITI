using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_1
{
    struct Student
    {
        public string Name;
        public double GPA;
        public GradeLevel Level;

        public Student(string name, double GPA_, GradeLevel level)
        {
            Name = name;
            GPA = GPA_;
            Level = level;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Student Name: {Name}\n" +
                $"GPA: {GPA}\n" +
                $"Level: {Level}\n");
        }
    }
}
