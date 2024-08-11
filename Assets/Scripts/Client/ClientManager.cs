using Common;
using System;
using System.Net.Sockets;
using UnityEngine;

public class ClientManager : BaseManager
{
    const string ip = "127.0.0.1";
    const int port = 6000;

    Socket clientSocket;
    readonly Message message = new Message();

    readonly Action<ActionCode, string> OnProcessDataCallback;

    public ClientManager(GameFacade facade) : base(facade)
    {
        OnProcessDataCallback = facade.HandleResponse;
    }

    /// <summary>
    /// Initiation.
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
            Debug.LogWarning("Unable to connect to server:" + e);
        }
        Start();
    }

    /// <summary>
    /// Begin receive messages.
    /// </summary>
    void Start()
    {
        clientSocket.BeginReceive(message.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, null);
    }

    /// <summary>
    /// Callback function that receives messages from the server.
    /// </summary>
    /// <param name="ar">The asynchronous result returned.</param>
    void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            int count = clientSocket.EndReceive(ar);
            message.ReadMessage(OnProcessDataCallback);
            Start();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// Send request to server.
    /// </summary>
    /// <param name="requestCode">Request code.</param>
    /// <param name="actionCode">Action code.</param>
    /// <param name="data">Data.</param>
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    /// <summary>
    /// Destroy.
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();

        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to close the connection of server:" + e);
        }
    }
}
