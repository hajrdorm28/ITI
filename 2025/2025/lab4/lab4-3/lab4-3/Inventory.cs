using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_3
{
    class Inventory
    {
        private Item[] items;

        public Inventory(int capacity)
        {
            items = new Item[capacity];
        }

        public void AddItem(Item item, int index)
        {
            CheckIndex(index);
            items[index] = item;
        }

        public Item this[int index]
        {
            get
            {
                CheckIndex(index);
                return items[index];
            }
            set
            {
                CheckIndex(index);
                items[index] = value;
            }
        }

        public Item[] this[ItemCategory category]
        {
            get
            {
                int count = 0;
                foreach (Item item in items) 
                    if (item.Category == category) 
                        count++;

                Item[] result = new Item[count];
                
                int index = 0;
                CheckIndex(index);

                foreach (Item item in items)
                    if(item.Category == category)
                        result[index++] = item;

                return result;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= items.Length)
                Console.WriteLine("Invalid index!\n");
        }
    }
}
