using BiblioTastic.Database;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTastic.Controllers
{
    [ApiController]
    [Route("/book")]
    public class BookController : ControllerBase
    {
        private IConfiguration Configuration;
        public BookController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        public IEnumerable<Models.Book> Get() 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Book bookDB = new Database.Book(connString);
            return bookDB.Read();
        }

        [HttpGet("{id}")]
        public List<Models.Book> Get(int id) 
        {
            //TODO - If it turns out too slow pulling all the additional infos in here, change it so that it's pulled in 
            //separately when the book is clicked on in the front end. 
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Book bookDB = new Database.Book(connString);
            var books = bookDB.Select(id);
            
            Database.AdditionalInfo additionalInfoDB = new Database.AdditionalInfo(connString);
            //Book is resource type 1, article is resource type 2. Potential for a lookup table, but not worth it yet.
            var additionalInfoList = additionalInfoDB.Select(1);

            foreach (var book in books) 
            {
                foreach (var additionalInfo in additionalInfoList)
                {
                    if (book.BookID == additionalInfo.ResourceID) 
                    {
                        book.AdditionalInfo = additionalInfo;
                        continue;
                    }
                }
            }
            return books;
        }

        [HttpPost]
        public int Post(Models.Book book) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Book bookDB = new Database.Book(connString);
            Database.AdditionalInfo additionalInfoDB = new Database.AdditionalInfo(connString);

            //TODO - save book first, then pass the resourceID to the additionalInfo object. 
            book = SanitiseInputs(book);

            if (book.BookID > 0) 
            {
                bookDB.Update(book);
                additionalInfoDB.Update(book.AdditionalInfo);

            }
            else 
            {

                bookDB.Create(book);
                book.AdditionalInfo.ResourceID = book.BookID;
                additionalInfoDB.Create(book.AdditionalInfo);
            }
            return book.BookID; 
        }

        [HttpDelete]
        public bool Delete(int id) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Book bookDB = new Database.Book(connString);
            return bookDB.Delete(id);
        }

        private Models.Book SanitiseInputs(Models.Book book) 
        {
            book.Title = Utils.SanitiseString(book.Title);
            book.Author = Utils.SanitiseString(book.Author);
            //TODO - Keep an eye on this one. Might strip out chars and prevent the URL from working.
            book.URL = Utils.SanitiseString(book.URL);
            book.URL = Utils.FortmatURL(book.URL);
            book.PublicationLocation = Utils.SanitiseString(book.PublicationLocation);
            book.Publisher = Utils.SanitiseString(book.Publisher);
            book.AdditionalInfo.Summary = Utils.SanitiseString(book.AdditionalInfo.Summary);
            book.AdditionalInfo.KeyWords = Utils.SanitiseString(book.AdditionalInfo.KeyWords);
            //Not nessary since it's passed by reference, but better to be explicit.
            return book;
        }
    }
}