using System.Text.RegularExpressions;

namespace BiblioTastic.Database 
{
    public static class Utils
    {
        public static string SanitiseString(string stringToSanitise) 
        {
            return Regex.Replace(stringToSanitise, @"[?&^$#@!()+,;<>'_*]", "");
        }

        public static string FortmatURL(string urlToFormat) {
            //Without the https bit, the Angular anchors don't work. Check for it, if it's not there, add it.
            if (!urlToFormat.StartsWith("http"))
                urlToFormat = $"https://{urlToFormat}";
            return urlToFormat;
        }
    }
}