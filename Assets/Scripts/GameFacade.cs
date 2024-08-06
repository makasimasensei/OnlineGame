using Common;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    static GameFacade _instance;

    UIManager uiManager;
    AudioManager audioManager;
    PlayerManager playerManager;
    CameraManager cameraManager;
    RequestManager requestManager;
    ClientManager client;

    public static GameFacade Instance { get => _instance; }

    void Start()
    {
        InitManager();
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject); return;
        }
        _instance = this;
    }

    void InitManager()
    {
        uiManager = new UIManager(this);
        audioManager = new AudioManager(this);
        playerManager = new PlayerManager(this);
        cameraManager = new CameraManager(this);
        requestManager = new RequestManager(this);
        client = new ClientManager(this);

        uiManager.OnInit();
        audioManager.OnInit();
        playerManager.OnInit();
        cameraManager.OnInit();
        requestManager.OnInit();
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

    public void AddRequest(RequestCode requestCode, BaseRequest baseRequest)
    {
        requestManager.AddRequest(requestCode, baseRequest);
    }

    public void RemoveRequest(RequestCode requestCode)
    {
        requestManager.RemoveRequest(requestCode);
    }

    public void HandleResponse(RequestCode requestCode, string data)
    {
        requestManager.HandleResponse(requestCode, data);
    }

    public void GameFacadeCallShowMessage(string msg)
    {
        uiManager.UIManagerCallShowMessage(msg);
    }
}
