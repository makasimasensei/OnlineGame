using Common;
using UnityEngine;


public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade gameFacade;

    public virtual void Awake()
    {
        GameFacade.AddRequest(actionCode, this);
        gameFacade = GameFacade.Instance;
    }

    /// <summary>
    /// Send request method in BaseRequest class.
    /// </summary>
    /// <param name="data">Data needs to be sent.</param>
    public virtual void BaseSendRequest(string data) 
    {
        GameFacade.SendRequest(requestCode, actionCode, data);
    }

    public virtual void OnResponse(string data) { }

    public virtual void OnDestroy() 
    {
        GameFacade.RemoveRequest(actionCode);
    }
}
