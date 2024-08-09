using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public void Login(string data, Client client, Server server)
        {

        }
    }
}
