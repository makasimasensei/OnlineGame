using Common;
using GameServer.Model;
using GameServer.Tool;
using MySql.Data.MySqlClient;
using System.Net.Sockets;


namespace GameServer.Servers
{
    public class Client
    {
        public Socket client;
        public Server server;
        Message receiveMsg = new();
        readonly MySqlConnection conn;
        Room room;

        User user;
        Result result;

        public MySqlConnection Conn => conn;

        public Room Room { set => room = value; }

        /// <summary>
        /// Set the data.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="result"></param>
        public void SetUserData(User user, Result result)
        {
            this.user = user;
            this.result = result;
        }

        /// <summary>
        /// Get the data.
        /// </summary>
        public string GetUserData()
        {
            return user.Id + "," + user.Username + "," + result.TotalCount + "," + result.WinCount;
        }

        public int GetUserId()
        {
            return user.Id;
        }

        public Client(Socket c, Server s)
        {
            if (client == null) this.client = c;
            if (server == null) this.server = s;
            conn = ConnHelper.Connect();
        }

        /// <summary>
        /// Start receiving messages.
        /// </summary>
        /// <param name="Bytes">Received data stream.</param>
        /// <param name="startIndex">Start position.</param>
        /// <param name="RemainSize">Remain size</param>
        public void Start()
        {
            client.BeginReceive(receiveMsg.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, client);
        }

        /// <summary>
        /// Send messages.
        /// </summary>
        /// <param name="bytes">Messages the client sends.</param>
        public void Send(ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            client.Send(bytes);
        }

        /// <summary>
        /// Close the client.
        /// </summary>
        public void Close()
        {
            ConnHelper.CloseConnection(Conn);
            if (client != null)
            {
                client.Close();
                if (this.room != null)
                {
                    room.Close(this);
                }
                server.RemoveClient(this);
            }
        }

        /// <summary>
        /// Callback function that receives messages on the server.
        /// </summary>
        /// <param name="ar">The asynchronous result returned.</param>
        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (client == null || client.Connected == false) return;
                int len = client.EndReceive(ar);
                if (len == 0) Close();
                receiveMsg.ReadMessage(OnProcessMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Close();
            }
        }

        /// <summary>
        /// Call the method in ControlManager to process the message.
        /// </summary>
        /// <param name="requestCode">Request code.</param>
        /// <param name="actionCode">Action code.</param>
        /// <param name="data">Data.</param>
        void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }
    }
}
