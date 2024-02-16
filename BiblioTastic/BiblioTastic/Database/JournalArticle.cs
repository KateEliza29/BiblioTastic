using System.Data;
using System.Data.SqlClient;
using BiblioTastic.Interfaces;

namespace BiblioTastic.Database 
{
    public class JournalArticle : ICRUD<Models.JournalArticle>
    {
        private SqlConnection _conn;

        public JournalArticle(string connString)
        {
            _conn = new SqlConnection(connString);
        }

        public bool Create(Models.JournalArticle journalArticle)
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "JournalArticleCreate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", journalArticle.LibraryID);
                cmd.Parameters.AddWithValue("@JournalTitle", journalArticle.JournalTitle);
                cmd.Parameters.AddWithValue("@ArticleTitle", journalArticle.ArticleTitle);
                cmd.Parameters.AddWithValue("@Author", journalArticle.Author);
                cmd.Parameters.AddWithValue("@VolumeNumber", journalArticle.VolumeNumber);
                cmd.Parameters.AddWithValue("@IssueNumber", journalArticle.IssueNumber);
                cmd.Parameters.AddWithValue("@PageReference", journalArticle.PageReference);
                cmd.Parameters.AddWithValue("@URL", journalArticle.URL);
                cmd.Parameters.AddWithValue("@PublicationYear", journalArticle.PublicationYear);
                SqlParameter journalArticleID = new SqlParameter("@JournalArticleID", SqlDbType.Int)
                { 
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(journalArticleID);
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0) 
                {
                    saved = true;
                    journalArticle.JournalArticleID = (int)journalArticleID.Value;
                }
                cmd.Dispose(); 
                _conn.Close(); 
            }  
            catch (Exception ex)  
            {  
                _conn.Close(); 
                //Do nothing. The return value is already false. 
            }  
            return saved;
        }

        public List<Models.JournalArticle> Read() 
        {
            var journalArticles = new List<Models.JournalArticle>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "JournalArticleRead";  
                cmd.CommandType = CommandType.StoredProcedure;  
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var journalArticle = new Models.JournalArticle();
                        journalArticle.JournalArticleID = Convert.ToInt32(reader["JournalArticleID"]);
                        journalArticle.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        journalArticle.JournalTitle = Convert.ToString(reader["JournalTitle"]);
                        journalArticle.ArticleTitle = Convert.ToString(reader["ArticleTitle"]);
                        journalArticle.Author = Convert.ToString(reader["Author"]);
                        journalArticle.VolumeNumber = Convert.ToString(reader["VolumeNumber"]);
                        journalArticle.IssueNumber = Convert.ToString(reader["IssueNumber"]);
                        journalArticle.PageReference = Convert.ToString(reader["PageReference"]);
                        journalArticle.URL = Convert.ToString(reader["URL"]);
                        journalArticle.PublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                        journalArticle.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
                        journalArticles.Add(journalArticle);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                cmd.Dispose(); 
                _conn.Close();
            }
            
            catch 
            {
                _conn.Close(); 
            }
            journalArticles = journalArticles.OrderBy(x => x.ArticleTitle).ToList();
            return journalArticles;
        }

        public List<Models.JournalArticle> Select(int libraryID) 
        {
            var journalArticles = new List<Models.JournalArticle>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "JournalArticleSelect";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", libraryID);
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var journalArticle = new Models.JournalArticle();
                        journalArticle.JournalArticleID = Convert.ToInt32(reader["JournalArticleID"]);
                        journalArticle.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        journalArticle.JournalTitle = Convert.ToString(reader["JournalTitle"]);
                        journalArticle.ArticleTitle = Convert.ToString(reader["ArticleTitle"]);
                        journalArticle.Author = Convert.ToString(reader["Author"]);
                        journalArticle.VolumeNumber = Convert.ToString(reader["VolumeNumber"]);
                        journalArticle.IssueNumber = Convert.ToString(reader["IssueNumber"]);
                        journalArticle.PageReference = Convert.ToString(reader["PageReference"]);
                        journalArticle.URL = Convert.ToString(reader["URL"]);
                        journalArticle.PublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                        journalArticle.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
                        journalArticles.Add(journalArticle);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                cmd.Dispose(); 
                _conn.Close();
            }
            catch 
            {
               _conn.Close(); 
            }
            journalArticles = journalArticles.OrderBy(x => x.ArticleTitle).ToList();
            return journalArticles;
        }

        public bool Update(Models.JournalArticle journalArticle) 
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "JournalArticleUpdate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@JournalArticleID", journalArticle.JournalArticleID); 
                cmd.Parameters.AddWithValue("@LibraryID", journalArticle.LibraryID);
                cmd.Parameters.AddWithValue("@JournalTitle", journalArticle.JournalTitle);
                cmd.Parameters.AddWithValue("@ArticleTitle", journalArticle.ArticleTitle);
                cmd.Parameters.AddWithValue("@Author", journalArticle.Author);
                cmd.Parameters.AddWithValue("@VolumeNumber", journalArticle.VolumeNumber);
                cmd.Parameters.AddWithValue("@IssueNumber", journalArticle.IssueNumber);
                cmd.Parameters.AddWithValue("@PageReference", journalArticle.PageReference);
                cmd.Parameters.AddWithValue("@URL", journalArticle.URL);
                cmd.Parameters.AddWithValue("@PublicationYear", journalArticle.PublicationYear);
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0) 
                    saved = true;
                cmd.Dispose(); 
                _conn.Close(); 
            }  
            catch (Exception ex)  
            {  
                _conn.Close(); 
            }  
            return saved;
        }

        public bool Delete(int bookID) 
        {
            var deleted = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "JournalArticleDelete";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@JournalArticleID", bookID);  
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0)
                    deleted = true;
                    
                cmd.Dispose(); 
                _conn.Close();  
            }  
            catch (Exception ex)  
            {  
                _conn.Close();  
            }  
            return deleted;
        }
    }
}