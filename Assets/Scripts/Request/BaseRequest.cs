using Common;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade gameFacade;

    public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(actionCode, this);
        gameFacade = GameFacade.Instance;
    }

    /// <summary>
    /// Send request to server.
    /// </summary>
    /// <param name="data">Data.</param>
    public virtual void SendRequest(string data) 
    {
        gameFacade.SendRequest(requestCode, actionCode, data);
    }

    /// <summary>
    /// Response.
    /// </summary>
    /// <param name="data">Data.</param>
    public virtual void OnResponse(string data) { }

    public virtual void OnDestroy() 
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
