using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_3
{
    public class Account
    {
        // Private balance.
        private decimal balance;

        public int Id { get; }
        public string Owner { get; }
        public AccountType Type { get; }


        // Constructor chaining
        public Account() : this(0, "Unknown", AccountType.Checking, 0m) { }
        public Account(int id, string owner) : this(id, owner, AccountType.Checking, 0m) { }
        public Account(int id, string owner, AccountType type) : this(id, owner, type, 0m) { }
        public Account(int id, string owner, AccountType type, decimal initialBalance)
        {
            Id = (id < 0) ? 0 : id; 
            Owner = owner ?? "Unknown";
            Type = type;
            balance = (initialBalance < 0) ? 0m : initialBalance; 
        }

        public decimal Deposit(decimal amount)
        {
            if (amount > 0) 
                balance += amount;
            return balance;
        }

        public decimal Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= balance)
                balance -= amount;
            return balance;
        }

        public static bool operator >(Account a, Account b)
        {
            return a.balance > b.balance;
        }

        public static bool operator <(Account a, Account b)
        {
            return a.balance < b.balance;
        }

        public static explicit operator double(Account a) => (double)(a?.balance ?? 0m);

        public override string ToString() => $"ID: {Id}\nOwner: {Owner}\nType: {Type}\nBalance: {balance}\n";
    }
}
