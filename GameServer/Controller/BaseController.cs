using Common;
using GameServer.Servers;

namespace GameServer.Controller
{

    /// <summary>
    /// abstract class!!!
    /// </summary>
    abstract class BaseController
    {
        protected RequestCode requestCode = RequestCode.None;

        public RequestCode RequestCode { get => requestCode; }

        //public virtual string DefaultHandle(string data, Client client, Server server) { return null; }
    }
}
