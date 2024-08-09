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

    public virtual void SendRequest(string data) 
    {
        gameFacade.SendRequest(requestCode, actionCode, data);
    }

    public virtual void OnResponse(string data) { }

    public virtual void OnDestroy() 
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
