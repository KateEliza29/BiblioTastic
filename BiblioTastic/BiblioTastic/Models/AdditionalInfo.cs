namespace BiblioTastic.Models 
{
    public class AdditionalInfo
    {
        public int AdditionalInfoID { get; set; }
        public int ResourceType { get; set; }
        public int ResourceID { get; set; }
        public string Summary { get; set; }
        public string KeyWords { get; set; }
        public int Rating { get; set; }
    }
}