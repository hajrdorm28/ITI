using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_4
{
    struct Movie
    {
        public string Title;
        public int Year;
        public Genre Genre;

        public Movie(string title, int year, Genre genre)
        {
            Title = title;
            Year = year;
            Genre = genre;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Title: {Title}\n" +
                $"Year: {Year}\n" +
                $"Genre: {Genre}\n");
        }
    }
}
