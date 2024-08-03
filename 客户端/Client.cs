using System.Net;
using System.Net.Sockets;
using System.Text;
using 客户端;

byte[] buffer = new byte[1024];

void StartClientAsync()
{

    Socket? client = null;
    client ??= new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    IPAddressConfig(client, "192.168.0.109", 6000);

    Receive(client);

    while (true) Send(client);
}

void IPAddressConfig(Socket c, string IPv4, ushort port)
{
    c.Connect(new IPEndPoint(IPAddress.Parse(IPv4), port));
}

void Receive(Socket c)
{
    int count = c.Receive(buffer);
    string m = Encoding.UTF8.GetString(buffer, 0, count);
    Console.WriteLine(m);
}

void Send(Socket c)
{
    string? input = Console.ReadLine();
    if (input != null)
    {
        c.Send(Message.GetBytes(input));
    }
}

StartClientAsync();
Console.ReadKey();
//byte[] b =  Message.GetBytes("1");
//foreach(byte b2 in b)
//{
//    Console.Write(b2);
//}