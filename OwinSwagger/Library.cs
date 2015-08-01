using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSwagger
{
    /// <summary>
    /// Resource representing a book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The self-link
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// The name of the author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        public string Title { get; set; }
    }

    public interface ILibrary
    {
        Book GetBook();
    }

    public class Library : ILibrary
    {
        public Book GetBook()
        {
            return new Book
            {
                Author = "Stephen King",
                Title = "Hearts in Atlantis",
            };
        }
    }
}
