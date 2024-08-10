using Common;
using System;
using System.Net.Sockets;
using UnityEngine;


public class ClientManager : BaseManager
{
    const string ip = "127.0.0.1";
    const int port = 6000;

    Socket clientSocket;
    Message message = new Message();

    Action<ActionCode, string> OnProcessDataCallback;

    public ClientManager(GameFacade facade) : base(facade)
    {
        OnProcessDataCallback = GameFacade.HandleResponse;
    }

    /// <summary>
    /// initialization.
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(ip, port);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to connect to the server:" + e);
        }
        Start();
    }

    /// <summary>
    /// Start to receive.
    /// </summary>
    void Start()
    {
        clientSocket.BeginReceive(message.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, null);
    }

    /// <summary>
    /// Callback function that accepts clients on the server.
    /// </summary>
    /// <param name="ar">The asynchronous result returned.</param>
    void ReceiveCallback(IAsyncResult ar)
    {
        int count = clientSocket.EndReceive(ar);
        message.ReadMessage(OnProcessDataCallback);
        Start();
    }

    /// <summary>
    /// Send request code, action code and data to the server.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="actionCode">Action code.</param>
    /// <param name="data">Data.</param>
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭服务器连接：" + e);
        }
    }
}
