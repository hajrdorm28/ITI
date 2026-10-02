using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_3
{
    struct Item
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
            Console.WriteLine($"ID: {Id}\n" +
                $"Name: {Name}\n" +
                $"Prirce: {Price}\n" +
                $"Item Category: {Category}\n");
        }
    }
}
