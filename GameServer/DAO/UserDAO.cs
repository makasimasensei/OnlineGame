using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class UserDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
    }
}
