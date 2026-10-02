using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_1
{
    class StudentManager
    {
        public void Promote(ref Student student)
        {
            if (student.Level < GradeLevel.Senior)
                student.Level++;
        }

        public void GetTopStudent(Student[] students, out Student topStudent)
        {
            if (students == null || students.Length == 0)
                Console.WriteLine("Students array is empty.");

            topStudent = students[0];
            for (int i = 1; i < students.Length; i++)
            {
                if (students[1].GPA > topStudent.GPA)
                    topStudent = students[1];
            }
        }

        public void PrintStudent(in Student student)
        {
            student.PrintInfo();
        }
    }
}
