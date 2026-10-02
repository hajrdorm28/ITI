using System;

namespace LibraryApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Isbn { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}
