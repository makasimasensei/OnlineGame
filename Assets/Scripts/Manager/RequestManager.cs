using Common;
using System.Collections.Generic;
using UnityEngine;


public class RequestManager : BaseManager
{
    public RequestManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    Dictionary<RequestCode, BaseRequest> requestDict = new Dictionary<RequestCode, BaseRequest>();

    public void AddRequest(RequestCode requestCode, BaseRequest baseRequest)
    {
        requestDict.Add(requestCode, baseRequest);
    }

    public virtual void RemoveRequest(RequestCode requestCode)
    {
        requestDict.Remove(requestCode);
    }

    public void HandleResponse(RequestCode requestCode, string data)
    {
        BaseRequest baseRequest = DictionaryExtension.TryGet<RequestCode, BaseRequest>(requestDict, requestCode);
        if (baseRequest == null)
        {
            Debug.LogWarning("无法得到RequestCode" + requestCode);
        }
        baseRequest.OnResponse();
    }
}
