using Common;
using System;
using UnityEditor.PackageManager.Requests;
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

    public Action<ActionCode, BaseRequest> AddRequest;
    public Action<ActionCode> RemoveRequest;
    public Action<RequestCode, ActionCode, string> SendRequest;
    public Action<ActionCode, string> HandleResponse;


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

    void InitManager()
    {
        uiManager = new UIManager(this);
        audioManager = new AudioManager(this);
        playerManager = new PlayerManager(this);
        cameraManager = new CameraManager(this);
        requestManager = new RequestManager(this);
        AddRequest = requestManager.AddRequest;
        RemoveRequest = requestManager.RemoveRequest;
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
