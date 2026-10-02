using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6_1
{
    // early binding => non-virtual (compile time)
    public class Animal_Early
    {
        public void Speak() => Console.WriteLine("  Animal_Early speaks (base).");
    }
    public class Dog_Early : Animal_Early
    {
        public new void Speak() => Console.WriteLine("  Dog_Early barks.");
    }

    // late binding => virtual/override (runtime)
    public class Animal_Late
    {
        public virtual void Speak() => Console.WriteLine("  Animal_Late speaks (base).");
    }
    public class Dog_Late : Animal_Late
    {
        public override void Speak() => Console.WriteLine("  Dog_Late barks (override).");
    }
}
