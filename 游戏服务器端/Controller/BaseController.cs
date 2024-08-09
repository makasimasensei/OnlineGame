using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    abstract class BaseController
    {
        RequestCode requestCode = RequestCode.None;

        public RequestCode RequestCode { get => requestCode; }

        public virtual string DefaultHandle(string data, Client client, Server server) { return null; }
    }
}
