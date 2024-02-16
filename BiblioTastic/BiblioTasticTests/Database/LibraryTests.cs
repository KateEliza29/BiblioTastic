namespace BiblioTasticTests.Database
{
    public class LibraryTests
    {
        //TODO - Add tests for create + update for invalid fields to check function on failure. 

        #region Setup and Teardown
        public BiblioTastic.Database.Library GenerateDB()
        {
            return new BiblioTastic.Database.Library("connstring");
        }
        
        public BiblioTastic.Models.Library GenerateModel()
        {
            return new BiblioTastic.Models.Library() {LibraryName = "Test Name", LibraryDescription = "Test Description"};
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
            var libraries = db.Read();

            //Assert
            Assert.That(libraries.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void Select_ValidID_ReturnsResult()
        {
            //Arrange
            var db = GenerateDB();

            //Act
            var library = db.Select(1);

            //Assert
            Assert.That(library.LibraryName, Is.Not.EqualTo(""));
        }

        #endregion

        #region Create Tests
        [Test]
        public void Create_ValidFields_ReturnsResults()
        {
            //Arrange - generate the model to be saved. 
            var db = GenerateDB();
            var library = GenerateModel();

            //Act - Create it.
            var created = db.Create(library);

            //Assert - Retrieve the saved model and check the properties match. 
            var retrievedLibrary = db.Select(library.LibraryID);
            Assert.That(library.ToString(), Is.EqualTo(retrievedLibrary.ToString()));

            //Sweep and mop.
            DeleteTestModel(library.LibraryID);
        }

        #endregion

        #region Update Tests 
        [Test]
        public void Update_ValidFields_ReturnsResults()
        {
            //Arrange - Create a new model in the db to work with.
            var db = GenerateDB();
            var library = GenerateModel();
            var created = db.Create(library);

            //Act - Make changes to the model and call 'Update'.
            library.LibraryName = "Changed Name";
            library.LibraryDescription = "Changed Description";
            db.Update(library);

            //Asset - Retrieve the model from the db and check the properties match. 
            var retrievedLibrary = db.Select(library.LibraryID);
            Assert.That(library.ToString(), Is.EqualTo(retrievedLibrary.ToString()));

            //Hoover the cobwebs.
            DeleteTestModel(library.LibraryID);
        }

        #endregion
    }
}
