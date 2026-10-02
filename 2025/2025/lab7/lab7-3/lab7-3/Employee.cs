using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7_3
{
    public class Employee : IComparable<Employee>
    {
        public int Id;
        public int Salary;
        public string? Name;

        public Employee(int id, int salary, string name) 
        {
            Id = id;
            Salary = salary;
            Name = name;
        }
        public int CompareTo(Employee? other)
        {
            if(other == null) 
                return 1; 
            return Salary.CompareTo(other.Salary);
        }
        public override string ToString() => $"  ID: {Id}" +
            $"\n  Name: {Name} " +
            $"\n  Salary: {Salary}\n";
    }
}
