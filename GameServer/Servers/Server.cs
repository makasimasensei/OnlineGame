using Common;
using GameServer.Controller;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;


namespace GameServer.Servers
{
    public class Server
    {
        Socket? server = null;
        readonly IPEndPoint ipEndPoint;
        readonly List<Client> clientsList = new();
        readonly List<Room> roomList = new();
        readonly ControllerManager controllerManager;
        public readonly Action<RequestCode, ActionCode, string, Client> HandleRequest;

        /// <summary>
        /// Constructor (configuring IP)
        /// </summary>
        /// <param name="ip">IPv4 address</param>
        /// <param name="port">port</param>
        public Server(string ip, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            controllerManager = new(this);
            //Generic delegation
            HandleRequest = controllerManager.HandleRequest;
        }

        /// <summary>
        /// Start creating server, binding, and listening.
        /// </summary>
        /// <exception cref="NullReferenceException">ipEndPoint can't be null.</exception>
        public void Start()
        {
            server ??= new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (ipEndPoint == null) throw new NullReferenceException(nameof(ipEndPoint));
            server.Bind(ipEndPoint);
            server.Listen(0);
            server.BeginAccept(AcceptCallback, server);
        }

        /// <summary>
        /// Delete current client from the client list
        /// </summary>
        /// <param name="client">Client created by the server.</param>
        public void RemoveClient(Client client)
        {
            lock (clientsList)
            {
                clientsList.Remove(client);
            }
        }

        /// <summary>
        /// Callback function that accepts clients on the server.
        /// </summary>
        /// <param name="ar">The asynchronous result returned.</param>
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

        /// <summary>
        /// Server send responses to clients
        /// </summary>
        /// <param name="client">Client instance</param>
        /// <param name="actionCode">Code of action</param>
        /// <param name="data">Data from clients</param>
        public static void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }
    }
}
