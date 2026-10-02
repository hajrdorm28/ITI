using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5_3
{
    public class Transaction
    {
        public int Id { get; }
        public decimal Amount { get; }
        public TransactionType Type { get; }

        public Transaction(int id, decimal amount, TransactionType type)
        {
            Id = id < 0 ? 0 : id;
            Amount = amount < 0 ? 0 : amount;
            Type = type;
        }

        // to add two transactions (merges amounts if type is same).
        public static Transaction operator +(Transaction? a, Transaction? b)
        {
            if (a is null && b is null) return new Transaction(0, 0, TransactionType.Deposit);
            if (a is null) return new Transaction(b.Id, b.Amount, b.Type);
            if (b is null) return new Transaction(a.Id, a.Amount, a.Type);

            if (a.Type == b.Type)
            {
                var sum = a.Amount + b.Amount;
                var maxId = Math.Max(a.Id, b.Id);
                return new Transaction(maxId, sum, a.Type);
            }

            return new Transaction(a.Id, a.Amount, a.Type);
        }

        public override string ToString() => $"Transaction {Id} => {Type} {Amount}";
    }
}
