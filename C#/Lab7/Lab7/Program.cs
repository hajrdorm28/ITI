using System;
using System.Text;

namespace lab7
{
    #region Task 1
    public class DynamicArray<T>
    {
        private T[] _items;
        private int _length;

        public int Length => _length;
        public int Capacity => _items.Length;

        public DynamicArray()
        {
            _items = new T[2];
            _length = 0;
        }
        public void Push(T item)
        {
            if (_length == Capacity)
            {
                T[] newArr = new T[Capacity * 2];
                for (int i = 0; i < _length; i++)
                    newArr[i] = _items[i];
                _items = newArr;
            }
            _items[_length++] = item;
        }
        public T Pop()
        {
            if (_length == 0)
                throw new InvalidOperationException("Error: Array is empty!");

            return _items[--_length];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _length)
                    throw new IndexOutOfRangeException("Invalid index");
                return _items[index];
            }
        }
    }
    #endregion

    #region Task 2
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StringBuilder Notes { get; set; }
        public Student(int id, string name, StringBuilder notes)
        {
            Id = id;
            Name = name;
            Notes = notes;
        }
        public Student DeepCopy() => new Student(this.Id, this.Name, new StringBuilder(this.Notes.ToString()));

    }
    #endregion

    #region Task 3
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
            if (other == null)
                return 1;
            return Salary.CompareTo(other.Salary);
        }
        public override string ToString() => $"  ID: {Id}" +
            $"\n  Name: {Name} " +
            $"\n  Salary: {Salary}\n";
    }

    public class EmployeeNameComparer : IComparer<Employee>
    {
        public int Compare(Employee? emp1, Employee? emp2)
        {
            return String.Compare(emp1?.Name, emp2?.Name);
        }
    }
    #endregion



    internal class Program
    {
        #region Task 3 con.
        public static void PrintEmployees(Employee[] employees, string message)
        {
            Console.WriteLine(message);
            foreach (var employee in employees)
                Console.WriteLine(employee);
            Console.WriteLine();
        } 
        #endregion
        static void Main(string[] args)
        {
            #region Task 1 run
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region Task 2 run
            Student s1 = new Student(1, "Ali", new StringBuilder("Good"));
            Student s2 = s1;
            Student s3 = s1.DeepCopy();
            s1.Notes.Append(" Student");
            Console.WriteLine($"s2 Notes: {s2.Notes}");
            Console.WriteLine($"s3 Notes: {s3.Notes}");
            #endregion

            #region Task 3 run
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
            #endregion
        }
    }
}


