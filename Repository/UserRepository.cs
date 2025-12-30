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

        public bool UpdateCompletionTime(int userId, double completionTime)
        {
            using (MySqlConnection conn = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();
                string sql = @"UPDATE User SET completionTime=@completionTime WHERE id=@userId";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@completionTime", completionTime);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    return rowsAffected > 0;
                }
            }
        }

        public List<User> GetLeaderboard(int limit = 10)
        {
            List<User> leaderboard = new List<User>();
            using (MySqlConnection conn = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                conn.Open();
                string sql = @"SELECT * FROM User WHERE completionTime IS NOT NULL ORDER BY completionTime ASC LIMIT @limit";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@limit", limit);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            id = (int)reader["id"],
                            username = (string)reader["username"],
                            password = (string)reader["password"],
                            completionTime = reader.GetDouble("completionTime"),
                            isPaidUser = !reader.IsDBNull(reader.GetOrdinal("isPaidUser")) && reader.GetBoolean("isPaidUser")
                        };
                        leaderboard.Add(user);
                    }
                    conn.Close();
                }
            }
            return leaderboard;
        }
    }
}

