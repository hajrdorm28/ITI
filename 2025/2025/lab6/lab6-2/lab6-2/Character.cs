using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_2
{
    public abstract class Character
    {
        public string? Name;
        public int Health;

        public Character(string name, int health)
        {
            Name = name;
            Health = health;
        }
        public bool IsAlive() => Health > 0;
        public abstract void Attack(Character target);
        public override string ToString() => $"Name: {Name}\nHealth: {Health}";
    }
}
