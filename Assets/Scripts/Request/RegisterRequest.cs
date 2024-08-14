using Common;

public class RegisterRequest : BaseRequest
{
    RegisterPanel registerPanel;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Awake();
    }
    /// <summary>
    /// Send request.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    public void SendRequest(string username, string password)
    {
        string data = username + ":" + password;
        SendRequest(data);
    }

    /// <summary>
    /// Handle response data.
    /// </summary>
    /// <param name="data">Data.</param>
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        registerPanel.OnRegisterResponse(returnCode);
    }
}