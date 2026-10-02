using System;

namespace lab4_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an Inventory with capacity 3.
            var inventory = new Inventory(3);

            // Create 3 items (different categories).
            var items = new Item[]
            {
                new Item(1, "Laptop", 1500, ItemCategory.Electronics),
                new Item(2, "Apples", 20, ItemCategory.Grocery),
                new Item(3, "T-Shirt", 100, ItemCategory.Clothing)
            };

            // Add them to inventory.
            for (int i = 0; i < items.Length; i++)
                inventory.AddItem(items[i], i);

            // Use the indexer to access an item by index.
            for (int i = 0;i < items.Length; i++)
            {
                Console.WriteLine($"Item at index {i}");
                inventory[i].PrintInfo();
            }


            //Use the category indexer to list all items of a given category.
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("All Category items (Clothing)");
            foreach (var item in inventory[ItemCategory.Clothing]) 
                item.PrintInfo();

        }
    }
}