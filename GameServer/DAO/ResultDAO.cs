using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
     class ResultDAO
    {
        public static Result GetResultByUserId(MySqlConnection mySqlConnection, int userId)
        {
            MySqlDataReader? mySqlDataReader = null;
            try
            {
                MySqlCommand command = new("select * from result where userid = @userid", mySqlConnection);
                command.Parameters.AddWithValue("userid", userId);
                mySqlDataReader = command.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    int id = mySqlDataReader.GetInt32("Id");
                    int winCount = mySqlDataReader.GetInt32("winCount");
                    int totalCount = mySqlDataReader.GetInt32("totalCount");

                    Result res = new(id, userId, winCount, totalCount);
                    return res;
                }
                else
                {
                    Result res = new(-1, userId, 0, 0);
                    return res;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs in getting username:" + e);
            }
            finally
            {
                mySqlDataReader?.Close();
            }
            return null;
        }
    }
}
