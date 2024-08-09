using Common;
using System.Collections.Generic;
using UnityEngine;


public class RequestManager : BaseManager
{
    public RequestManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    Dictionary<ActionCode, BaseRequest> actionDict = new Dictionary<ActionCode, BaseRequest>();

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
        BaseRequest baseRequest = DictionaryExtension.TryGet<ActionCode, BaseRequest>(actionDict, actionCode);
        if (baseRequest == null)
        {
            Debug.LogWarning("无法得到ActionCode" + actionCode);
        }
        baseRequest.OnResponse(data);
    }
}
