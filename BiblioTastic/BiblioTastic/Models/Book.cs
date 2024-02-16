namespace BiblioTastic.Models 
{
    public class Book 
    {
        public int BookID { get; set; }
        public int LibraryID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string URL { get; set; }
        public string PublicationLocation { get; set; }
        public string Publisher { get; set; }
        public DateTime DateAdded { get; set; }
        public AdditionalInfo AdditionalInfo { get; set; }
        
        public string HarvardReference 
        {
            get 
            {
                //First is no url meaning it's a real book. Second is ebook with no publication details. 
                if (String.IsNullOrEmpty(URL))
                    return $"{Author} ({PublicationYear}) {Title}. {PublicationLocation}: {Publisher}.";
                else 
                    return $"{Author} ({PublicationYear}) {Title}. Available at: {URL} (Accessed: {DateAdded.ToString("d MMM yyyy")}).";
            } 
        }

        //For tests.
        public override string ToString() 
        {
            return $"BookID: {BookID}, LibraryID: {LibraryID}, Title: {Title}, Author: {Author}, PublicationYear: {PublicationYear}, URL: {URL}, PublicationLocation: {PublicationLocation}, Publisher: {Publisher}";
        }
    }
}