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
        OnProcessDataCallback = facade.HandleResponse;
    }

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

    void Start()
    {
        clientSocket.BeginReceive(message.Bytes, 0, 1024, SocketFlags.None, ReceiveCallback, null);
    }

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
            Debug.LogWarning("Unable to close the connection of server:" + e);
        }
    }
}
