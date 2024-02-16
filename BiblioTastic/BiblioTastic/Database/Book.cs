using System.Data;
using System.Data.SqlClient;
using BiblioTastic.Interfaces;

namespace BiblioTastic.Database
{
    public class Book : ICRUD<Models.Book>
    {
        private SqlConnection _conn;

        public Book(string connString)
        {
            _conn = new SqlConnection(connString);
        }

        public bool Create(Models.Book book)
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "BookCreate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", book.LibraryID);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                cmd.Parameters.AddWithValue("@URL", book.URL);
                cmd.Parameters.AddWithValue("@PublicationLocation", book.PublicationLocation);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                SqlParameter bookID = new SqlParameter("@BookID", SqlDbType.Int)
                { 
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(bookID);
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0) 
                {
                    saved = true;
                    book.BookID = (int)bookID.Value;
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

        public List<Models.Book> Read() 
        {
            var books = new List<Models.Book>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "BookRead";  
                cmd.CommandType = CommandType.StoredProcedure;  
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new Models.Book();
                        book.BookID = Convert.ToInt32(reader["BookID"]);
                        book.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        book.Title = Convert.ToString(reader["Title"]);
                        book.Author = Convert.ToString(reader["Author"]);
                        book.PublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                        book.URL = Convert.ToString(reader["URL"]);
                        book.PublicationLocation = Convert.ToString(reader["PublicationLocation"]);
                        book.Publisher = Convert.ToString(reader["Publisher"]);
                        book.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
                        books.Add(book);
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
            books = books.OrderBy(x => x.Title).ToList();
            return books;
        }

        public List<Models.Book> Select(int libraryID) 
        {
            var books = new List<Models.Book>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "BookSelect";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", libraryID);
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new Models.Book();
                        book.BookID = Convert.ToInt32(reader["BookID"]);
                        book.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        book.Title = Convert.ToString(reader["Title"]);
                        book.Author = Convert.ToString(reader["Author"]);
                        book.PublicationYear = Convert.ToInt32(reader["PublicationYear"]);
                        book.URL = Convert.ToString(reader["URL"]);
                        book.PublicationLocation = Convert.ToString(reader["PublicationLocation"]);
                        book.Publisher = Convert.ToString(reader["Publisher"]);
                        book.DateAdded = Convert.ToDateTime(reader["DateAdded"]);
                        books.Add(book);
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
            books = books.OrderBy(x => x.Title).ToList();
            return books;
        }

        public bool Update(Models.Book book) 
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "BookUpdate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@BookID", book.BookID);
                cmd.Parameters.AddWithValue("@LibraryID", book.LibraryID);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                cmd.Parameters.AddWithValue("@URL", book.URL);
                cmd.Parameters.AddWithValue("@PublicationLocation", book.PublicationLocation);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
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
                cmd.CommandText = "BookDelete";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookID", bookID);  
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

