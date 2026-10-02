using System;

namespace lab6_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character[] team = new Character[3];
            team[0] = new Hero("A", 30);   
            team[1] = new Enemy("B", 22);
            team[2] = new Enemy("C", 18);

            for (int i = 0; i < team.Length; i++)
            {
                IMovable? m = team[i] as IMovable;
                if (m != null) 
                    m.Move();
            }

            Hero hero = (Hero)team[0];

            for (int i = 1; i < team.Length && hero.Health > 0; i++)
            {
                Character enemy = team[i];
                Console.WriteLine($"\n{hero.Name} vs {enemy.Name}");

                while (hero.Health > 0 && enemy.Health > 0)
                {
                    hero.Attack(enemy);       
                    if (enemy.Health <= 0) 
                        break;
                    enemy.Attack(hero);        
                }

                Console.WriteLine(hero.Health > 0 
                    ? $"{hero.Name} wins!" 
                    : $"{enemy.Name} wins!");
            }
            Console.WriteLine($"\nFinal: {(hero.Health > 0 
                ? "Hero survived" 
                : "Hero fell")}");
        }
    }
}