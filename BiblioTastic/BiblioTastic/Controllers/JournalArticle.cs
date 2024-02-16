using BiblioTastic.Database;
using Microsoft.AspNetCore.Mvc;

namespace BiblioTastic.Controllers
{
    [ApiController]
    [Route("/journalArticle")]
    public class JournalArticleController : ControllerBase
    {
        private IConfiguration Configuration;
        public JournalArticleController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        [HttpGet]
        public IEnumerable<Models.JournalArticle> Get() 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.JournalArticle journalArticleDB = new Database.JournalArticle(connString);
            return journalArticleDB.Read();
        }

        [HttpGet("{id}")]
        public List<Models.JournalArticle> Get(int id) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.JournalArticle journalArticleDB = new Database.JournalArticle(connString);
            var journalArticles = journalArticleDB.Select(id);

            Database.AdditionalInfo additionalInfoDB = new Database.AdditionalInfo(connString);
            //Book is resource type 1, article is resource type 2. Potential for a lookup table, but not worth it yet.
            var additionalInfoList = additionalInfoDB.Select(2);

            foreach (var journalArticle in journalArticles) 
            {
                foreach (var additionalInfo in additionalInfoList)
                {
                    if (journalArticle.JournalArticleID == additionalInfo.ResourceID) 
                    {
                        journalArticle.AdditionalInfo = additionalInfo;
                        continue;
                    }
                }
            }
            return journalArticles;
        }

        [HttpPost]
        public int Post(Models.JournalArticle journalArticle) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.JournalArticle journalArticleDB = new Database.JournalArticle(connString);
            Database.AdditionalInfo additionalInfoDB = new Database.AdditionalInfo(connString);

            journalArticle = SanitiseInputs(journalArticle);

            if (journalArticle.JournalArticleID > 0) 
            {
                journalArticleDB.Update(journalArticle); 
                additionalInfoDB.Update(journalArticle.AdditionalInfo);
            }
            else {
                journalArticleDB.Create(journalArticle);
                journalArticle.AdditionalInfo.ResourceID = journalArticle.JournalArticleID;
                additionalInfoDB.Create(journalArticle.AdditionalInfo);
            }
            return journalArticle.JournalArticleID; 
        }

        [HttpDelete]
        public bool Delete(int id) 
        {
            string connString = this.Configuration.GetConnectionString("AzureDB");
            Database.JournalArticle journalArticleDB = new Database.JournalArticle(connString);
            return journalArticleDB.Delete(id);
        }

        private Models.JournalArticle SanitiseInputs(Models.JournalArticle journalArticle) 
        {
            journalArticle.JournalTitle = Utils.SanitiseString(journalArticle.JournalTitle);
            journalArticle.ArticleTitle = Utils.SanitiseString(journalArticle.ArticleTitle);
            journalArticle.Author = Utils.SanitiseString(journalArticle.Author);
            journalArticle.VolumeNumber = Utils.SanitiseString(journalArticle.VolumeNumber);
            journalArticle.IssueNumber = Utils.SanitiseString(journalArticle.IssueNumber);
            journalArticle.PageReference = Utils.SanitiseString(journalArticle.PageReference);
            //TODO - Keep an eye on this one. Might strip out chars and prevent the URL from working.
            journalArticle.URL = Utils.SanitiseString(journalArticle.URL);
            journalArticle.URL = Utils.FortmatURL(journalArticle.URL);
            journalArticle.AdditionalInfo.Summary = Utils.SanitiseString(journalArticle.AdditionalInfo.Summary);
            journalArticle.AdditionalInfo.KeyWords = Utils.SanitiseString(journalArticle.AdditionalInfo.KeyWords);
            //Not nessary since it's passed by reference, but better to be explicit.
            return journalArticle;
        }
    }
}