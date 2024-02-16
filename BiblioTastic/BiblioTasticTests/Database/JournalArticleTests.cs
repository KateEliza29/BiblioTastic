namespace BiblioTasticTests.Database
{
    public class JournalArticleTests
    {
        //TODO - Add tests for create + update for invalid fields to check function on failure. 

        #region Setup and Teardown
        public BiblioTastic.Database.JournalArticle GenerateDB()
        {
            return new BiblioTastic.Database.JournalArticle("connstring");
        }
        
        public BiblioTastic.Models.JournalArticle GenerateModel()
        {
            return new BiblioTastic.Models.JournalArticle() 
            {
                JournalArticleID = 0,
                LibraryID = 1,
                JournalTitle = "Test Journal Title",
                ArticleTitle = "Test Article Title",
                Author = "Test Author",
                VolumeNumber = "Vol1",
                IssueNumber = "Iss1",
                PageReference = "pp.1-10",
                URL = "www.testurl.com"
            };
        }

        public void DeleteTestModel(int modelID) 
        {
            var db = GenerateDB();
            db.Delete(modelID);
        }
        #endregion

        #region Read Tests
        [Test]
        public void Read_Called_ReturnsResults()
        {
            //Arrange
            var db = GenerateDB();

            //Act 
            var journalArticles = db.Read();

            //Assert
            Assert.That(journalArticles.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void Select_ValidID_ReturnsResult()
        {
            //Arrange - add one to the db for that library. 
            var db = GenerateDB();
            var model = GenerateModel();
            db.Create(model);

            //Act
            var journalArticle = db.Select(1);

            //Assert
            Assert.That(journalArticle.Count, Is.Not.EqualTo(0));

            //Wipe the windows
            DeleteTestModel(model.JournalArticleID);
        }

        #endregion

        #region Create Tests
        [Test]
        public void Create_ValidFields_ReturnsResults()
        {
            //Arrange - generate the model to be saved. 
            var db = GenerateDB();
            var journalArticle = GenerateModel();

            //Act - Create it.
            var created = db.Create(journalArticle);

            //Assert - Retrieve the saved model and check the properties match.
            var retrievedJournalArticles = db.Select(journalArticle.LibraryID);
            var matchingJournalArticle = retrievedJournalArticles.First(x => x.ArticleTitle == journalArticle.ArticleTitle);
            Assert.That(journalArticle.ToString(), Is.EqualTo(matchingJournalArticle.ToString()));

            //Sweep and mop.
            DeleteTestModel(journalArticle.JournalArticleID);
        }

        #endregion

        #region Update Tests 
        [Test]
        public void Update_ValidFields_ReturnsResults()
        {
            //Arrange - Create a new model in the db to work with.
            var db = GenerateDB();
            var journalArticle = GenerateModel();
            var created = db.Create(journalArticle);

            //Act - Make changes to the model and call 'Update'.
            journalArticle.LibraryID = 2;
            journalArticle.JournalTitle = "Changed journal article";
            journalArticle.ArticleTitle = "Changed article title";
            journalArticle.Author = "Changed author";
            journalArticle.VolumeNumber = "Vol2";
            journalArticle.IssueNumber = "Iss2";
            journalArticle.PageReference = "pp.2-11";
            journalArticle.URL = "www.changedurl.com";
            db.Update(journalArticle);

            //Assert - Retrieve the saved model and check the properties match.
            var retrievedJournalArticles = db.Select(journalArticle.LibraryID);
            var matchingJournalArticle = retrievedJournalArticles.First(x => x.ArticleTitle == journalArticle.ArticleTitle);
            Assert.That(journalArticle.ToString(), Is.EqualTo(matchingJournalArticle.ToString()));

            //Hoover the cobwebs.
            DeleteTestModel(journalArticle.JournalArticleID);
        }

        #endregion
    }
}
