using Common;
using System.Collections.Generic;
using UnityEngine;


public class RequestManager : BaseManager
{
    public RequestManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    readonly Dictionary<ActionCode, BaseRequest> actionDict = new Dictionary<ActionCode, BaseRequest>();

    /// <summary>
    /// Add actionCode from actionDict.
    /// </summary>
    /// <param name="actionCode">Action code.</param>
    /// <param name="baseRequest">BaseRequest class.</param>
    public void AddAction(ActionCode actionCode, BaseRequest baseRequest)
    {
        actionDict.Add(actionCode, baseRequest);
    }

    /// <summary>
    /// Remove actionCode from actionDict.
    /// </summary>
    /// <param name="actionCode">Action code.</param>
    public virtual void RemoveAction(ActionCode actionCode)
    {
        actionDict.Remove(actionCode);
    }

    /// <summary>
    /// Handle response data.
    /// </summary>
    /// <param name="actionCode">Action code.</param>
    /// <param name="data">Data.</param>
    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest Request = DictionaryExtension.TryGet<ActionCode, BaseRequest>(actionDict, actionCode);
        if (Request == null)
        {
            Debug.LogWarning("Can't get ActionCode:" + actionCode);
        }
        Request.OnResponse(data);
    }
}
