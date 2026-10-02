using System;

namespace lab5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var acc1 = new Account(101, "Tasnim", AccountType.Savings, 1000m);
            var acc2 = new Account(102, "Marwa", AccountType.Business, 500m);

            Console.WriteLine("Before Deposit, Withdraw");
            Console.WriteLine(acc1);
            Console.WriteLine(acc2);

            acc1.Deposit(250m);   
            acc2.Deposit(-50m);   
            acc2.Withdraw(900m);  
            acc2.Deposit(900m);

            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("After Deposit, Withdraw");
            Console.WriteLine(acc1);
            Console.WriteLine(acc2);


            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"acc1 > acc2 ? {acc1 > acc2}");
            Console.WriteLine($"acc1 < acc2 ? {acc1 < acc2}");


            Console.WriteLine("\n-----------------------------------------------------");
            var t1 = new Transaction(1, 200m, TransactionType.Deposit);
            var t2 = new Transaction(2, 300m, TransactionType.Deposit);
            var t3 = t1 + t2;

            Console.WriteLine(t1);
            Console.WriteLine(t2);
            Console.WriteLine($"Merged same type(Deposit): {t3}");
            
            
            Console.WriteLine("\n-----------------------------------------------------");
            var t4 = new Transaction(3, 50m, TransactionType.Withdraw);
            var t5 = t1 + t4; 

            Console.WriteLine($"Merged different type (keeps first): {t5}");


            Console.WriteLine("\n-----------------------------------------------------");
            double b1 = (double)acc1;
            double b2 = (double)acc2;
            Console.WriteLine($"Balances as double → acc1: {b1}, acc2: {b2}");
        }
    }
}