using System.Data;
using System.Data.SqlClient;
using BiblioTastic.Interfaces;

namespace BiblioTastic.Database 
{
    public class AdditionalInfo : ICRUD<Models.AdditionalInfo>
    {
        private SqlConnection _conn;

        public AdditionalInfo(string connString)
        {
            _conn = new SqlConnection(connString);
        }

        public bool Create(Models.AdditionalInfo additionalInfo)
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "AdditionalInfoCreate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@ResourceType", additionalInfo.ResourceType);
                cmd.Parameters.AddWithValue("@ResourceID", additionalInfo.ResourceID);
                cmd.Parameters.AddWithValue("@Summary", additionalInfo.Summary);
                cmd.Parameters.AddWithValue("@KeyWords", additionalInfo.KeyWords);
                cmd.Parameters.AddWithValue("@Rating", additionalInfo.Rating);
                SqlParameter additionalInfoID = new SqlParameter("@AdditionalInfoID", SqlDbType.Int)
                { 
                    Direction = ParameterDirection.Output 
                };
                cmd.Parameters.Add(additionalInfoID);
                _conn.Open();  

                if (cmd.ExecuteNonQuery() > 0) 
                {
                    saved = true;
                    additionalInfo.AdditionalInfoID = (int)additionalInfoID.Value;
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

        public List<Models.AdditionalInfo> Read() 
        {
            var additionalInfoList = new List<Models.AdditionalInfo>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "AdditionalInfoRead";  
                cmd.CommandType = CommandType.StoredProcedure;  
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var additionalInfo = new Models.AdditionalInfo();
                        additionalInfo.AdditionalInfoID = Convert.ToInt32(reader["AdditionalinfoID"]);
                        additionalInfo.ResourceType = Convert.ToInt32(reader["ResourceType"]);
                        additionalInfo.ResourceID = Convert.ToInt32(reader["ResourceID"]);
                        additionalInfo.Summary = Convert.ToString(reader["Summary"]);
                        additionalInfo.KeyWords = Convert.ToString(reader["KeyWords"]);
                        additionalInfo.Rating = Convert.ToInt32(reader["Rating"]);
                        additionalInfoList.Add(additionalInfo);
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
            return additionalInfoList;
        }

        public List<Models.AdditionalInfo> Select(int resourceType) 
        {
            var additionalInfoList = new List<Models.AdditionalInfo>();
            try 
            {
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "AdditionalInfoSelect";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@ResourceType", resourceType);
                _conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var additionalInfo = new Models.AdditionalInfo();
                        additionalInfo.AdditionalInfoID = Convert.ToInt32(reader["AdditionalinfoID"]);
                        additionalInfo.ResourceType = Convert.ToInt32(reader["ResourceType"]);
                        additionalInfo.ResourceID = Convert.ToInt32(reader["ResourceID"]);
                        additionalInfo.Summary = Convert.ToString(reader["Summary"]);
                        additionalInfo.KeyWords = Convert.ToString(reader["KeyWords"]);
                        additionalInfo.Rating = Convert.ToInt32(reader["Rating"]);
                        additionalInfoList.Add(additionalInfo);
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
            catch (Exception ex)
            {
               _conn.Close(); 
            }
            return additionalInfoList;
        }

        public bool Update(Models.AdditionalInfo additionalInfo) 
        {
            var saved = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "AdditionalInfoUpdate";  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@AdditionalInfoID", additionalInfo.AdditionalInfoID);
                cmd.Parameters.AddWithValue("@ResourceType", additionalInfo.ResourceType);
                cmd.Parameters.AddWithValue("@ResourceID", additionalInfo.ResourceID);
                cmd.Parameters.AddWithValue("@Summary", additionalInfo.Summary);
                cmd.Parameters.AddWithValue("@KeyWords", additionalInfo.KeyWords);
                cmd.Parameters.AddWithValue("@Rating", additionalInfo.Rating);
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

        public bool Delete(int additionalInfoID) 
        {
            var deleted = false;
            try  
            {  
                SqlCommand cmd = new SqlCommand();  
                cmd.Connection = _conn;  
                cmd.CommandText = "AdditionalInfoDelete";  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdditionalInfoID", additionalInfoID);  
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