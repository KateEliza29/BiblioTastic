namespace BiblioTasticTests.Database
{
    public class BookTests
    {
        //TODO - Add tests for create + update for invalid fields to check function on failure. 

        #region Setup and Teardown
        public BiblioTastic.Database.Book GenerateDB()
        {
            return new BiblioTastic.Database.Book("connstring");
        }
        
        public BiblioTastic.Models.Book GenerateModel()
        {
            return new BiblioTastic.Models.Book() {LibraryID = 1, Title = "Test Title", Author = "Test Author", PublicationYear = 2002, URL = "testURL.com", PublicationLocation = "London", Publisher = "Test Publisher"};
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
            var books = db.Read();

            //Assert
            Assert.That(books.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void Select_ValidID_ReturnsResult()
        {
            //Arrange
            var db = GenerateDB();

            //Act
            var books = db.Select(1);

            //Assert
            Assert.That(books.Count, Is.Not.EqualTo(0));
        }

        #endregion

        #region Create Tests
        [Test]
        public void Create_ValidFields_ReturnsResults()
        {
            //Arrange - generate the model to be saved. 
            var db = GenerateDB();
            var book = GenerateModel();

            //Act - Create it.
            var created = db.Create(book);

            //Assert - Retrieve the saved model and check the properties match. 
            var retrievedBooks = db.Select(book.LibraryID);
            var matchingBook = retrievedBooks.First(x => x.Title == book.Title);
            Assert.That(book.ToString(), Is.EqualTo(matchingBook.ToString()));

            //Sweep and mop.
            DeleteTestModel(book.BookID);
        }

        #endregion

        #region Update Tests 
        [Test]
        public void Update_ValidFields_ReturnsResults()
        {
            //Arrange - Create a new model in the db to work with.
            var db = GenerateDB();
            var book = GenerateModel();
            var created = db.Create(book);

            //Act - Make changes to the model and call 'Update'.
            book.LibraryID = 2;
            book.Title = "Changed Title";
            book.Author = "Changed Author";
            book.PublicationYear = 2005;
            book.URL = "changedURL.com";
            book.PublicationLocation = "Changed London";
            book.Publisher = "Changed Publisher";
            db.Update(book);

            //Assert - Retrieve the saved model and check the properties match. 
            var retrievedBooks = db.Select(book.LibraryID);
            var matchingBook = retrievedBooks.First(x => x.Title == book.Title);
            Assert.That(book.ToString(), Is.EqualTo(matchingBook.ToString()));

            //Hoover the cobwebs.
            DeleteTestModel(book.BookID);
        }

        #endregion
    }
}
