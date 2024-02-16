using System.Data;
using System.Data.SqlClient;

namespace BiblioTastic.Database 
{
    public class Library
    {
        private SqlConnection _conn;

        public Library(string connString)
        {
            _conn = new SqlConnection(connString);
        }

        public bool Create(Models.Library library)
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "LibraryCreate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryName", library.LibraryName);
                cmd.Parameters.AddWithValue("@LibraryDescription", library.LibraryDescription);
                SqlParameter libraryID = new SqlParameter("@LibraryID", SqlDbType.Int)
                { 
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(libraryID);
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0) 
                {
                    saved = true;
                    library.LibraryID = (int)libraryID.Value;
                }
                cmd.Dispose(); 
                _conn.Close(); 
            }  
            catch (Exception ex)  
            {  
                _conn.Close(); 
            }  
            return saved;
        }

        public List<Models.Library> Read() 
        {
            var libraries = new List<Models.Library>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "LibraryRead";  
                cmd.CommandType = CommandType.StoredProcedure;  
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var library = new Models.Library();
                        library.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        library.LibraryName = Convert.ToString(reader["LibraryName"]);
                        library.LibraryDescription = Convert.ToString(reader["LibraryDescription"]);
                        libraries.Add(library);
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
            libraries = libraries.OrderBy(x => x.LibraryName).ToList();
            return libraries;
        }

        public Models.Library Select(int libraryID) 
        {
            var library = new Models.Library();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "LibrarySelect";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", libraryID);
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        library.LibraryID = Convert.ToInt32(reader["LibraryID"]);
                        library.LibraryName = Convert.ToString(reader["LibraryName"]);
                        library.LibraryDescription = Convert.ToString(reader["LibraryDescription"]);
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
            return library;
        }

        public bool Update(Models.Library library) 
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "LibraryUpdate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@LibraryID", library.LibraryID);
                cmd.Parameters.AddWithValue("@LibraryName", library.LibraryName);
                cmd.Parameters.AddWithValue("@LibraryDescription", library.LibraryDescription);
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

        public bool Delete(int libraryID) 
        {
            var deleted = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "LibraryDelete";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LibraryID", libraryID);  
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