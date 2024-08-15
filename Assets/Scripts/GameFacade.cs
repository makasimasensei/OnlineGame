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

    ///Delegation to the ShowMessage method in uiManager.
    public Action<string> ShowMessage;
    ///Delegation to the AddRequest method in requestManager.
    public Action<ActionCode, BaseRequest> AddRequest;
    ///Delegation to the RemoveRequest method in requestManager.
    public Action<ActionCode> RemoveRequest;
    ///Delegation to the SendRequest method in clientManager.
    public Action<RequestCode, ActionCode, string> SendRequest;
    ///Delegation to the HandleResponse method in requestManager.
    public Action<ActionCode, string> HandleResponse;
    ///Delegation to the PlayBgSound method in audioManager.
    public Action<string> PlayBgSound;
    ///Delegation to the PlayNormalSound method in audioManager.
    public Action<string> PlayNormalSound;

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

     void Update()
    {
        UpdateManager();
    }

    /// <summary>
    /// Initiation.
    /// </summary>
    void InitManager()
    {
        uiManager = new UIManager(this);
        ShowMessage = uiManager.ShowMessageSync;

        audioManager = new AudioManager(this);
        PlayBgSound = audioManager.PlayBgSound;
        PlayNormalSound = audioManager.PlayNormalSound;

        playerManager = new PlayerManager(this);
        cameraManager = new CameraManager(this);
        requestManager = new RequestManager(this);
        AddRequest = requestManager.AddAction;
        RemoveRequest = requestManager.RemoveAction;
        HandleResponse = requestManager.HandleResponse;
        clientManager = new ClientManager(this);
        SendRequest = clientManager.SendRequest;

        uiManager.OnInit();
        audioManager.OnInit();
        playerManager.OnInit();
        cameraManager.OnInit();
        requestManager.OnInit();
        clientManager.OnInit();
    }

    void UpdateManager()
    {
        uiManager.Update();
    }

    /// <summary>
    /// Destroy all managers.
    /// </summary>
    void DestroyManager()
    {
        uiManager.OnDestroy();
        audioManager.OnDestroy();
        playerManager.OnDestroy();
        cameraManager.OnDestroy();
        requestManager.OnDestroy();
        clientManager.OnDestroy() ;
    }

    void OnDestroy()
    {
        DestroyManager();
    }
}
