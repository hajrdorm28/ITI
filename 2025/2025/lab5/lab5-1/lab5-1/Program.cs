using System;

namespace lab5_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();

            Console.WriteLine($"Initial: {player}\n");

            player.TryAdd(PlaybackOptions.Play);
            player.TryAdd(PlaybackOptions.Next);

            Console.WriteLine($"Current Options: {player}\n"); // play + next

            Console.Write("Which option to check? ");
            string input = Console.ReadLine();
            Enum.TryParse(input, true, out PlaybackOptions optToCheck);
            Console.WriteLine($"Contains {optToCheck}? {player.Has(optToCheck)}\n");

            player.TogglePause();
            Console.WriteLine($"Toggled Pause -> {player}\n");

            Console.Write("Which option to remove? ");
            input = Console.ReadLine();
            Enum.TryParse(input, true, out PlaybackOptions optToRemove);
            player.Remove(optToRemove);
            Console.WriteLine($"Removed {optToRemove} -> {player}");
        }
    }
}