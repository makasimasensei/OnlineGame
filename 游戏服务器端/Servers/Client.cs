using Common;
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
        MySqlConnection conn;

        public Client(Socket c, Server s)
        {
            if (client == null) this.client = c;
            if (server == null) this.server = s;
            conn = ConnHelper.Connect();
        }

        /// <summary>
        /// 开始接收消息
        /// </summary>
        /// <param name="Bytes">接收到的数据流</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="RemainSize">剩余大小</param>
        public void Start()
        {
            client.BeginReceive(receiveMsg.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, client);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes">客户端发送消息</param>
        public void Send(ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            client.Send(bytes);
        }

        /// <summary>
        /// 关闭客户端
        /// </summary>
        public void Close()
        {
            ConnHelper.CloseConnection(conn);
            if (client != null)
            {
                client.Close();
                server.RemoveClient(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
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

        void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }
    }
}
