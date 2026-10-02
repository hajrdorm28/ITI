using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_2
{
    public class Enemy : Character
    {
        public Enemy(string name, int health) : base(name, health) { }
        public override void Attack(Character target)
        {
            target.Health -= 5;
            Console.WriteLine($"{Name} hits {target.Name}, {target.Name} HP = {target.Health}");
        }
    }
}
