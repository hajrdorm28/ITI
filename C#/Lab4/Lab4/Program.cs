#region Task 1
namespace StudentGrades
{
    public enum GradeLevel
    {
        Freshman,
        Sophomore,
        Junior,
        Senior
    }
    public struct Student
    {
        public string Name;
        public double GPA;
        public GradeLevel Level;

        public Student(string name, double gpa, GradeLevel level)
        {
            Name = name;
            GPA = gpa;
            Level = level;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Student Info:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"GPA: {GPA}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine();
        }
    }

    public class StudentManager
    {
        public void Promote(ref Student student)
        {
            if (student.Level < GradeLevel.Senior)
            {
                student.Level = student.Level + 1;
            }
        }

        public void GetTopStudent(Student[] students, out Student topStudent)
        {
            topStudent = students[0];
            for (int i = 1; i < students.Length; i++)
            {
                if (students[i].GPA > topStudent.GPA)
                {
                    topStudent = students[i];
                }
            }
        }

        public void PrintStudent(in Student student)
        {
            student.PrintInfo();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();

            Student[] students = new Student[3];
            students[0] = new Student("Alice", 3.5, GradeLevel.Freshman);
            students[1] = new Student("Bob", 3.9, GradeLevel.Junior);
            students[2] = new Student("Carol", 3.2, GradeLevel.Senior);

            Console.WriteLine($"Before Promotion: {students[0].Name} - {students[0].Level}");
            manager.Promote(ref students[0]);
            Console.WriteLine($"After Promotion: {students[0].Name} - {students[0].Level}");
            Console.WriteLine();

            manager.GetTopStudent(students, out Student topStudent);
            Console.WriteLine($"Top Student: {topStudent.Name} - GPA {topStudent.GPA}");
            Console.WriteLine();

            foreach (Student s in students)
            {
                manager.PrintStudent(in s);
            }
        }
    }
}
#endregion

#region Task 2
namespace OrderManagement
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
    public struct Order
    {
        public int Id;
        public string CustomerName;
        public double Amount;
        public OrderStatus Status;

        public Order(int id, string customerName, double amount, OrderStatus status)
        {
            Id = id;
            CustomerName = customerName;
            Amount = amount;
            Status = status;
        }

        public override string ToString()
        {
            return $"ID: {Id} \n Customer: {CustomerName} \n Amount: {Amount} \n Status: {Status}\n";
        }
    }

    public class OrderManager
    {
        public void UpdateStatus(ref Order order, OrderStatus newStatus)
        {
            order.Status = newStatus;
        }

        public void GetOrderStats(Order[] orders, out int totalOrders, out double totalAmount)
        {
            totalOrders = orders.Length;
            totalAmount = 0;
            foreach (Order o in orders)
            {
                totalAmount += o.Amount;
            }
        }

        public void PrintOrder(in Order order)
        {
            Console.WriteLine(order.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            OrderManager manager = new OrderManager();

            Order[] orders = new Order[3];
            orders[0] = new Order(1, "Alice", 250, OrderStatus.Pending);
            orders[1] = new Order(2, "Bob", 300, OrderStatus.Processing);
            orders[2] = new Order(3, "Carol", 200, OrderStatus.Delivered);

            Console.WriteLine($"Before Update: Order #{orders[0].Id} - {orders[0].Status}");
            manager.UpdateStatus(ref orders[0], OrderStatus.Shipped);
            Console.WriteLine($"After Update: Order #{orders[0].Id} - {orders[0].Status}");
            Console.WriteLine();

            manager.GetOrderStats(orders, out int totalOrders, out double totalAmount);
            Console.WriteLine("Order Stats:");
            Console.WriteLine($"Total Orders = {totalOrders}");
            Console.WriteLine($"Total Amount = {totalAmount}");
            Console.WriteLine();

            Console.WriteLine("Order Details:");
            foreach (Order o in orders)
            {
                manager.PrintOrder(in o);
            }
        }
    }
}
#endregion

#region Task 3 
namespace Inventory
{
    public enum ItemCategory
    {
        Electronics,
        Grocery,
        Clothing,
        Stationery
    }

    public struct Item
    {
        public int Id;
        public string Name;
        public double Price;
        public ItemCategory Category;

        public Item(int id, string name, double price, ItemCategory category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine();
        }
    }

    public class Inventory
    {
        private Item[] items;

        public Inventory(int capacity)
        {
            items = new Item[capacity];
        }

        public void AddItem(Item item, int index)
        {
            if (index < 0 || index >= items.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is outside inventory bounds.");
            }
            items[index] = item;
        }

        public Item this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }

        public Item[] this[ItemCategory category]
        {
            get
            {
                List<Item> matches = new List<Item>();
                foreach (Item item in items)
                {
                    if (item.Category == category && item.Name != null)
                    {
                        matches.Add(item);
                    }
                }
                return matches.ToArray();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory(3);

            Item laptop = new Item(1, "Laptop", 1000, ItemCategory.Electronics);
            Item milk = new Item(2, "Milk", 2.5, ItemCategory.Grocery);
            Item shirt = new Item(3, "Shirt", 25, ItemCategory.Clothing);

            inventory.AddItem(laptop, 0);
            inventory.AddItem(milk, 1);
            inventory.AddItem(shirt, 2);

            Console.WriteLine("Item at index 1:");
            inventory[1].PrintInfo();

            Console.WriteLine("Items in Electronics:");
            foreach (Item item in inventory[ItemCategory.Electronics])
            {
                item.PrintInfo();
            }
        }
    }
}
#endregion



