using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
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
        public static string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(':');
            User? user = UserDAO.VerifyUser(client.Conn, strs[0], strs[1]);
            if (user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result result = ResultDAO.GetResultByUserId(client.Conn, user.Id);
                client.SetUserData(user, result);
                return string.Format("{0}, {1}, {2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, result.TotalCount, result.WinCount);
            }
        }

        /// <summary>
        /// Call in Server class indirectly.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="client"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(':');
            string username = strs[0];
            string password = strs[1];
            bool res = UserDAO.GetUserByUsername(client.Conn, username);
            if (res) return ((int)ReturnCode.Fail).ToString();
            UserDAO.AddUser(client.Conn, username, password);
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
