using System.Net;
using System.Net.Sockets;
using System.Text;
using 服务器端;

//数据
Message receiveMsg = new();

//开始异步创建服务器
void StartServerAsync()
{
    Socket? server = null;
    server ??= new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    IPAddressConfig(server, "192.168.0.109", 6000, 10);

    server.BeginAccept(AcceptCallback, server);
}

//IP地址的配置
void IPAddressConfig(Socket s, string IPv4, ushort port, int connectNum)
{
    IPAddress ipAddress = IPAddress.Parse(IPv4);
    IPEndPoint ipEndPoint = new(ipAddress, port);
    s.Bind(ipEndPoint);
    s.Listen(connectNum);
}

//客户端接收的回调函数
void AcceptCallback(IAsyncResult ar)
{
    if (ar.AsyncState is Socket server)
    {
        Socket socketToCommunicate = server.EndAccept(ar);

        string msg = "Hello world!你好世界";
        byte[] bytes = Encoding.UTF8.GetBytes(msg);
        socketToCommunicate.Send(bytes);

        socketToCommunicate.BeginReceive(receiveMsg.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, socketToCommunicate);
        server.BeginAccept(AcceptCallback, server);
    }
}

//客户端接收的回调函数
void ReceiveCallback(IAsyncResult ar)
{
    if (ar.AsyncState is Socket socketToCommunicate)
    {
        try
        {
            int len = socketToCommunicate.EndReceive(ar);
            receiveMsg.ReadMessage();
            socketToCommunicate.BeginReceive(receiveMsg.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, socketToCommunicate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            socketToCommunicate?.Close();
        }
        finally
        {
        }
    }
}

StartServerAsync();
Console.ReadKey();