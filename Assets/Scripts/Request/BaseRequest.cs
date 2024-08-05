using Common;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    RequestCode requestCode = RequestCode.None;

    public virtual void Awake()
    {
        GameFacade.Instance.AddRequest(requestCode, this);
    }

    public virtual void Sendrequest() { }
    public virtual void OnResponse() { }

    public virtual void OnDestroy() 
    {
        GameFacade.Instance.RemoveRequest(requestCode);
    }
}
