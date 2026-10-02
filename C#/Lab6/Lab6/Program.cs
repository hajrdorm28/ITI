using System;
using System.Collections.Generic;

namespace Lab6_Tasks
{

    #region Ex. 1
    class Animal
    {
        public virtual void Speak()
        {
            Console.WriteLine("Animal: some sound");
        }

        public void Move()
        {
            Console.WriteLine("Animal: moves somehow");
        }
    }

    class Dog : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("Cat: Meow!");
        }

        public new void Move()
        {
            Console.WriteLine("Cat: Runs on four legs");
        }
    }

    static class Binding
    {
        public static void Run()
        {
            Console.WriteLine("=== 1. Binding: Early vs Late ===");

            Dog dogObj = new Dog();
            Animal animalRef = dogObj;

            Console.WriteLine("-- Late binding (virtual) --");
            dogObj.Speak();
            animalRef.Speak();

            Console.WriteLine("-- Early binding (non-virtual) --");
            dogObj.Move();
            animalRef.Move();
            Console.WriteLine();
        }
    }
    #endregion

    #region Ex. 2
    interface IPlayable
    {
        void Play();
        void Pause();
    }

    enum PlaybackState { Stopped, Playing, Paused }

    abstract class MediaPlayerBase : IPlayable
    {
        protected PlaybackState State { get; set; } = PlaybackState.Stopped;
        protected abstract string MediaTypeName { get; }

        public void Play()
        {
            if (State == PlaybackState.Playing)
                throw new InvalidOperationException($"{MediaTypeName} is already playing — cannot call Play() again.");

            State = PlaybackState.Playing;
            Console.WriteLine($"{MediaTypeName}: Playing...");
        }

        public void Pause()
        {
            if (State == PlaybackState.Paused)
                throw new InvalidOperationException($"{MediaTypeName} is already paused — cannot call Pause() again.");
            if (State != PlaybackState.Playing)
                throw new InvalidOperationException($"{MediaTypeName} must be playing before it can be paused.");

            State = PlaybackState.Paused;
            Console.WriteLine($"{MediaTypeName}: Paused.");
        }
    }

    class AudioPlayer : MediaPlayerBase
    {
        protected override string MediaTypeName => "AudioPlayer";
    }

    class VideoPlayer : MediaPlayerBase
    {
        protected override string MediaTypeName => "VideoPlayer";
    }

    static class Interface
    {
        public static void Run()
        {
            Console.WriteLine("=== 2. Interfaces: IPlayable state guarding ===");

            IPlayable audio = new AudioPlayer();
            audio.Play();
            TryAction(() => audio.Play(), "Play() again while already playing");

            audio.Pause();
            TryAction(() => audio.Pause(), "Pause() again while already paused");

            IPlayable video = new VideoPlayer();
            TryAction(() => video.Pause(), "Pause() before ever playing");
            video.Play();
            video.Pause();
            Console.WriteLine();
        }

        private static void TryAction(Action action, string description)
        {
            try
            {
                action();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Blocked ({description}): {ex.Message}");
            }
        }
    }
    #endregion

    #region Ex. 3
    abstract class Shape3D
    {
        public abstract double GetVolume();
        public abstract double GetSurfaceArea();
        public abstract string Name { get; }

        public void Describe()
        {
            Console.WriteLine($"{Name}: Volume = {GetVolume():F2}, Surface Area = {GetSurfaceArea():F2}");
        }
    }

    class Sphere : Shape3D
    {
        private readonly double _radius;
        public Sphere(double radius) => _radius = radius;
        public override string Name => "Sphere";
        public override double GetVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(_radius, 3);
        public override double GetSurfaceArea() => 4 * Math.PI * Math.Pow(_radius, 2);
    }

    class Cube : Shape3D
    {
        private readonly double _side;
        public Cube(double side) => _side = side;
        public override string Name => "Cube";
        public override double GetVolume() => Math.Pow(_side, 3);
        public override double GetSurfaceArea() => 6 * Math.Pow(_side, 2);
    }

    class Cylinder : Shape3D
    {
        private readonly double _radius;
        private readonly double _height;
        public Cylinder(double radius, double height)
        {
            _radius = radius;
            _height = height;
        }
        public override string Name => "Cylinder";
        public override double GetVolume() => Math.PI * Math.Pow(_radius, 2) * _height;
        public override double GetSurfaceArea() => 2 * Math.PI * _radius * (_radius + _height);
    }

    static class Abstraction
    {
        public static void Run()
        {
            Console.WriteLine("=== 3. Abstraction: Shape3D hierarchy ===");

            List<Shape3D> shapes = new List<Shape3D>
            {
                new Sphere(radius: 3),
                new Cube(side: 4),
                new Cylinder(radius: 2, height: 5)
            };

            foreach (var shape in shapes)
                shape.Describe();

            Console.WriteLine();
        }
    }
    #endregion

    #region Ex. 4
    interface IMovable
    {
        void Move();
    }

    abstract class Character
    {
        public string Name { get; }
        public int Health { get; protected set; }

        protected Character(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public abstract void Attack(Character target);

        public bool IsAlive => Health > 0;

        public void ReceiveDamage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
        }
    }

    class Hero : Character, IMovable
    {
        private readonly int _attackPower;

        public Hero(string name, int health, int attackPower) : base(name, health)
        {
            _attackPower = attackPower;
        }

        public void Move()
        {
            Console.WriteLine($"{Name} (Hero) charges forward!");
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} swings a sword at {target.Name} for {_attackPower} damage!");
            target.ReceiveDamage(_attackPower);
        }
    }

    class Enemy : Character
    {
        private readonly int _biteDamage;

        public Enemy(string name, int health, int biteDamage) : base(name, health)
        {
            _biteDamage = biteDamage;
        }

        private readonly Random _rng = new Random();

        public override void Attack(Character target)
        {
            bool isCritical = _rng.Next(0, 2) == 1;
            int damage = isCritical ? _biteDamage * 2 : _biteDamage;
            string style = isCritical ? "CRITICAL bite" : "bite";

            Console.WriteLine($"{Name} lands a {style} on {target.Name} for {damage} damage!");
            target.ReceiveDamage(damage);
        }
    }

    static class MixedChallengeDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== Battle Simulation ===");

            Character hero = new Hero("Eren", health: 30, attackPower: 8);
            Character enemy = new Enemy("Goblin", health: 25, biteDamage: 6);

            Character[] combatants = { hero, enemy };

            foreach (var c in combatants)
            {
                if (c is IMovable movable)
                    movable.Move();
            }

            int round = 1;
            while (hero.IsAlive && enemy.IsAlive)
            {
                Console.WriteLine($"Round {round}");
                hero.Attack(enemy);
                if (!enemy.IsAlive) break;

                enemy.Attack(hero);
                round++;
            }

            Console.WriteLine();
            Console.WriteLine(hero.IsAlive
                ? $"{hero.Name} wins with {hero.Health} HP left!"
                : $"{enemy.Name} wins with {enemy.Health} HP left!");
            Console.WriteLine();
        }
    } 
    #endregion
}

