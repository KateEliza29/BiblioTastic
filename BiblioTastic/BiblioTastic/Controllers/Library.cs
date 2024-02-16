using BiblioTastic.Database;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTastic.Controllers
{
    [ApiController]
    [Route("/library")]
    public class LibraryController : ControllerBase
    {
        private IConfiguration Configuration;
        public LibraryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        public IEnumerable<Models.Library> Get() 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Library libraryDB = new Database.Library(connString);
            return libraryDB.Read();
        }

        [HttpGet("{id}")]
        public Models.Library Get(int id) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Library libraryDB = new Database.Library(connString);
            return libraryDB.Select(id);
        }

        [HttpPost]
        public int Post(Models.Library library) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Library libraryDB = new Database.Library(connString);

            library = SanitiseInputs(library);

            if (library.LibraryID > 0) 
            {
                libraryDB.Update(library); 
            }
            else 
            {
                libraryDB.Create(library);
            }
            return library.LibraryID; 
        }

        [HttpDelete]
        public bool Delete(int id) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.Library libraryDB = new Database.Library(connString);
            return libraryDB.Delete(id);
        }

        private Models.Library SanitiseInputs(Models.Library library) 
        {
            library.LibraryName = Utils.SanitiseString(library.LibraryName);
            library.LibraryDescription = Utils.SanitiseString(library.LibraryDescription);
            //Not nessary since it's passed by reference, but better to be explicit.
            return library;
        }
    }
}