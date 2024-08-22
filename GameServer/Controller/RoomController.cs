using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;
using System.Text;

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
            bool res = UserDAO.GetUserByUsername(client.Conn, username);
            if (res) return ((int)ReturnCode.Fail).ToString();
            UserDAO.AddUser(client.Conn, username, password);
            return ((int)ReturnCode.Success).ToString();
        }

        public static string ListRoom(string data, Client client, Server server)
        {
            StringBuilder sb = new();
            foreach (Room room in server.GetRoomList())
            {
                if (room.IsWaitingJoin())
                {
                    sb.Append(room.GetHouseOwnerData() + "|");
                }
            }
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

        public static string JoinRoom(string data, Client client, Server server)
        {
            int id = int.Parse(data);
            Room? room = server.GetRoomById(id);
            if (room == null)
            {

            }
            else if (room.IsWaitingJoin() == false)
            {

            }
            else
            {

            }
        }
    }
}
