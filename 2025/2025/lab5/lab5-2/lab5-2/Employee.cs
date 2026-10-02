using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab5_2
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Branch Branch { get; set; }
        public Permissions Permissions { get; set; }

        // Constructor chaining
        public Employee() : this(0, string.Empty, Branch.Kafr_Elshiekh, Permissions.None) { }

        public Employee(int id, string name) : this(id, name, Branch.Kafr_Elshiekh, Permissions.None) { }

        public Employee(int id, string name, Branch branch, Permissions permissions)
        {
            Id = id;
            Name = name;
            Branch = branch;
            Permissions = permissions;
        }

        // Overload + operator → to merge permissions of two employees.
        public static Employee operator +(Employee? a, Employee? b)
        {
            if (a is null && b is null)
                return new Employee(); // empty

            if (a is null)
                return new Employee(b.Id, b.Name ?? string.Empty, b.Branch, b.Permissions);

            if (b is null)
                return new Employee(a.Id, a.Name ?? string.Empty, a.Branch, a.Permissions);

            return new Employee(a.Id, a.Name ?? string.Empty, a.Branch, a.Permissions | b.Permissions);
        }

        // Overload == and !=
        public static bool operator ==(Employee? a, Employee? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Id == b.Id;
        }

        public static bool operator !=(Employee? a, Employee? b) => !(a == b);


        // employees are equal if Id matches.  
        public override bool Equals(object obj)
        {
            var emp = obj as Employee;
            if(emp == null)
                return false;

            return this.Id == emp.Id;
        }
        public override int GetHashCode() => Id.GetHashCode();

        // Explicit cast Employee -> string
        public static explicit operator string(Employee e)
            => e is null 
            ? "Employee: 0 -  - Kafr_Elshiekh"
            : $"Employee: {e.Id} - {e.Name} - {e.Branch}";


        public override string ToString() => $"ID: {Id} \nName: {Name} \nBranch: {Branch} \nPerms: {Permissions}\n";
    }
}
