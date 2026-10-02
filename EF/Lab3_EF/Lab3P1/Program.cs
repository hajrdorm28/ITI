using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var context = new LibraryContext();
            context.Database.EnsureCreated(); //to make sure it doesn't exist

            SeedData(context);
            QueryAndPrint(context);
        }

        private static void SeedData(LibraryContext context)
        {
            if (context.Authors.Any(a => a.LastName == "Martin"))
            {
                Console.WriteLine("Sample data already exists, skipping seed.");
                return;
            }
            var author = new Author
            {
                FirstName = "Robert",
                LastName = "Martin",
                Bio = "Software engineer and author, known for Clean Code.",
                Books = new List<Book>
                {
                    new Book { Title = "Clean Code", Isbn = "9780132350884", PublishedDate = new DateTime(2008, 8, 1) },
                    new Book { Title = "The Clean Coder", Isbn = "9780137081073", PublishedDate = new DateTime(2011, 5, 13) },
                    new Book { Title = "Clean Architecture", Isbn = "9780134494166", PublishedDate = new DateTime(2017, 9, 20) }
                }
            };

            context.Authors.Add(author);
            context.SaveChanges();

            Console.WriteLine("Seeded 1 author with 3 books.");
        }

        private static void QueryAndPrint(LibraryContext context)
        {
            var authorsWithBooks = context.Authors
                .Include(a => a.Books)
                .OrderBy(a => a.LastName)
                .ToList();

            foreach (var author in authorsWithBooks)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
                foreach (var book in author.Books.OrderBy(b => b.PublishedDate))
                {
                    Console.WriteLine($"   - {book.Title} ({book.PublishedDate:yyyy}) [ISBN: {book.Isbn}]");
                }
            }
            var bookToUpdate = context.Books.FirstOrDefault(b => b.Title == "Clean Code");
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = "Clean Code (2nd Printing)";
                context.SaveChanges();
                Console.WriteLine("\nUpdated a book title.");
            }
            var bookToDelete = context.Books.FirstOrDefault(b => b.Title == "The Clean Coder");
            if (bookToDelete != null)
            {
                context.Books.Remove(bookToDelete);
                context.SaveChanges();
                Console.WriteLine("Deleted a book.");
            }

            var bookCounts = context.Authors
                .Select(a => new
                {
                    AuthorName = a.FirstName + " " + a.LastName,
                    BookCount = a.Books.Count
                })
                .ToList();

            Console.WriteLine("Book counts per author: ");
            foreach (var item in bookCounts)
            {
                Console.WriteLine($"{item.AuthorName}: {item.BookCount} book(s)");
            }
        }
    }
}
