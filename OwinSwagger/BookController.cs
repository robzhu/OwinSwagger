using System.Web.Http;
using System.Web.Http.Description;

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

    public class BookController : ApiController
    {
        /// <summary>
        /// Retrieves a book with the specified Id.
        /// </summary>
        /// <param name="id">The id of the book to retrieve</param>
        [ResponseType( typeof( Book ) )]
        public IHttpActionResult Get( string id )
        {
            //a real controller would need to take the id and look up the book in the database
            //let's hack it below to always return a single book
            var book = new Book
            {
                Author = "Stephen King",
                Title = "Hearts in Atlantis",
                Href = Request.RequestUri.ToString(),
            };
            return Ok( book );
        }
    }
}
