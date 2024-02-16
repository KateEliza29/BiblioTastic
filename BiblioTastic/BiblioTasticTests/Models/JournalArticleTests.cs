using BiblioTastic.Models;

namespace BiblioTasticTests.Models
{
    public class JournalArticleTests
    {
        [Test]
        public void HarvardReference_EJournal_ReturnsExpectedReference()
        {
            //Arrange
            var model = new JournalArticle() 
            {
                JournalArticleID = 1,
                LibraryID = 1,
                JournalTitle = "Journal Title",
                ArticleTitle = "Article Title",
                Author = "Lastname, F.",
                VolumeNumber = "vol 1",
                IssueNumber = "issue 2",
                PublicationYear = 2010,
                PageReference = "pp.1-10",
                URL = "www.thisisaurl.com",
                DateAdded = new DateTime(2020, 01, 29)
            };

            var expectedReference = "Lastname, F. (2010) 'Article Title', Journal Title, vol 1 (issue 2), pp.1-10. Available at: www.thisisaurl.com (Accessed: 29 Jan 2020).";

            //Assert
            Assert.That(model.HarvardReference, Is.EqualTo(expectedReference));
        }

        [Test]
        public void HarvardReference_Journal_ReturnsExpectedReference()
        {
            //Arrange
            var model = new JournalArticle() 
            {
                JournalArticleID = 1,
                LibraryID = 1,
                JournalTitle = "Journal Title",
                ArticleTitle = "Article Title",
                Author = "Lastname, F.",
                VolumeNumber = "vol 1",
                IssueNumber = "issue 2",
                PublicationYear = 2010,
                PageReference = "pp.1-10",
                URL = "",
                DateAdded = new DateTime(2020, 01, 29)
            };

            var expectedReference = "Lastname, F. (2010) 'Article Title', Journal Title, vol 1 (issue 2), pp.1-10.";

            //Assert
            Assert.That(model.HarvardReference, Is.EqualTo(expectedReference));
        }
    }
}