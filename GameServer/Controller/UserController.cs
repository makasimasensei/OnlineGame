using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        readonly UserDAO userDAO = new();

        public UserController()
        {
            requestCode = RequestCode.User;
        }

        /// <summary>
        /// Call in Server class indirectly.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="client">Client.</param>
        /// <returns>Return code</returns>
        public static string Login(string data, Client client)
        {
            string[] strs = data.Split(':');
            User? user = UserDAO.VerifyUser(client.conn, strs[0], strs[1]);
            if (user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                return ((int)ReturnCode.Success).ToString();
            }
        }

        public static string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(':');
            string username = strs[0];
            string password = strs[1];
            bool res = UserDAO.GetUserByUsername(client.conn, username);
            if (res) return ((int)ReturnCode.Fail).ToString();
            UserDAO.AddUser(client.conn, username, password);
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
