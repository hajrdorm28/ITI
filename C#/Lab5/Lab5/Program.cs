
#region Task 1
[Flags]
enum PlaybackOptions
{
    None = 0,
    Play = 1,
    Pause = 2,
    Stop = 4,
    Next = 8,
    Previous = 16
}

class Player
{
    public PlaybackOptions CurrentOptions { get; private set; }

    public Player()
    {
        CurrentOptions = PlaybackOptions.None;
    }

    public bool Validate(PlaybackOptions option)
    {
        if ((option.HasFlag(PlaybackOptions.Play) && (option.HasFlag(PlaybackOptions.Pause) || option.HasFlag(PlaybackOptions.Stop))))
            return false;

        if (option.HasFlag(PlaybackOptions.Next) &&
            option.HasFlag(PlaybackOptions.Previous))
            return false;

        return true;
    }

    public void AddOption(PlaybackOptions option)
    {
        PlaybackOptions temp = CurrentOptions | option;

        if (Validate(temp))
            CurrentOptions = temp;
        else
            Console.WriteLine("Invalid Combination!");
    }

    public void RemoveOption(PlaybackOptions option)
    {
        CurrentOptions &= ~option;
    }

    public bool Contains(PlaybackOptions option)
    {
        return CurrentOptions.HasFlag(option);
    }

    public void TogglePause()
    {
        PlaybackOptions temp = CurrentOptions ^ PlaybackOptions.Pause;

        if (Validate(temp))
            CurrentOptions = temp;
        else
            Console.WriteLine("Cannot Toggle Pause.");
    }
}

class PlayerPlayback
{
    static PlaybackOptions ReadOption()
    {
        Console.WriteLine("1.Play");
        Console.WriteLine("2.Pause");
        Console.WriteLine("3.Stop");
        Console.WriteLine("4.Next");
        Console.WriteLine("5.Previous");

        int choice = int.Parse(Console.ReadLine()!);

        return choice switch
        {
            1 => PlaybackOptions.Play,
            2 => PlaybackOptions.Pause,
            3 => PlaybackOptions.Stop,
            4 => PlaybackOptions.Next,
            5 => PlaybackOptions.Previous,
            _ => PlaybackOptions.None
        };
    }

    static void Main()
    {
        Player player = new Player();

        Console.WriteLine("Initial: " + player.CurrentOptions);

        Console.WriteLine("Add First Option:");
        player.AddOption(ReadOption());

        Console.WriteLine("Add Second Option:");
        player.AddOption(ReadOption());

        Console.WriteLine("Current Options: " + player.CurrentOptions);

        Console.WriteLine("Which option to check?");
        PlaybackOptions check = ReadOption();

        Console.WriteLine("Contains? " + player.Contains(check));

        player.TogglePause();
        Console.WriteLine("After Toggle Pause: " + player.CurrentOptions);

        Console.WriteLine("Which option to remove?");
        PlaybackOptions remove = ReadOption();

        player.RemoveOption(remove);

        Console.WriteLine("Final Options: " + player.CurrentOptions);
    }
}
#endregion


#region Task 2
[Flags]
enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4,
    Execute = 8
}

enum Branch
{
    Cairo,
    Alex,
    Mansoura,
    Tanta
}

class Employee
{
    public int Id;
    public string Name;
    public Branch Branch;
    public Permissions Permissions;

    public Employee()
        : this(0, "Unknown")
    {
    }

    public Employee(int id, string name)
        : this(id, name, Branch.Cairo, Permissions.None)
    {
    }

    public Employee(int id, string name, Branch branch, Permissions permissions)
    {
        Id = id;
        Name = name;
        Branch = branch;
        Permissions = permissions;
    }

    public static Employee operator +(Employee e1, Employee e2)
    {
        return new Employee(
            e1.Id,
            e1.Name,
            e1.Branch,
            e1.Permissions | e2.Permissions
        );
    }
    public static bool operator ==(Employee? e1, Employee? e2)
    {
        if (ReferenceEquals(e1, e2)) return true;
        if (e1 is null || e2 is null) return false;
        return e1.Id == e2.Id;
    }

    public static bool operator !=(Employee? e1, Employee? e2)
    {
        return !(e1 == e2);
    }

    public override bool Equals(object? obj)
    {
        return obj is Employee emp && emp.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static explicit operator string(Employee e)
    {
        return $"Employee: {e.Id} - {e.Name} - {e.Branch}";
    }
}

class EmployeeMain
{
    static void Main()
    {
        Employee e1 = new Employee(1, "Ali", Branch.Cairo,
            Permissions.Read | Permissions.Write);

        Employee e2 = new Employee(2, "Omar", Branch.Alex,
            Permissions.Delete | Permissions.Execute);

        Employee merged = e1 + e2;

        Console.WriteLine(merged.Permissions);

        Console.WriteLine(e1 == e2);

        string info = (string)e1;

        Console.WriteLine(info);
    }
}
#endregion


#region Task 3
enum AccountType
{
    Savings,
    Checking,
    Business
}

enum TransactionType
{
    Deposit,
    Withdraw
}

class Transaction
{
    public int Id;
    public double Amount;
    public TransactionType Type;

    public Transaction(int id, double amount, TransactionType type)
    {
        Id = id;
        Amount = amount;
        Type = type;
    }

    public static Transaction operator +(Transaction t1, Transaction t2)
    {
        if (t1.Type != t2.Type)
            throw new Exception("Transaction types must match.");

        return new Transaction(
            t1.Id,
            t1.Amount + t2.Amount,
            t1.Type
        );
    }

    public override string ToString()
    {
        return $"{Id} - {Type} - {Amount}";
    }
}

class Account
{
    private double balance;

    public int Id;
    public string Owner;
    public AccountType Type;

    public Account()
        : this(0, "Unknown")
    {
    }

    public Account(int id, string owner)
        : this(id, owner, AccountType.Savings, 0)
    {
    }

    public Account(int id, string owner, AccountType type, double balance)
    {
        Id = id;
        Owner = owner;
        Type = type;
        this.balance = balance;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit amount must be positive.");
            return;
        }
        balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdraw amount must be positive.");
            return;
        }
        if (amount <= balance)
            balance -= amount;
        else
            Console.WriteLine("Insufficient Balance.");
    }

    public static bool operator >(Account a1, Account a2)
    {
        return a1.balance > a2.balance;
    }

    public static bool operator <(Account a1, Account a2)
    {
        return a1.balance < a2.balance;
    }

    public static explicit operator double(Account a)
    {
        return a.balance;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Owner: {Owner}, Type: {Type}, Balance: {balance}";
    }
}

class BankMangSys
{
    static void Main()
    {
        Account a1 = new Account(1, "Ali", AccountType.Savings, 1000);
        Account a2 = new Account(2, "Omar", AccountType.Business, 1500);

        a1.Deposit(500);
        a2.Withdraw(200);

        Console.WriteLine(a1);
        Console.WriteLine(a2);

        Console.WriteLine(a1 > a2);

        double balance = (double)a1;

        Console.WriteLine(balance);

        Transaction t1 = new Transaction(1, 500, TransactionType.Deposit);
        Transaction t2 = new Transaction(2, 300, TransactionType.Deposit);

        Transaction total = t1 + t2;

        Console.WriteLine(total);
    }
} 
#endregion

