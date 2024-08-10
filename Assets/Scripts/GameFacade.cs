using Common;
using System;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    static GameFacade instance;

    UIManager uiManager;
    AudioManager audioManager;
    PlayerManager playerManager;
    CameraManager cameraManager;
    RequestManager requestManager;
    ClientManager clientManager;

    //Delegation to the SendRequest method in clientManager
    public static Action<RequestCode, ActionCode, string> SendRequest;

    //Delegation to the AddRequest method in requestManager
    public static Action<ActionCode, BaseRequest> AddRequest;

    //Delegation to the HandleResponse method in requestManager
    public static Action<ActionCode, string> HandleResponse;

    //Delegation to the RemoveRequest method in requestManager
    public static Action<ActionCode> RemoveRequest;

    public static GameFacade Instance { get => instance; }

    void Start()
    {
        InitManager();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); return;
        }
        instance = this;
    }

    /// <summary>
    /// Initialize various Managers.
    /// </summary>
    void InitManager()
    {
        uiManager = new UIManager(this);
        audioManager = new AudioManager(this);
        playerManager = new PlayerManager(this);
        cameraManager = new CameraManager(this);

        requestManager = new RequestManager(this);
        HandleResponse = requestManager.HandleResponse;
        AddRequest = requestManager.AddRequest;
        RemoveRequest = requestManager.RemoveRequest;

        clientManager = new ClientManager(this);
        SendRequest = clientManager.SendRequest;

        uiManager.OnInit();
        audioManager.OnInit();
        playerManager.OnInit();
        cameraManager.OnInit();
        requestManager.OnInit();
        clientManager.OnInit();
    }

    /// <summary>
    /// Destroy existed Managers.
    /// </summary>
    void DestroyManager()
    {
        uiManager.OnDestroy();
        audioManager.OnDestroy();
        playerManager.OnDestroy();
        cameraManager.OnDestroy();
        requestManager.OnDestroy();
    }

    void OnDestroy()
    {
        DestroyManager();
    }

    public void GameFacadeCallShowMessage(string msg)
    {
        uiManager.UIManagerCallShowMessage(msg);
    }
}
