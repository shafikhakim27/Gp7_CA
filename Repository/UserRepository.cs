using Gp7_CA.Models;
using MySql.Data.MySqlClient;

namespace Gp7_CA.Repository
{
    public class UserRepository
    {
        public User? AuthenticateUser(string username, string password)
        {
            User? user = null;
            using (MySqlConnection conn = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();
                string sql = @"SELECT * FROM User WHERE User.username=@username and User.password=@password";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new User();
                        user.id = (int)reader["id"];
                        user.username = (string)reader["username"];
                        user.password = (string)reader["password"];
                        int completionTimeIdx = reader.GetOrdinal("completionTime");
                        user.completionTime = reader.IsDBNull(completionTimeIdx) ? 0.0 : reader.GetDouble(completionTimeIdx);
                        int isPaidUserIdx = reader.GetOrdinal("isPaidUser");
                        user.isPaidUser = reader.IsDBNull(isPaidUserIdx) ? false : reader.GetBoolean(isPaidUserIdx);
                    }
                    conn.Close();
                }
            }
            return user;
        }
    }
}

