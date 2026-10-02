using System;
using System.Diagnostics;
using System.Globalization;

namespace lab4_4
{
    internal class Program
    {
        // validate the inputs
        public static string GetStr(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }
        public static int GetInt(string message)
        {
            while (true)
            {
                var str = GetStr(message);
                if(int.TryParse(str, out var val)) 
                    return val;

                Console.WriteLine("\nInvalid number, please try again.\n");
            }
        }
        public static Genre GetGenre()
        {
            Console.WriteLine("Geners:\n" +
                "0. Action\n" +
                "1. Comedy\n" +
                "2. Drama\n" +
                "3. Horror\n" +
                "4. SciFi");

            while (true)
            {
                int g = GetInt("Genre number: ");
                if (g >= 0 && g <= 4)
                    return (Genre)g; // return the genre based on its number

                Console.WriteLine("\nInvalid input, please enter the genre number again.\n");
            }
        }


        // print the oldest and newest movie
        public static void FindOldestAndNewest(Movie[] movies, out Movie oldest, out Movie newest)
        {
            if (movies == null || movies.Length == 0)
                Console.WriteLine("Movies array is empty.\n");

            oldest = movies[0];
            newest = movies[0];

            for (int i = 1; i < movies.Length; i++)
            {
                if (movies[i].Year > newest.Year)
                    newest = movies[i];

                if(movies[i].Year < oldest.Year)
                    oldest = movies[i];
            }
        }
       
        static void Main(string[] args)
        {
            // Ask the user: “How many movies do you want to add?”
            int num = 0;
            do
            {
                Console.Write("How many movies do you want to add? ");
                if (!int.TryParse(Console.ReadLine(), out num) || num <= 0)
                   {
                    Console.WriteLine("Invalid input, please try again\n");
                    num = 0; 
                }

            } while (num <= 0);

            // Create an array of Movie with that size.
            Movie[] movies = new Movie[num];


            //For each movie:
            //Ask user to enter title, year, and genre number (e.g., 0=Action, 1=Comedy…).
            //Store the movie in the array.

            for (int i = 0; i < num; i++) 
            {
                Console.WriteLine($"\nMovie num {i + 1}");
                
                string title = GetStr("Enter Title: ");
                int year = GetInt("Enter Year: ");
                Genre genre = GetGenre();

                movies[i] = new Movie(title, year, genre);
            }

            // print all movies.
            Console.WriteLine("\n----------------------All Movies------------------");
            foreach (Movie movie in movies)
                movie.PrintInfo();  

            // Find and print the oldest and newest movie using out parameters.
            FindOldestAndNewest(movies, out Movie oldest, out Movie newest);


            Console.WriteLine("\nOldest Movie");
            oldest.PrintInfo();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Newest Movie");
            newest.PrintInfo();


        }
    }
}