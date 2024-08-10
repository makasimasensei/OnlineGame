using MySql.Data.MySqlClient;


namespace GameServer.Tool
{
    class ConnHelper
    {
        public const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=game;user=root;password=gzy19980926;";

        /// <summary>
        /// Connect MySql.
        /// </summary>
        /// <returns>The connection of MySql.</returns>
        public static MySqlConnection Connect()
        {
            MySqlConnection mySqlConnection = new(CONNECTIONSTRING);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurs when connecting to MySQL:" + e.ToString());
            }
            return mySqlConnection;
        }

        /// <summary>
        /// Close MySql.
        /// </summary>
        /// <param name="conn">The connection of MySql.</param>
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
            else Console.WriteLine("MySqlConnection can not be null.");
        }
    }
}
