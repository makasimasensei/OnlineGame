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

        public string Login(string data, Client client, Server server)
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
    }
}
