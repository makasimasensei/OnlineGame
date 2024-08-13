using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class UserDAO
    {
        /// <summary>
        /// Verify whether user is in the database.
        /// </summary>
        /// <param name="connection">Connection to MySql.</param>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>User class.</returns>
        public static User? VerifyUser(MySqlConnection connection, string username, string password)
        {
            MySqlDataReader? mySqlDataReader = null;
            try
            {
                MySqlCommand command = new("select * from user where username = @username and password = @password", connection);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    int id = mySqlDataReader.GetInt32("id");
                    User user = new(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs in identification:" + e);
            }
            finally
            {
                mySqlDataReader?.Close();
            }
            return null;
        }

        public static bool GetUserByUsername(MySqlConnection connection, string username)
        {
            MySqlDataReader? mySqlDataReader = null;
            try
            {
                MySqlCommand command = new("select * from user where username = @username", connection);
                command.Parameters.AddWithValue("username", username);
                mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read()) return true;
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs in getting username:" + e);
            }
            finally
            {
                mySqlDataReader?.Close();
            }
            return false;
        }

        public static void AddUser(MySqlConnection connection, string username, string password)
        {
            try
            {
                MySqlCommand command = new("insert into user set username = @username, password = @password", connection);
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", password);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs in adding user:" + e);
            }
        }
    }
}
