using Common;
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
        clientManager = new ClientManager(this);

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

    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestManager.AddRequest(actionCode, baseRequest);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestManager.HandleResponse(actionCode, data);
    }

    public void GameFacadeCallShowMessage(string msg)
    {
        uiManager.UIManagerCallShowMessage(msg);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientManager.SendRequest(requestCode, actionCode, data);
    }

}
