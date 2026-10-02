using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_2
{
    public class Hero : Character, IMovable
    {
        public Hero(string name, int health) : base(name, health) { }
        public void Move() => Console.WriteLine($"{Name} moved forward");
        public override void Attack(Character target)
        {
            target.Health -= 10;
            Console.WriteLine($"{Name} attacks {target.Name}, {target.Name} HP = {target.Health}");
        }
    }
}
