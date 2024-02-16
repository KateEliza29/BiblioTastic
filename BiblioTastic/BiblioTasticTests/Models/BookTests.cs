using BiblioTastic.Models;

namespace BiblioTasticTests.Models
{
    public class BookTests
    {
        [Test]
        public void HarvardReference_EBook_ReturnsExpectedReference()
        {
            //Arrange
            var model = new Book() 
            {
                BookID = 1,
                LibraryID = 1,
                Title = "Book Title",
                Author = "Lastname, F.",
                PublicationYear = 2020,
                URL = "www.thisisaurl.com",
                PublicationLocation = "",
                Publisher = "",
                DateAdded = new DateTime(2020, 01, 29)
            };

            var expectedReference = "Lastname, F. (2020) Book Title. Available at: www.thisisaurl.com (Accessed: 29 Jan 2020).";

            //Assert
            Assert.That(model.HarvardReference, Is.EqualTo(expectedReference));
        }

        [Test]
        public void HarvardReference_Book_ReturnsExpectedReference()
        {
            //Arrange
            var model = new Book() 
            {
                BookID = 1,
                LibraryID = 1,
                Title = "Book Title",
                Author = "Lastname, F.",
                PublicationYear = 2020,
                URL = "",
                PublicationLocation = "London",
                Publisher = "Penguin",
                DateAdded = new DateTime(2020, 01, 29)
            };

            var expectedReference = "Lastname, F. (2020) Book Title. London: Penguin.";

            //Assert
            Assert.That(model.HarvardReference, Is.EqualTo(expectedReference));
        }
    }
}