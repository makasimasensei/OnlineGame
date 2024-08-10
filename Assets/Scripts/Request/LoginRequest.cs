using Common;

public class LoginRequest : BaseRequest
{
    LogInPanel logInPanel;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        logInPanel = GetComponent<LogInPanel>();
        base.Awake();
    }

    /// <summary>
    /// Send username and password.
    /// </summary>
    /// <param name="username">Username.</param>
    /// <param name="password">Password.</param>
    public void SendRequest(string username, string password)
    {
        string data = username + ":" + password;
        BaseSendRequest(data);
    }

    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        logInPanel.OnLoginResponse(returnCode);
    }
}
