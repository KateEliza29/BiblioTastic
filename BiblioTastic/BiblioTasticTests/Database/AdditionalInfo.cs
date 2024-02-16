namespace BiblioTasticTests.Database
{
    public class AdditionalInfoTests
    {
        //TODO - Add tests for create + update for invalid fields to check function on failure. 

        #region Setup and Teardown
        public BiblioTastic.Database.AdditionalInfo GenerateDB()
        {
            return new BiblioTastic.Database.AdditionalInfo("connstring");
        }
        
        public BiblioTastic.Models.AdditionalInfo GenerateModel()
        {
            return new BiblioTastic.Models.AdditionalInfo() 
            {
                AdditionalInfoID = 0,
                ResourceType = 1,
                ResourceID = 1,
                Summary = "This is a summary of a book",
                KeyWords = "keyword1, keyword2, keyword3",
                Rating = 4
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
            var additionalInfoList = db.Read();

            //Assert
            Assert.That(additionalInfoList.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void Select_ValidID_ReturnsResult()
        {
            //Arrange
            var db = GenerateDB();

            //Act - select by resource type
            var additionalInfoList = db.Select(1);

            //Assert
            Assert.That(additionalInfoList.Count, Is.Not.EqualTo(0));
        }

        #endregion

        #region Create Tests
        [Test]
        public void Create_ValidFields_ReturnsResults()
        {
            //Arrange - generate the model to be saved. 
            var db = GenerateDB();
            var additionalInfo = GenerateModel();

            //Act - Create it.
            var created = db.Create(additionalInfo);

            //Assert - Retrieve the saved model and check the properties match. 
            var retrievedAdditionalInfoList = db.Select(additionalInfo.ResourceType);
            var matchingAdditionalInfo = retrievedAdditionalInfoList.First(x => x.Summary == additionalInfo.Summary);
            Assert.That(additionalInfo.ToString(), Is.EqualTo(matchingAdditionalInfo.ToString()));

            //Sweep and mop.
            DeleteTestModel(additionalInfo.AdditionalInfoID);
        }

        #endregion

        #region Update Tests 
        [Test]
        public void Update_ValidFields_ReturnsResults()
        {
            //Arrange - Create a new model in the db to work with.
            var db = GenerateDB();
            var additionalInfo = GenerateModel();
            var created = db.Create(additionalInfo);

            //Act - Make changes to the model and call 'Update'.
            additionalInfo.ResourceID = 2;
            additionalInfo.Summary = "This is a changed summary of a book";
            additionalInfo.KeyWords = "keyword4, keyword5, keyword6";
            additionalInfo.Rating = 3;
            db.Update(additionalInfo);

            //Assert - Retrieve the saved model and check the properties match. 
            var retrievedAdditionalInfoList = db.Select(additionalInfo.ResourceType);
            var matchingAdditionalInfo = retrievedAdditionalInfoList.First(x => x.Summary == additionalInfo.Summary);
            Assert.That(additionalInfo.ToString(), Is.EqualTo(matchingAdditionalInfo.ToString()));

            //Hoover the cobwebs.
            DeleteTestModel(additionalInfo.AdditionalInfoID);
        }

        #endregion
    }
}
