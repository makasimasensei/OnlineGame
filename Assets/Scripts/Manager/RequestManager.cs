using Common;
using System.Collections.Generic;
using UnityEngine;


public class RequestManager : BaseManager
{
    public readonly static Dictionary<ActionCode, BaseRequest> actionDict = new Dictionary<ActionCode, BaseRequest>();

    public RequestManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        actionDict.Add(actionCode, baseRequest);
    }

    public virtual void RemoveRequest(ActionCode actionCode)
    {
        actionDict.Remove(actionCode);
    }

    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest baseRequest = actionDict.TryGet(actionCode);
        if (baseRequest == null)
        {
            Debug.LogWarning("Can't get ActionCode:" + actionCode);
        }
        baseRequest.OnResponse(data);
    }
}
