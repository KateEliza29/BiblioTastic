namespace BiblioTastic.Models 
{
    public class Library 
    {
        public int LibraryID { get; set; }
        public string LibraryName { get; set; }
        public string LibraryDescription { get; set; }

        //For tests.
        public override string ToString() 
        {
            return $"LibraryID: {LibraryID}, LibraryName: {LibraryName}, LibraryDescription: {LibraryDescription}";
        }
    }
}