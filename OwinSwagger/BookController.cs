using System.Web.Http;
using System.Web.Http.Description;

namespace OwinSwagger
{
    public class BookController : ApiController
    {
        ILibrary _library;

        public BookController( ILibrary library )
        {
            _library = library;
        }

        /// <summary>
        /// Retrieves a book with the specified Id.
        /// </summary>
        /// <param name="id">The id of the book to retrieve</param>
        [ResponseType( typeof( Book ) )]
        public IHttpActionResult Get( string id )
        {
            //a real controller would need to take the id and look up the book in the database
            //let's hack it below to always return a single book
            var book = _library.GetBook();
            book.Href = Request.RequestUri.ToString();
            return Ok( book );
        }
    }
}
