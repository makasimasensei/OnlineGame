using Common;
using GameServer.Controller;
using System.Net;
using System.Net.Sockets;

namespace GameServer.Servers
{
    public class Server
    {
        Socket? server = null;
        readonly IPEndPoint ipEndPoint;
        List<Client> clientsList = new();
        ControllerManager controllerManager;

        /// <summary>
        /// 构造函数（配置IP）
        /// </summary>
        /// <param name="ip">IPv4地址</param>
        /// <param name="port">端口号</param>
        public Server(string ip, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            controllerManager = new(this);
        }

        /// <summary>
        /// 开始创建服务器，绑定和监听
        /// </summary>
        /// <exception cref="NullReferenceException">ipEndPoint不能为空</exception>
        public void Start()
        {
            server ??= new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (ipEndPoint == null) throw new NullReferenceException(nameof(ipEndPoint));
            server.Bind(ipEndPoint);
            server.Listen(0);
            server.BeginAccept(AcceptCallback, server);
        }

        /// <summary>
        /// 从客户端列表中删除当前客户端
        /// </summary>
        /// <param name="client">服务器端创建的客户端</param>
        public void RemoveClient(Client client)
        {
            lock (clientsList)
            {
                clientsList.Remove(client);
            }
        }

        /// <summary>
        /// 服务器接收客户端连接的回调函数
        /// </summary>
        /// <param name="ar">返回的异步结果</param>
        void AcceptCallback(IAsyncResult ar)
        {
            if (ar.AsyncState is Socket server)
            {
                Client socketToCommunicate = new(server.EndAccept(ar), this);
                clientsList.Add(socketToCommunicate);

                socketToCommunicate.Start();
                server.BeginAccept(AcceptCallback, server);
            }
        }

        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
    }
}
