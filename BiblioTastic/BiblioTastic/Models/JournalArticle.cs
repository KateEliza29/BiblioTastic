namespace BiblioTastic.Models 
{
    public class JournalArticle
    {
        public int JournalArticleID { get; set; }
        public int LibraryID { get; set; }
        public string JournalTitle { get; set; }
        public string ArticleTitle { get; set; }
        public string Author { get; set; }
        //This + IssueNumber are string because of the variability in articles. Some are 'SPR01' etc, not just plain ints. 
        public string VolumeNumber { get; set; }
        public string IssueNumber { get; set; }
        public int PublicationYear { get; set; }
        //Arguable whether this should be changed to start page and finish page. Data integrity + validation would be better... 
        public string PageReference { get; set; }
        public string URL { get; set; }
        public DateTime DateAdded { get; set; }
        public AdditionalInfo AdditionalInfo { get; set; }

        public string HarvardReference 
        {
            get 
            {
                //First is no url, meaning they've read an actual physical journal article. Crazy kid. Second is an electronic version.
                if (String.IsNullOrEmpty(URL))
                    return $"{Author} ({PublicationYear}) '{ArticleTitle}', {JournalTitle}, {VolumeNumber} ({IssueNumber}), {PageReference}.";
                else 
                    return $"{Author} ({PublicationYear}) '{ArticleTitle}', {JournalTitle}, {VolumeNumber} ({IssueNumber}), {PageReference}. Available at: {URL} (Accessed: {DateAdded.ToString("d MMM yyyy")}).";
            } 
        }

        //For tests.
        public override string ToString() 
        {
            return $"JournalArticleID: {JournalArticleID}, LibraryID: {LibraryID}, JournalTitle: {JournalTitle}, ArticleTitle: {ArticleTitle}, Author: {Author}, URL: {URL}, VolumeNumber: {VolumeNumber}, IssueNumber : {IssueNumber}, PageReference: {PageReference}, ";
        }
    }
}