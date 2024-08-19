using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    class RoomController : BaseController
    {
        public RoomController()
        {
            requestCode = RequestCode.Room;
        }

        /// <summary>
        /// Call in Server class indirectly.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="client">Client.</param>
        /// <returns>Return code</returns>
        public static string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client);
            return ((int)ReturnCode.Success).ToString();
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
            bool res = UserDAO.GetUserByUsername(client.conn, username);
            if (res) return ((int)ReturnCode.Fail).ToString();
            UserDAO.AddUser(client.conn, username, password);
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
